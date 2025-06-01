
# Desenvolvimento da Aplicação

## Modelagem da Aplicação
# Estrutura da Aplicação

A aplicação é separada em 2 projetos:

## 1. Projeto API

Este projeto contém a parte de execução da aplicação, incluindo controladores, serviços, dados e arquivos de configuração.

### Pastas do projeto:

- **Connected Services, Dependencies, Properties**:  
  Pastas padrão do Visual Studio para gerenciar pacotes, serviços e propriedades do projeto.

- **Controllers**:  
  Onde estão definidos os controladores (endpoints) que expõem as rotas da API e recebem as requisições HTTP.

- **Data**:  
  Pasta para gerenciar o contexto do banco de dados e configurações de conexão.

- **Migrations**:  
  Contém as migrações do banco de dados geradas pelo Entity Framework Core.

- **Service**:  
  Aqui ficam as regras de negócio e lógica de serviço para abstrair a lógica dos controladores.

- **ViewModel**:  
  Modelos usados para transportar dados entre a API e a interface do usuário (por exemplo, objetos de resposta e requisição).

### Arquivos importantes:

- **API.http**:  
  Arquivo para testar as requisições HTTP diretamente pelo Visual Studio.

- **appsettings.json**:  
  Arquivo de configuração da aplicação, contendo informações como strings de conexão, chaves de API, etc.

- **Program.cs**:  
  Ponto de entrada da aplicação.

---

## 2. Projeto Model

Este projeto contém as entidades e modelos de domínio. Ele abstrai toda a parte de dados e está separado do projeto API para facilitar reuso e manutenção.

### Pastas do projeto:

- **Dependências, Autenticação, Cart, Notification, Order, Payments, Products**:  
  Pastas organizadas para cada domínio do sistema, separando as classes que representam as entidades e a lógica específica de cada área (por exemplo, autenticação, carrinho, pedidos, etc.).

### Arquivos importantes:

- **DefaultValues.cs**:  
  Contém os valores padrões do sistema. Por exemplo, um pedido pode estar em "processamento", "separação", "rota de entrega", "concluído" e "cancelado". Esses valores padrões são armazenados nessa classe.

## Tecnologias Utilizadas

Existem muitas tecnologias diferentes que podem ser usadas para desenvolver APIs Web. A tecnologia certa para o seu projeto dependerá dos seus objetivos, dos seus clientes e dos recursos que a API deve fornecer.

- ASP.NET Core — framework para criação de APIs RESTful, robusto e de alto desempenho.

- C# — linguagem de programação utilizada no desenvolvimento da aplicação.

- Entity Framework Core (EF Core) — ORM (Object-Relational Mapper) para mapeamento das entidades e controle do banco de dados via migrations.

- PostgreSQL — sistema gerenciador de banco de dados relacional, utilizado para persistência dos dados.

- Supabase — plataforma de backend como serviço (BaaS) que hospeda o banco de dados PostgreSQL e oferece ferramentas adicionais.

- Swagger / OpenAPI — ferramenta integrada para documentação e teste dos endpoints da API.

- .NET CLI — interface de linha de comando para gerenciamento, execução, criação de migrations e atualização do banco.

- Npgsql — driver ADO.NET utilizado pelo EF Core para conectar aplicações .NET ao PostgreSQL.

- Git — sistema de controle de versão, essencial para versionamento do código e colaboração entre as equipes.

- Visual Studio / VS Code — ambiente de desenvolvimento (IDE) utilizado para escrever, editar e depurar o código.


## Programação de Funcionalidades

# Implementação do Sistema
---

## ✅ RF-001: Cadastro e Login de Usuário

**Descrição:**  
A aplicação permite que usuários se cadastrem e façam login.

**Artefatos criados:**  
- **API**  
  - `Controllers/AutenticacaoController.cs`  
- **Model**  
  - `Autenticacao`  

**Estruturas de dados:**  
- Entidade `Autenticacao` (armazenamento de dados de login, senha hash, etc.)

**Verificação:**  
- Testar endpoints:  
  - `POST /api/autenticacao/cadastrar`  
  - `POST /api/autenticacao/login`

---
## ✅ RF-002: CRUD de Produtos
**Descrição:**
A aplicação permite criar, ler, atualizar e excluir produtos.

**Artefatos criados:**  
- **API** 
   - `Controllers/ProductsController.cs`
- **Model**  
  - `Model.Products.Products.cs`

**Estruturas de dados:** 
  - Entidade `Products`
    - Id: Identificador único do produto.
    - Nome: Nome do produto (obrigatório, até 100 caracteres).
    - escricao: Descrição do produto (até 500 caracteres).
    - Preco: Preço do produto (valor não negativo).
    - Estoque: Quantidade em estoque (valor não negativo).
**Verificação:**
- Testar endpoints:
  
  -`POST /api/Products`
    -Criar novo produto.
    -Validações automáticas via data annotations ([Required], [StringLength], [Range]).

  -`GET /api/Products`
    - Listar todos os produtos.

  -`GET /api/Products/{id}`
    - Consultar produto pelo ID.

  -`PUT /api/Products/{id}`
    - Atualizar produto existente.

  -`DELETE /api/Products/{id}`
    - Remover produto pelo ID.

---
## ✅ RF-007: Notificações sobre Pedidos e Atualizações

**Descrição:**  
Usuários recebem notificações importantes.

**Artefatos criados:**  
- **API**  
  - `Controllers/NotificationController.cs`  
  - `Service/NotificationService.cs`  
- **Model**  
  - `Notification/Notification.cs`

**Estruturas de dados:**  
- Entidade `Notification` vinculada ao usuário e ao pedido

**Verificação:**  
- Testar endpoints de envio e recebimento de notificações
  
- `POST /api/notification/sendwelcomeemail`  
- `POST /api/notification/sendstatuspurchase`
---

### Requisitos Atendidos

As tabelas que se seguem apresentam os requisitos funcionais e não-funcionais que relacionam o escopo do projeto com os artefatos criados:

### Requisitos Funcionais

|ID    | Descrição do Requisito | Responsável | Artefato Criado |
|------|------------------------|------------|-----------------|
|RF-001| A aplicação deve permitir que o usuário se cadastre e faça login | Barbara | Rota:  |
|RF-002| A aplicação deve permitir que o vendedor cadastre e gerencie novas peças | Leni | Rota: /api/Products (GET, POST), /api/Products/{id} (GET, PUT, DELETE)  |
|RF-007| Notificações sobre Pedidos e Atualizações | Matheus Canuto | Rota: /api/notification/sendwelcomeemail e /api/notification/sendstatuspurchase|



