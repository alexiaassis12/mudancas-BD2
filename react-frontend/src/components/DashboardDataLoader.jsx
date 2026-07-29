import { useEffect, useState } from 'react'

const API_BASE = 'http://localhost:5065'

function fetchJson(url) {
  return fetch(url).then(async (res) => {
    if (!res.ok) {
      const text = await res.text()
      throw new Error(text || `HTTP ${res.status}`)
    }
    return res.json()
  })
}

function DashboardDataLoader({ children }) {
  const [histogramaServicos, setHistogramaServicos] = useState([])
  const [pagamentosPorCidade, setPagamentosPorCidade] = useState([])
  const [topCidadesValor, setTopCidadesValor] = useState([])
  const [topCidadesServicos, setTopCidadesServicos] = useState([])
  const [topEmpresasServicos, setTopEmpresasServicos] = useState([])
  const [topEmpresasValoresGanhos, setTopEmpresasValoresGanhos] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
  const [debugResponses, setDebugResponses] = useState(null)

  useEffect(() => {
    let active = true
    const load = async () => {
      try {
        // Fetch endpoints sequentially so we can capture per-endpoint errors and debug results
        const debugResponses = {}

        const histograma = await fetchJson(`${API_BASE}/api/Relatorio/histograma-servicos-cidade`).catch((e) => {
          debugResponses.histogramaError = e.message || String(e)
          return []
        })
        // API may return { histograma: [...] } or directly an array
        debugResponses.histograma = histograma
        const histogramaArray = Array.isArray(histograma) ? histograma : (histograma?.histograma ?? [])

        const pagamentos = await fetchJson(`${API_BASE}/api/Relatorio/pagamentos-cidade`).catch((e) => {
          debugResponses.pagamentosError = e.message || String(e)
          return []
        })
        debugResponses.pagamentos = pagamentos

        const topCidadesValorData = await fetchJson(`${API_BASE}/api/Relatorio/top5-cidades-valor-investido`).catch((e) => {
          debugResponses.topCidadesValorError = e.message || String(e)
          return []
        })
        debugResponses.topCidadesValor = topCidadesValorData

        const topCidadesServicosData = await fetchJson(`${API_BASE}/api/Relatorio/top5-cidades-numero-servicos`).catch((e) => {
          debugResponses.topCidadesServicosError = e.message || String(e)
          return []
        })
        debugResponses.topCidadesServicos = topCidadesServicosData

        const topEmpresas = await fetchJson(`${API_BASE}/api/Relatorio/top5-empresas-servicos`).catch((e) => {
          debugResponses.topEmpresasError = e.message || String(e)
          return []
        })
        debugResponses.topEmpresas = topEmpresas

        const topEmpresasValoresGanhosData = await fetchJson(`${API_BASE}/api/Relatorio/top5-empresas-valores-ganhos`).catch((e) => {
          debugResponses.topEmpresasValoresGanhosError = e.message || String(e)
          return []
        })
        debugResponses.topEmpresasValoresGanhos = topEmpresasValoresGanhosData

        if (!active) return
        setHistogramaServicos(histogramaArray)
        setPagamentosPorCidade(pagamentos)
        setTopCidadesValor(topCidadesValorData)
        setTopCidadesServicos(topCidadesServicosData)
        setTopEmpresasServicos(topEmpresas)
        setTopEmpresasValoresGanhos(topEmpresasValoresGanhosData)
        // Expose debug info for quick inspection in browser console during dev
        try { window.__dashboardDebug = debugResponses } catch {}
        setDebugResponses(debugResponses)
      } catch (err) {
        if (!active) return
        setError(err.message || 'Erro ao carregar dados do dashboard')
      } finally {
        if (active) setLoading(false)
      }
    }

    load()
    return () => {
      active = false
    }
  }, [])

  return children({
    histogramaServicos,
    pagamentosPorCidade,
    topCidadesValor,
    topCidadesServicos,
    topEmpresasServicos,
    topEmpresasValoresGanhos,
    debugResponses,
    loading,
    error,
  })
}

export default DashboardDataLoader
