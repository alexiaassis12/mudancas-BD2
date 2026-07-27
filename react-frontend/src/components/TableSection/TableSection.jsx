import React from 'react'
import DataTable from '../DataTable/DataTable'
import CreateRowPanel from './CreateRowPanel'
import { useTableSectionData } from './useTableSectionData'
import './TableSection.css'

function TableSection({ sectionId, title, description }) {
  const {
    rows,
    columns,
    loading,
    error,
    createFields,
    createData,
    setCreateData,
    creating,
    createError,
    editError,
    onCreate,
    onDelete,
    onSave,
  } = useTableSectionData(sectionId)

  return (
    <section className="page-view">
      <header className="page-header">
        <div>
          <p className="eyebrow">Gerenciar</p>
          <h1>{title}</h1>
          <p className="page-description">{description}</p>
        </div>
      </header>

      {createFields.length > 0 && (
        <CreateRowPanel
          createFields={createFields}
          createData={createData}
          setCreateData={setCreateData}
          onCreate={onCreate}
          creating={creating}
          createError={createError}
        />
      )}

      {editError && <p className="edit-row-error">{editError}</p>}

      {loading ? (
        <p>Carregando...</p>
      ) : error ? (
        <p style={{ color: 'red' }}>Erro: {error}</p>
      ) : (
        <DataTable columns={columns} rows={rows} onDelete={onDelete} onSave={onSave} />
      )}
    </section>
  )
}

export default TableSection
