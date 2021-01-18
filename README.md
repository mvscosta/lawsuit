# Teste SoftPlan

> NETCORE 3.1 + EFCORE + Migrations + Pipelines + CQRS Patterns + AutoMapper + FluentAssertions + Swagger + Oauth2 + XUnit + Moq + AutoFixture

Ambiente de demonstração publicado:
	https://mc2tech-softplan-personsapi.azurewebsites.net/swagger
	https://mc2tech-softplan-lawsuitsapi.azurewebsites.net/swagger

Usuário e senha para autenticação:
	Usuário: softplan@mc2tech.com.br
	Senha: 2a:WuS"9{GHbj

### Características do projeto:
* Autenticação utilizando Oauth2 via SSO do Azure AD
* SimpleSoft.Mediator para orquestração dos Pipelines
* CI/CD utilizando Azure DevOps
* Aplicações web utilizando Azure App Services
* Banco de dados utilizando Azure SQL Databases
	
### Pipelines configurados:
* Timeout Pipeline (padrão de 5s para drop dos comandos)
* Logging Pipeline (pipeline de log de eventos)
* Validation Pipeline (validação de objetos, regras por domínio)
* Transaction pipeline (encapsulamento de comandos em transações)
* Audit Pipeline (pipeline específico para log em banco de dados de auditoria)

### Intregração com banco de dados:
* Entity Framework (Mapeamento de objetos relacionais e execução de ações DML)
* Migrations (execução de ações DDL)
* Os databases estão separados por domínio

* Foi-se utilizado do conceito de crosscutting para centralização de estruturas compartilhadas entre projetos, afim de redução de duplicação de código.
* Para isolamento dos domínios (Pessoa e processo) foram utilizados ServiceClient onde não há cross reference entre os projetos de domínio.
* Para facilitação do desenvolvimento do teste softplan, foi adotado somente 1 projeto para cada domínio, mas para um sistema real, teria dividido os projetos de API por subprojetos de bibliotecas por tipo de comando (criar, buscar, atualizar).
* A documentação foi feita utilizando-se o swagger para facilitação de validação (execuções) e auto documentação das estruturas desenvolvidas de acordo com o código-fonte.
* Na camada de testes, foi utilizado o entity framework com banco de dados inmemory para testes de integração com banco de dados e camada de validação.


* Na camada de banco de dados, optou-se por utilizar SQL Azure, visando a redução do custo.
* Na camada de aplicacão, foram utilizados AppServices do Azure (grátis), mas foram configurados os dockerfiles e docker-compose para execução da aplicação em container.


### Pipeline de CI/CD no Azure DevOps contemplando:
* Validação de Pull Requests
* Build por domínio e geração de artefato para publicação
* Deploy por domínio integrado ao azure onde pode ser conferido os steps de build/test/deploy


#### Para rodar localmente, é necessário alterar as configurações de connection string apontando para uma instância do sqlserver ou mslocaldb.
	##### instalar o docker
	* https://docs.docker.com/engine/install/
	##### Executar o sql server via docker 
	* https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker


#### Para execução local via docker, utilizar os comandos abaixo no diretório raiz do projeto:
	* docker-compose up

#### Para a configuração do Azure AD
	* https://docs.microsoft.com/en-us/azure/app-service/configure-authentication-provider-aad


#### Pendente:
* Finalizar validations no serviço de pessoas
* Criação de queue para serviço de envio de email
* Criação de serviço de envio de email
	
#### Melhorias
* Criação de Slot para deploy por ambiente Dev/QA/Prod
* Criar instância docker para rodar as apps
* Criar documentação de configuração do AAD (camada de autenticação)
* Criar camada de abstração IRepository, remover DBContexts nas DI
* Criar Frontend (React) para Pessoas
* Criar Frontend (React) para Processos
