# Teste SoftPlan

> NETCORE 3.1 + EFCORE + Migrations + Pipelines + CQRS Patterns + AutoMapper + FluentAssertions + Swagger + Oauth2 + XUnit + Moq + AutoFixture

Ambiente de demonstra��o publicado:
	https://mc2tech-softplan-personsapi.azurewebsites.net/swagger
	https://mc2tech-softplan-lawsuitsapi.azurewebsites.net/swagger

Usu�rio e senha para autentica��o:
	Usu�rio: softplan@mc2tech.com.br
	Senha: 2a:WuS"9{GHbj

### Caracter�sticas do projeto:
* Autentica��o utilizando Oauth2 via SSO do Azure AD
* SimpleSoft.Mediator para orquestra��o dos Pipelines
* CI/CD utilizando Azure DevOps
* Aplica��es web utilizando Azure App Services
* Banco de dados utilizando Azure SQL Databases
	
### Pipelines configurados:
* Timeout Pipeline (padr�o de 5s para drop dos comandos)
* Logging Pipeline (pipeline de log de eventos)
* Validation Pipeline (valida��o de objetos, regras por dom�nio)
* Transaction pipeline (encapsulamento de comandos em transa��es)
* Audit Pipeline (pipeline espec�fico para log em banco de dados de auditoria)

### Intregra��o com banco de dados:
* Entity Framework (Mapeamento de objetos relacionais e execu��o de a��es DML)
* Migrations (execu��o de a��es DDL)
* Os databases est�o separados por dom�nio

* Foi-se utilizado do conceito de crosscutting para centraliza��o de estruturas compartilhadas entre projetos, afim de redu��o de duplica��o de c�digo.
* Para isolamento dos dom�nios (Pessoa e processo) foram utilizados ServiceClient onde n�o h� cross reference entre os projetos de dom�nio.
* Para facilita��o do desenvolvimento do teste softplan, foi adotado somente 1 projeto para cada dom�nio, mas para um sistema real, teria dividido os projetos de API por subprojetos de bibliotecas por tipo de comando (criar, buscar, atualizar).
* A documenta��o foi feita utilizando-se o swagger para facilita��o de valida��o (execu��es) e auto documenta��o das estruturas desenvolvidas de acordo com o c�digo-fonte.
* Na camada de testes, foi utilizado o entity framework com banco de dados inmemory para testes de integra��o com banco de dados e camada de valida��o.


* Na camada de banco de dados, optou-se por utilizar SQL Azure, visando a redu��o do custo.
* Na camada de aplicac�o, foram utilizados AppServices do Azure (gr�tis), mas foram configurados os dockerfiles e docker-compose para execu��o da aplica��o em container.


### Pipeline de CI/CD no Azure DevOps contemplando:
* Valida��o de Pull Requests
* Build por dom�nio e gera��o de artefato para publica��o
* Deploy por dom�nio integrado ao azure onde pode ser conferido os steps de build/test/deploy


#### Para rodar localmente, � necess�rio alterar as configura��es de connection string apontando para uma inst�ncia do sqlserver ou mslocaldb.
	##### instalar o docker
	* https://docs.docker.com/engine/install/
	##### Executar o sql server via docker 
	* https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker


#### Para execu��o local via docker, utilizar os comandos abaixo no diret�rio raiz do projeto:
	* docker-compose up

#### Para a configura��o do Azure AD
	* https://docs.microsoft.com/en-us/azure/app-service/configure-authentication-provider-aad


#### Pendente:
* Finalizar validations no servi�o de pessoas
* Cria��o de queue para servi�o de envio de email
* Cria��o de servi�o de envio de email
	
#### Melhorias
* Cria��o de Slot para deploy por ambiente Dev/QA/Prod
* Criar inst�ncia docker para rodar as apps
* Criar documenta��o de configura��o do AAD (camada de autentica��o)
* Criar camada de abstra��o IRepository, remover DBContexts nas DI
* Criar Frontend (React) para Pessoas
* Criar Frontend (React) para Processos
