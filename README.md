# Documentação Desafio BeBlue

## Tecnologias utilizadas

O projeto é desenvolvido e mantido nas seguintes tecnologias:
* C#
* Dapper
* Asp.net Core 2.0
* Banco de Dados MySql

## Arquitetura e organização do projeto

O projeto está organizado na seguinte arvore de pastas:
* BeBlue
  * /BeBlue.Desafio.Api
  * /BeBlue.Desafio.Entities
  * /BeBlue.Desafio.lib
  * /BeBlue.Desafio.Test
  * /docker 

**BeBlue.Desafio.Api** - Projeto que contém a API. \
**BeBlue.Desafio.Entities** - Projeto do tipo Class Library que possui os modelos e DTOs que serão utilizados. \
**BeBlue.Desafio.lib** - Projeto do tipo Class Library que possui os Services (com as regras de negocio) e os Repository(Para acesso ao BD utilizando Dapper). \
**BeBlue.Desafio.Test** - Projeto do tipo XUnit  que comtém os testes de unidades referente a regra de negocio. \
**docker** - pasta contendo docker-file do banco de dados. Aqui também encontra-se o dump do bd para instalação em **_MySQL_Init_Script/init.sql**.


## Sobre o Banco de Dados

Utilizamos banco de dados MySQL.

Até o momento, temos as seguintes tabelas

Nome da Tabela | Descrição
------------ | -------------
genres 		            | Tabela de opções de genero  musicais.
dayOfWeek		 		| Tabela de o cadastro de dia da semana.
cashback		 		| Tabela de com o cadastro de porcentagem de cashback do genero musical x Dia da semana
tracks		            | Tabela de catalogo de musicas dispponiel a venda.
sales				    | Tabela com as vendas realizadas.
saleItems			    | Tabela com as tracks de cada venda.


### Criando a base local para desenvolvimento
Para criar uma base local e utilizar para desenvolvimento, temos um arquivo chamado **init.sql** em **docker/_MySQL_Init_Script**.
Utiizar o comando **docker-compose -f docker\docker-compose.yml -p dev up -d** para iniciar a intancia do MySQL, o usuario do mySQL é **root** e a senha é **123456**, o container estara rodando na porta **3306**(Porta Padrão), conforme o arquivo **docker\docker-compose.yml**.
Ele irá criar um container MySQL contendo o database **BeBlue**
Para executar o MySQL basta executar o seguinte comando **mysql --protocol=tcp -u root -p** e após isso digitar a senha **123456**
Basta executar o arquivo chamado **init.sql** em **docker/_MySQL_Init_Script** e automaticamente irá criar a base localmente.
Para destruir o container basta executar o comando **docker-compose -f documents\database\docker\docker-compose.yml -p dev down -v**

**IMPORTANTE:** Verificar se a string de conexão está correta no appsettings da aplicação;




## Sobre a Lib

O projeto LIB, foi construido e está organizado em Services e Repository. 

O objetivo é de simplicar as camada de serviço e repository de acordo com a regra de negocio da BeBlue 
e injetando os services nas controllers do **BeBlue.Desafio.Api** conforme a necessidade.

Alem da services, a LIB possui o **Repository**.
O **Repository** tem como objetivo realizar a iteração entre a camada service e o banco de dados.


## Sobre a Entities
As **Entities** tem como objetivo de ler e escrever no banco de dados um objeto relacional.

**DTO** é utilizada para transferir/expor dos dados da **Entities** para a camada de aplicação.



### Executando a aplicação pela primeira vez
Para executar a aplicação pela primeira vez precisa ir até a pasta **BeBlue.Desafio.Api** e executar os seguintes comandos:
* **dotnet restore** - Para restaurar as dlls da lib e da aplicação;

E em uma instancia separada executar
* **dotnet build** - Para construir
* **dotnet run** - Para executar o webapi.
A webapi será escutada o endereço  **http://localhost:5000**
Toda a documentação dos endpoints estará disponivel no **swagger** através do endereçõ **http://localhost:5000/index.html**


### Realizar seed do catalogo de tracks
Para realizar o Seed do catalogo de musicas realize um **PUT** no endoint **/api/Spotify/seed**