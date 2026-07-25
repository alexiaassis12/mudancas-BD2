// ==========================================
// CONFIGURAÇÃO DA API
// ==========================================
const API_BASE_URL = 'http://localhost:5000/api'; 

// ==========================================
// INICIALIZAÇÃO E NAVEGAÇÃO
// ==========================================
document.addEventListener('DOMContentLoaded', () => {
    const navButtons = document.querySelectorAll('#nav-menu button');
    const sections = document.querySelectorAll('.view-section');

    // Lógica para trocar de abas
    navButtons.forEach(button => {
        button.addEventListener('click', () => {
            // Remove a classe 'active' de todos
            navButtons.forEach(btn => btn.classList.remove('active'));
            sections.forEach(sec => sec.classList.remove('active'));

            // Adiciona 'active' na aba atual
            button.classList.add('active');
            const targetId = button.getAttribute('data-target');
            
            const targetSection = document.getElementById(targetId);
            if (targetSection) {
                targetSection.classList.add('active');
            } else {
                console.warn(`Seção '${targetId}' não encontrada no HTML.`);
            }

            // Carrega os dados do banco dependendo da aba que foi clicada
            if(targetId === 'dashboard') {
                carregarRelatoriosSeguro();
            } else if (targetId === 'cidades') {
                carregarCidadesSeguro();
            }
        });
    });

    // Carrega o dashboard automaticamente ao abrir a página
    carregarRelatoriosSeguro();
});

// ==========================================
// MÓDULO: DASHBOARD E RELATÓRIOS
// ==========================================
async function carregarRelatoriosSeguro() {
    try {
        await carregarRelatorios();
    } catch (error) {
        console.error("Erro na comunicação com a API:", error);
    }
}

async function carregarRelatorios() {
   
       const resServicos = await fetch(`${API_BASE_URL}/Relatorio/HistogramaServicos`);
       const dadosServicos = await resServicos.json();
       
       const resPagamentos = await fetch(`${API_BASE_URL}/Relatorio/HistogramaPagamentos`);
       const dadosPagamentos = await resPagamentos.json();
       
       const resTopCidadesValor = await fetch(`${API_BASE_URL}/Relatorio/TopCidadesValor`);
       const topCidadesValor = await resTopCidadesValor.json();
       
       const resTopCidadesServicos = await fetch(`${API_BASE_URL}/Relatorio/TopCidadesServicos`);
       const topCidadesServicos = await resTopCidadesServicos.json();
       
       const resTopEmpresasServicos = await fetch(`${API_BASE_URL}/Relatorio/TopEmpresasServicos`);
       const topEmpresasServicos = await resTopEmpresasServicos.json();
       
       const resTopEmpresasValores = await fetch(`${API_BASE_URL}/Relatorio/TopEmpresasValores`);
       const topEmpresasValores = await resTopEmpresasValores.json();


    /* --- MOCK TEMPORÁRIO (Apagar isso quando ativar os fetchs acima) ---
    const dadosServicos = { labels: ["São Paulo", "Campinas", "Rio de Janeiro"], values: [120, 85, 45] };
    const dadosPagamentos = { labels: ["São Paulo", "Campinas", "Rio de Janeiro"], values: [50000, 32000, 15000] };
    const topCidadesValor = [{ NomeCidade: 'São Paulo', ValorInvestido: 50000 }, { NomeCidade: 'Campinas', ValorInvestido: 32000 }];
    const topCidadesServicos = [{ NomeCidade: 'São Paulo', TotalServicos: 120 }, { NomeCidade: 'Campinas', TotalServicos: 85 }];
    const topEmpresasServicos = [{ NomeEmpresa: 'MudaFácil', TotalServicosSolicitados: 40 }, { NomeEmpresa: 'RápidaMudança', TotalServicosSolicitados: 35 }];
    const topEmpresasValores = [{ NomeEmpresa: 'MudaFácil', ValoresGanhos: 15000 }, { NomeEmpresa: 'RápidaMudança', ValoresGanhos: 12000 }];
    --------------------------------------------------------------*/

    // Processa os Gráficos
    renderizarHistograma('chartServicosCidade', 'Qtd de Serviços', dadosServicos, '#FF8C00');
    renderizarHistograma('chartPagamentosCidade', 'Valor Total (R$)', dadosPagamentos, '#0A192F');

    // Processa as Listas (Top 5)
    preencherListaDinamica('topCidadesValor', topCidadesValor);
    preencherListaDinamica('topCidadesServicos', topCidadesServicos);
    preencherListaDinamica('topEmpresasServicos', topEmpresasServicos);
    preencherListaDinamica('topEmpresasValores', topEmpresasValores);
}

// ==========================================
// MÓDULO: CRUD (EXEMPLO CIDADES)
// ==========================================
async function carregarCidadesSeguro() {
    try {
        await carregarCidades();
    } catch (error) {
        console.error("Erro ao carregar lista de cidades:", error);
    }
}

async function carregarCidades() {
    
       const response = await fetch(`${API_BASE_URL}/Cidade`);
       const cidades = await response.json();
    


    const tbody = document.querySelector('#table-cidades tbody');
    if (!tbody) return;
    
    tbody.innerHTML = '';
    
    cidades.forEach(cidade => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${cidade.id_cidade}</td>
            <td>${cidade.nome_cidade}</td>
            <td>${cidade.estado}</td>
            <td><button onclick="deletarCidade(${cidade.id_cidade})" style="color:red; background:none; border:none; cursor:pointer;">Excluir</button></td>
        `;
        tbody.appendChild(tr);
    });
}

// Escuta o evento de enviar formulário para inserir (POST) no banco de dados
document.getElementById('form-cidades')?.addEventListener('submit', async (e) => {
    e.preventDefault();
    const nome_cidade = document.getElementById('cidade-nome').value;
    const estado = document.getElementById('cidade-estado').value;

    try {
        // Envia para o C# salvar no MySQL
        await fetch(`${API_BASE_URL}/Cidade`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome_cidade, estado })
        });
        
        document.getElementById('form-cidades').reset(); // Limpa o formulário
        carregarCidadesSeguro(); // Recarrega a tabela para mostrar o novo dado
    } catch (error) {
        console.error("Erro ao salvar cidade:", error);
        alert("Cadastrado localmente (Falso)! Conecte o C# para salvar no Banco de Dados.");
    }
});

async function deletarCidade(id) {
    if(confirm('Tem certeza que deseja excluir esta cidade?')) {
        try {
            await fetch(`${API_BASE_URL}/Cidade/${id}`, { method: 'DELETE' });
            carregarCidadesSeguro();
        } catch (error) {
            console.error("Erro ao deletar:", error);
        }
    }
}

// ==========================================
// FUNÇÕES UTILITÁRIAS (RENDERIZAÇÃO)
// ==========================================

// Renderiza Gráficos usando Chart.js
function renderizarHistograma(canvasId, label, data, color) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return; 
    
    const ctx = canvas.getContext('2d');
    
    // Evita sobreposição de gráficos se a aba for clicada várias vezes
    if(window[canvasId] instanceof Chart) { window[canvasId].destroy(); }

    window[canvasId] = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: data.labels, 
            datasets: [{
                label: label,
                data: data.values, 
                backgroundColor: color,
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: { y: { beginAtZero: true } }
        }
    });
}

// Processa as Views do SQL e transforma em HTML
function preencherListaDinamica(elementId, items) {
    const ul = document.getElementById(elementId);
    if (!ul) return; 
    
    ul.innerHTML = '';
    items.forEach(item => {
        const li = document.createElement('li');
        
        // Verifica dinamicamente qual é o nome do item (Cidade ou Empresa) vindo do C#
        const nome = item.NomeCidade || item.nomeCidade || item.NomeEmpresa || item.nomeEmpresa || item.nome || 'Desconhecido';
        
        // Verifica qual é o valor e formata (Dinheiro vs Quantidade de Serviços)
        let valorBruto = item.ValorInvestido || item.valorInvestido || item.ValoresGanhos || item.valoresGanhos;
        let textoValor = '';

        if (valorBruto !== undefined) {
            // Se achou um valor monetário, formata como Real (R$)
            textoValor = `R$ ${parseFloat(valorBruto).toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`;
        } else {
            // Se não for dinheiro, busca a quantidade de serviços
            const qtd = item.TotalServicos || item.totalServicos || item.TotalServicosSolicitados || item.totalServicosSolicitados || item.valor || 0;
            textoValor = `${qtd} serviços`;
        }

        li.innerHTML = `<span>${nome}</span> <strong>${textoValor}</strong>`;
        ul.appendChild(li);
    });
}