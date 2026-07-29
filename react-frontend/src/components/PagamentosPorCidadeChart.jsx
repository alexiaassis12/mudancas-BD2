import {
  BarChart,
  Bar,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'

function PagamentosPorCidadeChart({ data = [] }) {
  const items = Array.isArray(data) ? data : []

  const chartData = items.map((item) => ({
    name: item.NomeCidade ?? item.nomeCidade ?? 'Cidade desconhecida',
    valor: Number(item.ValorInvestido ?? item.valorInvestido ?? 0),
  }))

  if (chartData.length === 0) {
    return <div className="chart-empty">Nenhum dado disponível para pagamentos por cidade.</div>
  }

  return (
    <div className="chart-panel">
      <ResponsiveContainer width="100%" height={300}>
        <BarChart data={chartData} margin={{ top: 8, right: 16, left: 0, bottom: 40 }}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" angle={-20} textAnchor="end" interval={0} />
          <YAxis tickFormatter={(value) => `R$ ${value.toLocaleString('pt-BR')}`} />
          <Tooltip formatter={(value) => [`R$ ${Number(value).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}`, 'Pagamento']} />
          <Bar dataKey="valor" fill="#2c7a7b" radius={[6, 6, 0, 0]} />
        </BarChart>
      </ResponsiveContainer>
    </div>
  )
}

export default PagamentosPorCidadeChart
