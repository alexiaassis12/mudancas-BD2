import { useState } from 'react'
import './App.css'
import Sidebar from './components/Sidebar/Sidebar'
import Dashboard from './components/Dashboard'
import TableSection from './components/TableSection/TableSection'

const views = {
  dashboard: <Dashboard />,

  cidades: (
    <TableSection
      sectionId="cidades"
      title="Cidades"
      description="Crie, edite e remova cidades no sistema."
    />
  ),

  empresas: (
    <TableSection
      sectionId="empresas"
      title="Empresas"
      description="Crie, edite e remova empresas no sistema."
    />
  ),

  clientes: (
    <TableSection
      sectionId="clientes"
      title="Clientes"
      description="Crie, edite e remova clientes no sistema."
    />
  ),

  funcionarios: (
    <TableSection
      sectionId="funcionarios"
      title="Funcionarios"
      description="Crie, edite e remova funcionários no sistema."
    />
  ),

  servicos: (
    <TableSection
      sectionId="servicos"
      title="Servicos"
      description="Crie, edite e remova serviços no sistema."
    />
  ),

  pedidos: (
    <TableSection
      sectionId="pedidos"
      title="Pedidos"
      description="Crie, edite e remova pedidos no sistema."
    />
  ),
}

function App() {
  const [activeView, setActiveView] = useState('dashboard')

  return (
    <div className="app-shell">
      <Sidebar activeItem={activeView} onChange={setActiveView} />
      <main className="app-content">{views[activeView]}</main>
    </div>
  )
}

export default App
