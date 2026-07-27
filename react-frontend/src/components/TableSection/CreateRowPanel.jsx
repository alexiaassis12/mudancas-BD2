import React from 'react'

function CreateRowPanel({ createFields, createData, setCreateData, onCreate, creating, createError }) {
  return (
    <div className="create-row-panel">
      <button type="button" className="create-row-button" onClick={onCreate} disabled={creating}>
        {creating ? 'CRIANDO...' : 'CRIAR'}
      </button>

      <div className="create-row-inputs">
        {createFields.map((field) => (
          <label className="create-row-field" key={field.key}>
            <span>{field.label}</span>
            <input
              type={field.type}
              value={createData[field.key] ?? ''}
              placeholder={field.placeholder || ''}
              min={field.min}
              step={field.step}
              onChange={(event) =>
                setCreateData((prev) => ({
                  ...prev,
                  [field.key]: event.target.value,
                }))
              }
            />
          </label>
        ))}
      </div>

      {createError && <p className="create-row-error">{createError}</p>}
    </div>
  )
}

export default CreateRowPanel
