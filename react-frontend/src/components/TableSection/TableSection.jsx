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

const deleteKeyMap = {
  cidades: 'idCidade',
  empresas: 'idEmpresa',
  clientes: 'idCliente',
  funcionarios: 'cpf',
  servicos: 'nomeServico',
  pedidos: 'idPedido',
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

  const getDeleteValue = (row) => {
    const candidateKeys = [deleteKeyMap[sectionId], 'id', 'id_cidade', 'id_empresa', 'id_cliente', 'id_pedido', 'id_funcionario', 'id_servico', 'cpf', 'nomeServico', 'nome_servico']

    return candidateKeys
      .map((key) => row?.[key])
      .find((value) => value !== undefined && value !== null && value !== '')
  }

  const handleDeleteRow = async (row) => {
    const endpoint = endpointMap[sectionId]
    const value = getDeleteValue(row)

    if (!endpoint || !value) {
      throw new Error('Não foi possível identificar o item para exclusão')
    }

    const res = await fetch(`${API_BASE}/api/${endpoint}/${encodeURIComponent(value)}`, {
      method: 'DELETE',
    })

    if (!res.ok) {
      throw new Error(`Falha ao excluir item (${res.status})`)
    }

    setRows((prevRows) => prevRows.filter((item) => item !== row))
  }

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
        <DataTable columns={columns} rows={rows} onDelete={handleDeleteRow} />
      )}
    </section>
  )
}

export default TableSection
