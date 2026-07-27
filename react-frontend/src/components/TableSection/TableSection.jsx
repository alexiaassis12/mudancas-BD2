import React, { useEffect, useState } from 'react'
import DataTable from '../DataTable/DataTable'

const endpointMap = {
  cidades: 'Cidade',
  empresas: 'Empresa',
  clientes: 'Cliente',
  funcionarios: 'Funcionario',
  servicos: 'Servico',
  pedidos: 'Pedido',
}

const formatLabel = (key) => {
  if (!key) return ''
  return key
    .replace(/_/g, ' ')
    .replace(/([a-z])([A-Z])/g, '$1 $2')
    .replace(/(^|\s)\S/g, (t) => t.toUpperCase())
}

function TableSection({ sectionId, title, description }) {
  const [rows, setRows] = useState([])
  const [columns, setColumns] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)

  const API_BASE = 'http://localhost:5065'

  useEffect(() => {
    async function load() {
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
          const cols = keys.map((k) => ({ key: k, label: formatLabel(k) }))
          setColumns(cols)
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

    load()
  }, [sectionId, API_BASE])

  return (
    <section className="page-view">
      <header className="page-header">
        <div>
          <p className="eyebrow">Gerenciar</p>
          <h1>{title}</h1>
          <p className="page-description">{description}</p>
        </div>
      </header>

      {loading ? (
        <p>Carregando...</p>
      ) : error ? (
        <p style={{ color: 'red' }}>Erro: {error}</p>
      ) : (
        <DataTable columns={columns} rows={rows} />
      )}
    </section>
  )
}

export default TableSection
