import { useEffect, useState } from 'react'
import { endpointMap, createFieldMap, updateKeyMap, formatLabel, getDeleteValue, getFieldConfig } from './TableSectionConfig'

const API_BASE = 'http://localhost:5065'

export function useTableSectionData(sectionId) {
  const [rows, setRows] = useState([])
  const [columns, setColumns] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [createData, setCreateData] = useState({})
  const [creating, setCreating] = useState(false)
  const [createError, setCreateError] = useState(null)
  const [editError, setEditError] = useState(null)

  const createFields = createFieldMap[sectionId] || []
  const updateEnabled = Boolean(updateKeyMap[sectionId])

  const initializeCreateData = () => {
    return createFields.reduce((acc, field) => {
      acc[field.key] = ''
      return acc
    }, {})
  }

  useEffect(() => {
    setCreateData(initializeCreateData())
    setCreateError(null)
    setEditError(null)
  }, [sectionId])

  const loadData = async () => {
    const endpoint = endpointMap[sectionId]
    if (!endpoint) {
      setRows([])
      setColumns([])
      return
    }

    setLoading(true)
    setError(null)

    try {
      const res = await fetch(`${API_BASE}/api/${endpoint}`)
      if (!res.ok) throw new Error(`HTTP ${res.status}`)
      const data = await res.json()
      const arr = Array.isArray(data) ? data : [data]

      setRows(arr)
      if (arr.length > 0) {
        const keys = Object.keys(arr[0])
        setColumns(keys.map((key) => ({ key, label: formatLabel(key) })))
      } else {
        setColumns([])
      }
    } catch (err) {
      setError(err.message || 'Erro ao buscar dados')
      setRows([])
      setColumns([])
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    loadData()
  }, [sectionId])

  const handleCreate = async () => {
    const endpoint = endpointMap[sectionId]
    if (!endpoint) {
      setCreateError('Seção não suportada para criação')
      return
    }

    const missingField = createFields.find(
      (field) => field.required && (createData[field.key] === '' || createData[field.key] == null)
    )

    if (missingField) {
      setCreateError(`Preencha o campo ${missingField.label}`)
      return
    }

    setCreateError(null)
    setCreating(true)

    try {
      const payload = {}
      createFields.forEach((field) => {
        const value = createData[field.key]
        if (value === '' || value == null) return
        payload[field.key] = field.type === 'number' ? Number(value) : value
      })

      if (sectionId === 'pedidos' && !payload.DataSolicitacao) {
        payload.DataSolicitacao = new Date().toISOString()
      }

      const res = await fetch(`${API_BASE}/api/${endpoint}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      })

      if (!res.ok) {
        const text = await res.text()
        throw new Error(text || `Falha ao criar item (${res.status})`)
      }

      await loadData()
      setCreateData(initializeCreateData())
    } catch (err) {
      setCreateError(err.message || 'Erro ao criar item')
    } finally {
      setCreating(false)
    }
  }

  const handleDelete = async (row) => {
    const endpoint = endpointMap[sectionId]
    const identifier = getDeleteValue(row, sectionId)

    if (!endpoint || !identifier) {
      throw new Error('Não foi possível identificar o item para exclusão')
    }

    const res = await fetch(`${API_BASE}/api/${endpoint}/${encodeURIComponent(identifier)}`, {
      method: 'DELETE',
    })

    if (!res.ok) {
      throw new Error(`Falha ao excluir item (${res.status})`)
    }

    setRows((prevRows) => prevRows.filter((item) => item !== row))
  }

  const handleSave = async (row, updatedData) => {
    if (!updateEnabled) return

    const endpoint = endpointMap[sectionId]
    const pathKey = updateKeyMap[sectionId]
    const identifier = row?.[pathKey]

    if (!endpoint || !identifier) {
      throw new Error('Não foi possível identificar o item para atualização')
    }

    setEditError(null)

    const payload = { ...updatedData }

    Object.keys(payload).forEach((key) => {
      const value = payload[key]
      if (value === undefined) return

      const fieldConfig = getFieldConfig(key)
      if (fieldConfig?.type === 'number') {
        payload[key] = value === '' || value == null ? null : Number(value)
      } else if (fieldConfig?.type === 'date') {
        payload[key] = value
      } else {
        payload[key] = value
      }
    })

    const res = await fetch(`${API_BASE}/api/${endpoint}/${encodeURIComponent(identifier)}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    })

    if (!res.ok) {
      const text = await res.text()
      setEditError(text || `Falha ao atualizar item (${res.status})`)
      throw new Error(text || `Falha ao atualizar item (${res.status})`)
    }

    await loadData()
  }

  return {
    rows,
    columns,
    loading,
    error,
    createFields,
    createData,
    setCreateData,
    creating,
    createError,
    editError,
    onCreate: handleCreate,
    onDelete: handleDelete,
    onSave: updateEnabled ? handleSave : undefined,
  }
}
