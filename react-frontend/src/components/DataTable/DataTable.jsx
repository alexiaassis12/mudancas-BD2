import React from 'react'
import './DataTable.css'

class DataTable extends React.PureComponent {
  render() {
    const { columns = [], rows = [], emptyMessage = 'Sem dados para exibir' } = this.props

    return (
      <div className="data-table-panel">
        <div className="data-table-scroll">
          <table className="data-table">
            <thead>
              <tr>
                {columns.map((column) => (
                  <th key={column.key}>{column.label}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {rows.length === 0 ? (
                <tr>
                  <td colSpan={columns.length} className="empty-row">
                    {emptyMessage}
                  </td>
                </tr>
              ) : (
                rows.map((row, rowIndex) => (
                  <tr key={row.id ?? rowIndex}>
                    {columns.map((column) => (
                      <td key={column.key}>{row[column.key]}</td>
                    ))}
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    )
  }
}

export default DataTable
