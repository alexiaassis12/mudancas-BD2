import React, { useEffect, useState } from 'react'
import DataTable from '../DataTable/DataTable'
import './TableSection.css'

const endpointMap = {
  cidades: 'Cidade',
  empresas: 'Empresa',
  clientes: 'Cliente',
  funcionarios: 'Funcionario',
  servicos: 'Servico',
  pedidos: 'Pedido',
}

const createFieldMap = {
  cidades: [
    { key: 'NomeCidade', label: 'Nome da Cidade', type: 'text', required: true, placeholder: 'São Paulo' },
    { key: 'Estado', label: 'Estado', type: 'text', required: true, placeholder: 'SP' },
  ],
  empresas: [
    { key: 'Nome', label: 'Nome', type: 'text', required: true, placeholder: 'Vertex Express' },
    { key: 'Endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Rua das Flores, 123' },
  ],
  clientes: [
    { key: 'Cpf', label: 'CPF', type: 'text', required: true, placeholder: '00000000000' },
    { key: 'Rg', label: 'RG', type: 'text', required: true, placeholder: '12345678' },
    { key: 'NomeCompleto', label: 'Nome Completo', type: 'text', required: true, placeholder: 'João Silva' },
    { key: 'Endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Av. Brasil, 100' },
  ],
  funcionarios: [
    { key: 'CpfFunc', label: 'CPF', type: 'text', required: true, placeholder: '00000000000' },
    { key: 'RgFunc', label: 'RG', type: 'text', required: true, placeholder: '12345678' },
    { key: 'NomeCompleto', label: 'Nome Completo', type: 'text', required: true, placeholder: 'Ana Souza' },
    { key: 'Endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Rua ABC, 456' },
    { key: 'TipoFunc', label: 'Tipo', type: 'text', required: true, placeholder: 'MOTORISTA' },
    { key: 'Salario', label: 'Salário', type: 'number', required: true, placeholder: '0.00', min: 0, step: 0.01 },
  ],
  servicos: [
    { key: 'NomeServico', label: 'Nome do Serviço', type: 'text', required: true, placeholder: 'Transporte de carga' },
    { key: 'TipoEspecializacao', label: 'Especialização', type: 'text', required: true, placeholder: 'NENHUM' },
  ],
  pedidos: [
    { key: 'IdEmpresa', label: 'ID Empresa', type: 'number', required: true, placeholder: '1' },
    { key: 'CodigoCliente', label: 'Código Cliente', type: 'number', required: true, placeholder: '1' },
    { key: 'IdCidadeOrigem', label: 'Cidade Origem (ID)', type: 'number', required: false, placeholder: '1' },
    { key: 'EnderecoOrigem', label: 'Endereço Origem', type: 'text', required: false, placeholder: 'Rua de Origem' },
    { key: 'IdCidadeDestino', label: 'Cidade Destino (ID)', type: 'number', required: false, placeholder: '1' },
    { key: 'EnderecoDestino', label: 'Endereço Destino', type: 'text', required: false, placeholder: 'Rua de Destino' },
    { key: 'DataSolicitacao', label: 'Data Solicitação', type: 'date', required: false },
  ],
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
  const [createData, setCreateData] = useState({})
  const [creating, setCreating] = useState(false)
  const [createError, setCreateError] = useState(null)

  const API_BASE = 'http://localhost:5065'
  const createFields = createFieldMap[sectionId] || []

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

  const initializeCreateData = () => {
    return createFields.reduce((acc, field) => {
      acc[field.key] = ''
      return acc
    }, {})
  }

  useEffect(() => {
    setCreateData(initializeCreateData())
    setCreateError(null)
    setCreating(false)
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

  useEffect(() => {
    loadData()
  }, [sectionId, API_BASE])

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
        if (value === '' || value == null) {
          return
        }

        if (field.type === 'number') {
          payload[field.key] = Number(value)
        } else if (field.type === 'date') {
          payload[field.key] = value
        } else {
          payload[field.key] = value
        }
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

  return (
    <section className="page-view">
      <header className="page-header">
        <div>
          <p className="eyebrow">Gerenciar</p>
          <h1>{title}</h1>
          <p className="page-description">{description}</p>
        </div>
      </header>

      {createFields.length > 0 && (
        <div className="create-row-panel">
          <button
            type="button"
            className="create-row-button"
            onClick={handleCreate}
            disabled={creating}
          >
            {creating ? 'CRIANDO...' : 'CRIAR'}
          </button>

          <div className="create-row-inputs">
            {createFields.map((field) => (
              <label className="create-row-field" key={field.key}>
                <span>{field.label}</span>
                <input
                  type={field.type}
                  value={createData[field.key] ?? ''}
                  placeholder={field.placeholder || ''}
                  min={field.min}
                  step={field.step}
                  onChange={(event) =>
                    setCreateData((prev) => ({
                      ...prev,
                      [field.key]: event.target.value,
                    }))
                  }
                />
              </label>
            ))}
          </div>

          {createError && <p className="create-row-error">{createError}</p>}
        </div>
      )}

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
