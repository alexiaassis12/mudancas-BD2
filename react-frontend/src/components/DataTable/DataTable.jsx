import React from 'react'
import './DataTable.css'

class DataTable extends React.PureComponent {
  constructor(props) {
    super(props)
    this.state = { deletingRowId: null, editingRowId: null, editedData: {} }
  }

  getRowIdentity = (row) => {
    const candidates = [
      'id',
      'Id',
      'idCidade',
      'IdCidade',
      'id_cidade',
      'idEmpresa',
      'IdEmpresa',
      'id_empresa',
      'idCliente',
      'IdCliente',
      'id_cliente',
      'codigoCliente',
      'CodigoCliente',
      'idPedido',
      'IdPedido',
      'id_pedido',
      'codigoPedido',
      'CodigoPedido',
      'idFuncionario',
      'IdFuncionario',
      'id_funcionario',
      'idServico',
      'IdServico',
      'id_servico',
      'cpf',
      'Cpf',
      'cpfFunc',
      'CpfFunc',
      'nomeServico',
      'NomeServico',
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
      onSave,
      actionsLabel = 'Ações',
    } = this.props

    const showActions = typeof onDelete === 'function' || typeof onSave === 'function'
    const allColumns = showActions ? [...columns, { key: '__actions', label: actionsLabel }] : columns
    const { deletingRowId } = this.state
    const { editingRowId, editedData = {} } = this.state

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
                  const isEditing = editingRowId === rowId
                  return (
                    <tr key={rowId ?? rowIndex}>
                      {allColumns.map((column) => {
                        if (column.key === '__actions') {
                          return (
                            <td key={column.key} className="actions-cell">
                              {onSave && (
                                <button
                                  type="button"
                                  className="edit-row-button"
                                  onClick={() => {
                                    if (isEditing) {
                                      this.handleSave(row)
                                    } else {
                                      // don't allow entering edit mode while another save/delete is in progress
                                      if (deletingRowId) return
                                      this.setState({ editingRowId: rowId, editedData: { ...row } })
                                    }
                                  }}
                                  title={isEditing ? 'Salvar edição' : 'Editar item'}
                                  disabled={deletingRowId !== null && deletingRowId === rowId}
                                >
                                  {isEditing ? (deletingRowId === rowId ? '...' : '💾') : '✏️'}
                                </button>
                              )}
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

                        if (isEditing && column.key !== '__actions') {
                          return (
                            <td key={column.key}>
                              <input
                                type="text"
                                value={editedData[column.key] ?? ''}
                                onChange={(event) =>
                                  this.setState((prevState) => ({
                                    editedData: {
                                      ...prevState.editedData,
                                      [column.key]: event.target.value,
                                    },
                                  }))
                                }
                              />
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

  handleSave = async (row) => {
    const { onSave } = this.props
    const rowId = this.getRowIdentity(row)
    const { editedData } = this.state
    if (!onSave || !rowId) return

    this.setState({ deletingRowId: rowId })
    try {
      await onSave(row, editedData)
    } catch (err) {
      // no-op: error handling can be managed in parent
    } finally {
      this.setState({ deletingRowId: null, editingRowId: null, editedData: {} })
    }
  }
}

export default DataTable
