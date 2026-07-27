import React from 'react'
import './DataTable.css'

class DataTable extends React.PureComponent {
  constructor(props) {
    super(props)
    this.state = { deletingRowId: null }
  }

  getRowIdentity = (row) => {
    const candidates = [
      'id',
      'idCidade',
      'id_cidade',
      'idEmpresa',
      'id_empresa',
      'idCliente',
      'id_cliente',
      'idPedido',
      'id_pedido',
      'idFuncionario',
      'id_funcionario',
      'idServico',
      'id_servico',
      'cpf',
      'nomeServico',
      'nome_servico',
    ]

    return candidates
      .map((key) => row?.[key])
      .find((value) => value !== undefined && value !== null && value !== '')
  }

  handleDelete = async (row) => {
    const { onDelete } = this.props
    if (!onDelete) return

    const rowId = this.getRowIdentity(row)
    this.setState({ deletingRowId: rowId })

    try {
      await onDelete(row)
    } finally {
      this.setState({ deletingRowId: null })
    }
  }

  render() {
    const {
      columns = [],
      rows = [],
      emptyMessage = 'Sem dados para exibir',
      onDelete,
      actionsLabel = 'Ações',
    } = this.props

    const showActions = typeof onDelete === 'function'
    const allColumns = showActions ? [...columns, { key: '__actions', label: actionsLabel }] : columns
    const { deletingRowId } = this.state

    return (
      <div className="data-table-panel">
        <div className="data-table-scroll">
          <table className="data-table">
            <thead>
              <tr>
                {allColumns.map((column) => (
                  <th key={column.key}>{column.label}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {rows.length === 0 ? (
                <tr>
                  <td colSpan={allColumns.length} className="empty-row">
                    {emptyMessage}
                  </td>
                </tr>
              ) : (
                rows.map((row, rowIndex) => {
                  const rowId = this.getRowIdentity(row)
                  return (
                    <tr key={rowId ?? rowIndex}>
                      {allColumns.map((column) => {
                        if (column.key === '__actions') {
                          return (
                            <td key={column.key} className="actions-cell">
                              <button
                                type="button"
                                className="delete-row-button"
                                onClick={() => this.handleDelete(row)}
                                disabled={deletingRowId !== null && deletingRowId === rowId}
                                title="Excluir item"
                              >
                                {deletingRowId === rowId ? '...' : '🗑️'}
                              </button>
                            </td>
                          )
                        }

                        return <td key={column.key}>{row[column.key]}</td>
                      })}
                    </tr>
                  )
                })
              )}
            </tbody>
          </table>
        </div>
      </div>
    )
  }
}

export default DataTable
