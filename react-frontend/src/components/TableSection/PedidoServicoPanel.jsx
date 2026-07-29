// components/TableSection/PedidoServicoPanel.jsx
import React, { useState } from 'react'

function PedidoServicoPanel({ onAddServico, loading }) {
  const [servicoData, setServicoData] = useState({
    codigoPedido: '',
    nomeServico: '',
    tempoDuracao: '',
    pesoCarga: '',
  })
  const [error, setError] = useState(null)

  const handleChange = (field, value) => {
    setServicoData(prev => ({ ...prev, [field]: value }))
    setError(null)
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    
    // Validações
    if (!servicoData.codigoPedido || !servicoData.nomeServico || !servicoData.tempoDuracao) {
      setError('Preencha todos os campos obrigatórios')
      return
    }

    const payload = {
      codigoPedido: Number(servicoData.codigoPedido),
      nomeServico: servicoData.nomeServico,
      tempoDuracao: Number(servicoData.tempoDuracao),
      pesoCarga: servicoData.pesoCarga ? Number(servicoData.pesoCarga) : null,
    }

    try {
      await onAddServico(payload)
      // Limpar formulário
      setServicoData({
        codigoPedido: '',
        nomeServico: '',
        tempoDuracao: '',
        pesoCarga: '',
      })
      setError(null)
    } catch (err) {
      setError(err.message || 'Erro ao adicionar serviço')
    }
  }

  return (
    <div className="pedido-servico-panel">
      <h3>Adicionar Serviço ao Pedido</h3>
      <form onSubmit={handleSubmit}>
        <div className="pedido-servico-inputs">
          <label>
            <span>Código do Pedido *</span>
            <input
              type="number"
              value={servicoData.codigoPedido}
              onChange={(e) => handleChange('codigoPedido', e.target.value)}
              placeholder="1"
              min="1"
              required
            />
          </label>
          <label>
            <span>Nome do Serviço *</span>
            <input
              type="text"
              value={servicoData.nomeServico}
              onChange={(e) => handleChange('nomeServico', e.target.value)}
              placeholder="Transporte Padrão"
              required
            />
          </label>
          <label>
            <span>Tempo de Duração (horas) *</span>
            <input
              type="number"
              value={servicoData.tempoDuracao}
              onChange={(e) => handleChange('tempoDuracao', e.target.value)}
              placeholder="4"
              min="0.1"
              step="0.5"
              required
            />
          </label>
          <label>
            <span>Peso da Carga (kg)</span>
            <input
              type="number"
              value={servicoData.pesoCarga}
              onChange={(e) => handleChange('pesoCarga', e.target.value)}
              placeholder="0"
              min="0"
              step="10"
            />
          </label>
        </div>
        {error && <p className="create-row-error">{error}</p>}
        <button type="submit" disabled={loading}>
          {loading ? 'ADICIONANDO...' : 'ADICIONAR SERVIÇO'}
        </button>
      </form>
    </div>
  )
}

export default PedidoServicoPanel