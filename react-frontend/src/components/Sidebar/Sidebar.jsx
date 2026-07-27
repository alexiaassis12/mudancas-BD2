import './Sidebar.css'

// Seções do Menu do sidebar: (id, label, ícone)
const menuItems = [
  { id: 'dashboard', label: 'Dashboard / Relatórios', icon: '📊' },
  { id: 'cidades', label: 'Cidades', icon: '🗺️' },
  { id: 'empresas', label: 'Empresas', icon: '🏢' },
  { id: 'clientes', label: 'Clientes', icon: '👥' },
  { id: 'funcionarios', label: 'Funcionários', icon: '🧑‍💼' },
  { id: 'servicos', label: 'Serviços', icon: '🔧' },
  { id: 'pedidos', label: 'Pedidos', icon: '🚚' },
]

function Sidebar({ activeItem, onChange }) {
  return (
    <aside className="sidebar">
      <div className="sidebar-brand">
          <img src="..\src\assets\verticelogo.png" alt="Logo do Vértice Express" />
      </div>

      <nav>
        <ul className="menu-list">
          {menuItems.map((item) => (
            <li key={item.id}>
              <button
                type="button"
                className={item.id === activeItem ? 'menu-button active' : 'menu-button'}
                onClick={() => onChange(item.id)}
              >
                <span className="menu-icon">{item.icon}</span>
                <span>{item.label}</span>
              </button>
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  )
}

export default Sidebar;
