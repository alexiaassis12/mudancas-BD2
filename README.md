# 🚚 Sistema de Mudanças - Backend & Banco de Dados

Repositório oficial do projeto de Banco de Dados. Contém os scripts de criação do banco **MySQL** e a **API RESTful em C# (.NET 8)** para gerenciamento e relatórios.

---

## 📋 Pré-requisitos (O que instalar antes de rodar)

Para clonar e executar este projeto em qualquer computador, é necessário ter as seguintes ferramentas instaladas:

1. **.NET 8.0 SDK (Software Development Kit):**
   * Necessário para compilar e executar a API em C#.
   * 🔗 [Download do .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) *(Escolha o instalador para seu sistema operacional - Windows, macOS ou Linux)*.
   * *Para verificar se já possui instalado, rode no terminal:* `dotnet --version` (deve retornar algo como `8.0.x`).

2. **Servidor MySQL Server (Porta padrão 3306):**
   * Necessário para rodar a base de dados localmente.
   * 🔗 [Download do MySQL Installer for Windows](https://dev.mysql.com/downloads/installer/) *(Recomendado instalar o MySQL Server + MySQL Workbench)*.

3. **Cliente para MySQL (Opcional, mas recomendado):**
   * Interface gráfica para colar e executar os scripts do banco:
     * [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) (já incluso no instalador do MySQL) ou
     * [DBeaver](https://dbeaver.io/download/).

4. **Git:**
   * Para clonar o repositório.
   * 🔗 [Download do Git](https://git-scm.com/downloads).

---

## 📁 Estrutura do Repositório

**`mudancas-BD2/
│
├── dataset/             
│   ├── script.txt       
│   └── views.txt        
│
├── backend/             
│   ├── Controllers/
│   ├── DTOs/
│   ├── Repositories/
│   ├── Services/
│   ├── Program.cs
│   ├── Mudanca.csproj
│   └── appsettings.json
│
└── README.md`**         

---

## 🛠️ 1. Configuração do Banco de Dados (MySQL)

Antes de executar a API, certifique-se de que o seu serviço do MySQL está rodando localmente (porta padrão `3306`).

Os scripts SQL estão organizados na pasta `dataset/`. Execute-os na ordem indicada abaixo através do seu cliente MySQL (MySQL Workbench, DBeaver ou terminal):

1. **`dataset/script.txt`**: Cria o banco de dados `mudancas`, a estrutura de todas as tabelas, os `TRIGGERs` de validação/preços e insere o dataset inicial de testes.
2. **`dataset/views.txt`**: Cria as Views de relatórios utilizadas pelos endpoints de ranking e estatísticas da API.

---

## 🚀 2. Como Configurar e Rodar a API Backend

A API foi desenvolvida em C# (.NET 8) e está localizada na subpasta `backend`.

### Passo 1: Navegar até a pasta da API
Se você abriu o terminal na raiz do projeto clonado, entre na pasta do backend:
bash
**`cd backend`**

### Passo 2: Configurar a Senha do Banco
Abra o arquivo backend/appsettings.json em seu editor de texto e insira a sua senha local do MySQL no campo Pwd:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=mudancas;Uid=root;Pwd=SUA_SENHA_AQUI;"
  }
}

### Passo 3: Compilar e Executar
Ainda dentro da pasta backend, execute os comandos no terminal:
# 1. Compila e verifica se há erros no projeto
dotnet build

# 2. Executa a API
dotnet run

Abertura Automática: Ao executar dotnet run, o navegador abrirá automaticamente a interface do Swagger em http://localhost:5065
