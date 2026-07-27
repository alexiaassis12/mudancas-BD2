import DashboardCard from './DashboardCard'

const cards = [
  {
    title: 'Serviços por Cidade',
    value: '1.254',
    subtitle: 'Total de serviços realizados no último mês',
  },
  {
    title: 'Pagamento por Cidade',
    value: 'R$ 487.320',
    subtitle: 'Valor total faturado em serviços',
  },
  {
    title: 'Top Cidades Valor',
    value: 'São Paulo',
    subtitle: 'Cidade com maior investimento',
  },
  {
    title: 'Top Empresas Serviços',
    value: 'MudaFácil',
    subtitle: 'Empresa com maior volume de pedidos',
  },
]

function Dashboard() {
  return (
    <section className="dashboard-view">
      <header className="page-header">
        <div>
          <p className="eyebrow">Visão Geral</p>
          <h1>Dashboard e Relatórios</h1>
          <p className="page-description">
            Monitoramento rápido dos principais indicadores do sistema.
          </p>
        </div>
      </header>

      <div className="cards-grid">
        {cards.map((card) => (
          <DashboardCard key={card.title} {...card} />
        ))}
      </div>

      <div className="report-grid">
        <article className="report-card">
          <h2>Serviços solicitados por cidade</h2>
          <div className="report-preview">Gráfico de barras</div>
        </article>
        <article className="report-card">
          <h2>Pagamentos de serviços por cidade</h2>
          <div className="report-preview">Gráfico de barras</div>
        </article>
      </div>
    </section>
  )
}

export default Dashboard
