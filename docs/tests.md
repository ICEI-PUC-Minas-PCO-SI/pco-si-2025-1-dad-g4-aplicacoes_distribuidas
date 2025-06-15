## Testes

A estratégia de testes do projeto **Ossos do Ofício** foi baseada na verificação detalhada de todos os requisitos funcionais e não funcionais da aplicação. Foram realizados testes manuais e automatizados ao longo do desenvolvimento, com foco em garantir a qualidade das funcionalidades, integração entre microserviços e desempenho da plataforma.

### Estratégia e Tipos de Teste

- **Testes Unitários**  
  Foram realizados testes unitários para validar o comportamento de unidades isoladas de código, como serviços e controladores. Cada microserviço foi testado separadamente para garantir que sua lógica estivesse correta antes da integração.

- **Testes de Integração**  
  Após o desenvolvimento de cada funcionalidade, os serviços foram integrados e testados em conjunto. A comunicação entre os microserviços foi validada usando o Postman e o Swagger, garantindo que as rotas respondessem corretamente às requisições.

- **Testes Funcionais**  
  Todos os requisitos funcionais descritos no documento de especificação foram testados via Postman, com validação de inputs, retornos esperados e códigos de status HTTP. As APIs foram testadas com diferentes cenários e combinações de parâmetros.

- **Testes de Carga (manuais)**  
  Simulações com múltiplas requisições simultâneas foram realizadas para verificar a estabilidade da API sob carga moderada, com foco no requisito **RNF-003**, que exige suporte a pelo menos 100 usuários simultâneos.

- **Testes de Interface e Usabilidade**  
  Embora o foco deste ciclo tenha sido o backend, a estrutura da API foi validada quanto à clareza dos dados retornados, status e mensagens de erro, visando garantir uma futura integração fluida com o frontend.

### Ferramentas Utilizadas

- **Postman**: usado para testes manuais dos endpoints da API.
- **Swagger / OpenAPI**: documentação e testes interativos dos endpoints durante o desenvolvimento.
- **.NET CLI e Visual Studio Code**: execução de testes locais e debugging.
- **GitHub**: controle de versão e histórico de alterações no código testado.

### Casos de Teste Criados

Abaixo, estão os principais casos de teste aplicados por requisito:


| Requisito | Caso de Teste | Ferramenta | Resultado Esperado |
|----------|----------------|------------|---------------------|
| RF-001 | Cadastro e login de usuário | Postman | Retorno 201 Created / 200 OK / 401 Unauthorized |
| RF-002 | CRUD de produtos | Postman | Testes de POST, GET, PUT e DELETE com validações de dados |
| RF-003 | Filtro por nome, categoria e preço | Postman | Retorno filtrado correto com 200 OK |
| RF-004 | Adição e remoção de itens no carrinho | Postman | Atualização dinâmica do carrinho e retorno esperado |
| RF-005 | Criação de pagamento | Postman | Registro com status “Pending” e retorno 201 Created |
| RF-007 | Envio de notificações por e-mail | Postman | Retorno 200 OK e simulação de envio |
| RF-010 | Consulta de pagamento por ID | Postman | Retorno 200 OK ou 404 Not Found |
| RF-011 | Visualização do status de pedidos | Postman | Exibição de status corretos com 200 OK |
