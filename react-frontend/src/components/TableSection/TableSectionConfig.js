export const endpointMap = {
  cidades: 'Cidade',
  empresas: 'Empresa',
  clientes: 'Cliente',
  funcionarios: 'Funcionario',
  servicos: 'Servico',
  pedidos: 'Pedido',
}

export const createFieldMap = {
  cidades: [
    { key: 'nomeCidade', label: 'Nome da Cidade', type: 'text', required: true, placeholder: 'São Paulo' },
    { key: 'estado', label: 'Estado', type: 'text', required: true, placeholder: 'SP' },
  ],
  empresas: [
    { key: 'nome', label: 'Nome', type: 'text', required: true, placeholder: 'Vertex Express' },
    { key: 'endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Rua das Flores, 123' },
  ],
  clientes: [
    { key: 'cpf', label: 'CPF', type: 'text', required: true, placeholder: '00000000000' },
    { key: 'rg', label: 'RG', type: 'text', required: true, placeholder: '12345678' },
    { key: 'nomeCompleto', label: 'Nome Completo', type: 'text', required: true, placeholder: 'João Silva' },
    { key: 'endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Av. Brasil, 100' },
  ],
  funcionarios: [
    { key: 'cpfFunc', label: 'CPF', type: 'text', required: true, placeholder: '00000000000' },
    { key: 'rgFunc', label: 'RG', type: 'text', required: true, placeholder: '12345678' },
    { key: 'nomeCompleto', label: 'Nome Completo', type: 'text', required: true, placeholder: 'Ana Souza' },
    { key: 'endereco', label: 'Endereço', type: 'text', required: true, placeholder: 'Rua ABC, 456' },
    { key: 'tipoFunc', label: 'Tipo', type: 'text', required: true, placeholder: 'MOTORISTA' },
    { key: 'salario', label: 'Salário', type: 'number', required: true, placeholder: '0.00', min: 0, step: 0.01 },
  ],
  servicos: [
    { key: 'nomeServico', label: 'Nome do Serviço', type: 'text', required: true, placeholder: 'Transporte de carga' },
    { key: 'tipoEspecializacao', label: 'Especialização', type: 'text', required: true, placeholder: 'NENHUM' },
  ],
  pedidos: [
    { key: 'idEmpresa', label: 'ID Empresa', type: 'number', required: true, placeholder: '1' },
    { key: 'codigoCliente', label: 'Código Cliente', type: 'number', required: true, placeholder: '1' },
    { key: 'idCidadeOrigem', label: 'Cidade Origem (ID)', type: 'number', required: false, placeholder: '1' },
    { key: 'enderecoOrigem', label: 'Endereço Origem', type: 'text', required: false, placeholder: 'Rua de Origem' },
    { key: 'idCidadeDestino', label: 'Cidade Destino (ID)', type: 'number', required: false, placeholder: '1' },
    { key: 'enderecoDestino', label: 'Endereço Destino', type: 'text', required: false, placeholder: 'Rua de Destino' },
    { key: 'dataSolicitacao', label: 'Data Solicitação', type: 'date', required: false },
  ],
}

export const updateKeyMap = {
  cidades: 'idCidade',
  empresas: 'idEmpresa',
  clientes: 'codigoCliente',
  funcionarios: 'cpfFunc',
  servicos: 'nomeServico',
}

export const getFieldConfig = (fieldKey) => {
  if (!fieldKey) return undefined
  const normalizedKey = fieldKey.toLowerCase()
  const allFields = Object.values(createFieldMap).flat()
  return allFields.find((field) => field.key.toLowerCase() === normalizedKey)
}

export const formatLabel = (key) => {
  if (!key) return ''

  return key
    .replace(/_/g, ' ')
    .replace(/([a-z])([A-Z])/g, '$1 $2')
    .replace(/(^|\s)\S/g, (t) => t.toUpperCase())
}

export const getDeleteValue = (row, sectionId) => {
  const candidateKeys = [
    updateKeyMap[sectionId],
    'id',
    'id_cidade',
    'id_empresa',
    'id_cliente',
    'id_pedido',
    'id_funcionario',
    'id_servico',
    'cpf',
    'nomeServico',
    'nome_servico',
  ]

  return candidateKeys
    .map((key) => row?.[key] ?? row?.[key?.replace(/^./, (c) => c.toUpperCase())])
    .find((value) => value !== undefined && value !== null && value !== '')
}
