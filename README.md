# Gerenciador de Projetos

## Visão Geral
O Gerenciador de Projetos é uma aplicação desenvolvida em ASP.NET 7 utilizando a arquitetura Clean Architecture. Ele permite aos usuários organizar e monitorar suas tarefas, bem como colaborar com colegas de equipe. A aplicação foi executada no Docker e utiliza Entity Framework Core para acesso a banco de dados SQL Server e AutoMapper para mapeamento de objetos e segue os princípios de arquitetura limpa para manter um código organizado e modular.
## Funcionalidades Principais
*TASKMANAGEMENT*
- **Listagem de Projetos:** Lista todos os projetos do usuário.
- **Visualização de Tarefas:** Permite visualizar todas as tarefas de um projeto específico.
- **Visualização de Tarefa:** Permite visualizar os detalhes de uma tarefa específica.
- **Listagem de Histórico de Alterações:** Lista todas alterações registrados.
- **Visualização de Histórico de Alterações:** Lista todas alterações de uma tarefa específica.
- **Visualização de Comentários:** Lista todos os comentários realizados em uma tarefa específica.
- **Atualização de Tarefas:** Atualiza o status ou detalhes de uma tarefa.
- **Criação de Projetos:** Permite criar um novo projeto.
- **Adição de Tarefas:** Adiciona uma nova tarefa a um projeto existente.
- **Criação de Comentários:** Adiciona um novo comentário em uma tarefa específica.
- **Remoção de Tarefas:** Remove uma tarefa de um projeto.
- **Remoção de Projetos:** Remove um projeto que não possua tarefas ativas.
  
*USER*
- **Listagem de Usuários:** Lista todos os usuários.
- **Visualização de Usuário:** Visualiza todos os detalhes de um usuário específico.
- **Criação de Usuário:** Cria um novo usuário.
- **Remoção de Usuário:** Remove um usuário.
  
*PERFORMANCE*
- **Geração de um Relatórios de Desempenho:** Gera um relatório para gerentes com o desempenho médio de tarefas concluídas por usuários em 30 dias.

## Como Rodar o Projeto
1. Clone este repositório para o seu ambiente local.
2. Abra o projeto em sua IDE preferida ou em um terminal.
3. Certifique-se de ter o Docker instalado em sua máquina.
4. Para rodar o projeto digite no terminal: docker-compose up --build
5. O Projeto foi configurado para rodar no Swagger para facilitar a visualização: http://localhost:8081/swagger/index.html
6. É possível acessar as API's através do uso do Postman com seus respectivos endpoints.
![image](https://github.com/EduXimenes/TaskManagement/assets/53613863/3e2dd2d3-4653-485e-ae4c-970053c3aa3a)


# Fase 2 - Refinamento

### Questionamentos visando o refinamento para futuras implementações ou melhorias.

- A aplicação está considerando os aumentos de número de usuários e projetos/tarefas ?
- Existem preocupações com desempenho ?
- Existem áreas da aplicação que o usuário considera dificil ou confuso de utilizar? Como entradas de dados ou respostas com dados excessivos.
- Os relatórios gerados para gerentes estão atendendo as expectativas?
- Seria interessante incluir mais dados neste relatório ou criar outros tipos de relatórios?

# Fase 3 - Final

### Melhorarias no projeto

- Criar uma documentação abrangente para desenvolvedores, usuários e administradores do sistema, incluindo guias de uso, API, arquitetura de sistema e boas práticas de desenvolvimento.
- Para melhoria do projeto seria interessante adicionar recursos robustos de monitoramento e logging para acompanhar o desempenho do aplicativo, identificar problemas rapidamente e tomar medidas direcionadas para melhorias.
- Seria de grande importância avaliar a possibilidade de migrar para uma arquitetura de microsserviços para facilitar a escalabilidade, manutenção e implantação de componentes individuais do aplicativo.
