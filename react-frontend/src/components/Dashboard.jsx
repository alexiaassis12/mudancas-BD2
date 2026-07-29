import { useState } from 'react'
import DashboardCard from './DashboardCard'
import DashboardDataLoader from './DashboardDataLoader'
import ServicosPorCidadeChart from './ServicosPorCidadeChart'
import PagamentosPorCidadeChart from './PagamentosPorCidadeChart'

function formatCurrency(value) {
  return Number(value).toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL',
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })
}

function getAggregateValue(items, valueKey) {
  if (!Array.isArray(items)) return 0
  return items.reduce((sum, item) => sum + Number(item[valueKey] || 0), 0)
}

function lowercaseFirstLetter(str) {
  if (!str) return str;
  return str.charAt(0).toLowerCase() + str.slice(1);
}

function getTopItems(items, labelKey, valueKey, limit = 5) {
  if (!Array.isArray(items)) return []

  const sortedItems = [...items]
                          .map((item) => ({
                            label: item[lowercaseFirstLetter(labelKey)],
                            value: Number(item[lowercaseFirstLetter(valueKey)] || 0),
                          }))
                          .filter((item) => item.label)
                          .sort((a, b) => b.value - a.value)
                          .slice(0, limit)

  return sortedItems
}

function Dashboard() {
  const [showDebug, setShowDebug] = useState(false)
  return (
    <DashboardDataLoader>
      {({
        histogramaServicos,
        pagamentosPorCidade,
        topCidadesValor,
        topCidadesServicos,
        topEmpresasServicos,
        topEmpresasValoresGanhos,
        loading,
        error,
        debugResponses,
      }) => {
        // If the endpoint for topCidadesServicos is empty, try to aggregate from the histograma endpoint
        let servicosByCity = Array.isArray(topCidadesServicos) && topCidadesServicos.length > 0 ? topCidadesServicos : []
        if ((!servicosByCity || servicosByCity.length === 0) && Array.isArray(histogramaServicos) && histogramaServicos.length > 0) {
          const map = {}
          for (const it of histogramaServicos) {
            const city = it.NomeCidade ?? it.nomeCidade ?? 'Cidade desconhecida'
            const count = Number(it.TotalServicos ?? it.QuantidadeServicos ?? it.quantidadeServicos ?? 0)
            map[city] = (map[city] || 0) + count
          }
          servicosByCity = Object.keys(map).map((city) => ({ NomeCidade: city, TotalServicos: map[city] }))
          servicosByCity.sort((a, b) => b.TotalServicos - a.TotalServicos)
        }

        const topCidadesInvestimento = getTopItems(topCidadesValor, 'NomeCidade', 'ValorInvestido', 5)
        const topEmpresasServicosRanking = getTopItems(topEmpresasServicos, 'NomeEmpresa', 'TotalServicosSolicitados', 5)
        const topEmpresasGanhosRanking = getTopItems(topEmpresasValoresGanhos, 'NomeEmpresa', 'ValoresGanhos', 5)
        const topCidadesServicosRanking = getTopItems(servicosByCity, 'NomeCidade', 'TotalServicos', 5)

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

            <div className="cards-grid ranking-grid">
              <DashboardCard
                title="Top 5 cidades por valor investido"
                value={loading ? 'Carregando...' : ''}
                subtitle={
                  <ul className="ranking-list">
                    {topCidadesInvestimento.map((item, index) => (
                      <li key={`${item.label}-${index}`}>
                        <span>{index + 1}. {item.label}</span>
                        <strong>{formatCurrency(item.value)}</strong>
                      </li>
                    ))}
                  </ul>
                }
              />
              <DashboardCard
                title="Top 5 cidades por nº de serviços"
                value={loading ? 'Carregando...' : ''}
                subtitle={
                  <ul className="ranking-list">
                    {topCidadesServicosRanking.map((item, index) => (
                      <li key={`${item.label}-${index}`}>
                        <span>{index + 1}. {item.label}</span>
                        <strong>{item.value.toLocaleString('pt-BR')}</strong>
                      </li>
                    ))}
                  </ul>
                }
              />
              <DashboardCard
                title="Top 5 empresas por serviços solicitados"
                value={loading ? 'Carregando...' : ''}
                subtitle={
                  <ul className="ranking-list">
                    {topEmpresasServicosRanking.map((item, index) => (
                      <li key={`${item.label}-${index}`}>
                        <span>{index + 1}. {item.label}</span>
                        <strong>{item.value.toLocaleString('pt-BR')}</strong>
                      </li>
                    ))}
                  </ul>
                }
              />
              <DashboardCard
                title="Top 5 empresas por valores ganhos"
                value={loading ? 'Carregando...' : ''}
                subtitle={
                  <ul className="ranking-list">
                    {topEmpresasGanhosRanking.map((item, index) => (
                      <li key={`${item.label}-${index}`}>
                        <span>{index + 1}. {item.label}</span>
                        <strong>{formatCurrency(item.value)}</strong>
                      </li>
                    ))}
                  </ul>
                }
              />
            </div>

            <div className="report-grid">
              <article className="report-card">
                <h2>Serviços solicitados por cidade</h2>
                {loading ? (
                  <div className="placeholder-panel">Carregando gráfico...</div>
                ) : (
                  <ServicosPorCidadeChart data={servicosByCity} />
                )}
              </article>

              <article className="report-card">
                <h2>Pagamentos de serviços por cidade</h2>
                {loading ? (
                  <div className="placeholder-panel">Carregando gráfico...</div>
                ) : (
                  <PagamentosPorCidadeChart data={pagamentosPorCidade} />
                )}
              </article>
            </div>
          </section>
        )
      }}
    </DashboardDataLoader>
  )
}

export default Dashboard
