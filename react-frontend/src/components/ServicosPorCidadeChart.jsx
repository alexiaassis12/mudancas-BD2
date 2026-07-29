import {
  BarChart,
  Bar,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from 'recharts'

function ServicosPorCidadeChart({ data = [] }) {
  const items = Array.isArray(data) ? data : []

  const chartData = items.map((item) => ({
    name: item.NomeCidade ?? item.nomeCidade ?? 'Cidade desconhecida',
    valor: Number(item.totalServicos ?? 0),
  }))

  if (chartData.length === 0) {
    return <div className="chart-empty">Nenhum dado disponível para serviços por cidade.</div>
  }

  return (
    <div className="chart-panel">
      <ResponsiveContainer width="100%" height={300}>
        <BarChart data={chartData} margin={{ top: 8, right: 16, left: 0, bottom: 40 }}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="name" angle={-20} textAnchor="end" interval={0} />
          <YAxis allowDecimals={false} />
          <Tooltip formatter={(valor) => [Number(valor), 'Serviço']} />
          <Bar dataKey="valor" fill="#FF8C00" radius={[6, 6, 0, 0]} />
        </BarChart>
      </ResponsiveContainer>
    </div>
  )
}

export default ServicosPorCidadeChart
