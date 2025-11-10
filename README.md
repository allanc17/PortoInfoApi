• PortoInfoApi: Web API em Camadas com Segurança JWT



Este é um projeto de \*\*Web API RESTful\*\* construído em \*\*ASP.NET Core (.NET 8.0)\*\* que demonstra a aplicação prática da \*\*Arquitetura em Camadas\*\* (Controller, Service, Repository) com um foco na segurança.



• Arquitetura e Funcionalidades



O projeto garante a separação de responsabilidades (SOLID), onde:

\* \*\*Controller:\*\* Lida apenas com requisições e respostas HTTP.

\* \*\*Service:\*\* Contém toda a lógica de negócios, validação e segurança.

\* \*\*Repository:\*\* Isola a comunicação assíncrona com o banco de dados (usando \*\*Entity Framework Core In Memory\*\* para testes).



\*\*Funcionalidades de Segurança:\*\*

1\.  \*\*Autenticação JWT:\*\* Geração e validação de \*\*JSON Web Tokens\*\* para sessões seguras.

2\.  \*\*Autorização:\*\* Uso do atributo `\\\\\\\[Authorize(Roles="...")]` para proteger endpoints.

3\.  \*\*Hashing de Senhas:\*\* Utilização do \*\*BCrypt.Net-Core\*\* para criptografia segura de credenciais.



• Como Executar



O projeto utiliza o servidor Kestrel padrão e é testado via Swagger UI.



1\.  \*\*Pré-requisitos:\*\* .NET 8 SDK.

2\.  \*\*Inicialização:\*\* Execute o projeto no Visual Studio, garantindo que o perfil \*\*`http`\*\* esteja selecionado.

3\.  \*\*Acesso:\*\* Com o servidor rodando, acesse a interface de testes no seu navegador: `http://localhost:5136/swagger/index.html`



• Fluxo de Teste (Swagger)



Para validar a segurança da arquitetura:



1\.  \*\*Registro:\*\* Use o `POST /register` para criar um usuário (`role: "Admin"`). O retorno deve ser \*\*201 Created\*\*.

2\.  \*\*Login:\*\* Use o `POST /login` para receber o \*\*Token JWT\*\*. O retorno deve ser \*\*200 OK\*\*.

3\.  \*\*Acesso Protegido:\*\* Clique em \*\*"Authorize"\*\* no Swagger, insira o token (precedido por `Bearer `) e execute o `GET /users`. O retorno deve ser \*\*200 OK\*\* (confirmando a validação do token e da Role).



&nbsp;• Boas Práticas e Segurança



A chave secreta JWT está no `appsettings.json` para fins de demonstração. Em produção, esta chave \*\*deve ser movida\*\* para \*\*Variáveis de Ambiente\*\* ou um serviço de segredos (como Azure Key Vault) e removida do código-fonte público.

