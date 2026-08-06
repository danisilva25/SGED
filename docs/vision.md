# SGED - Sistema de Gerenciamento Escolar Digital

## Visão

O SGED (Sistema de Gerenciamento Escolar Digital) é uma plataforma moderna para gestão educacional, desenvolvida para centralizar e simplificar os processos administrativos e acadêmicos de instituições de ensino.

O projeto tem como objetivo fornecer uma solução escalável, segura e de fácil utilização para alunos, professores, diretores, secretários, diretorias de ensino e órgãos responsáveis pela administração da educação.

Além da área administrativa, o SGED também disponibiliza um portal público para consulta de informações institucionais, notícias, calendário acadêmico e indicadores das escolas.

---

# Problema

Grande parte dos sistemas de gestão escolar atuais apresenta dificuldades relacionadas à usabilidade, desempenho e escalabilidade, especialmente em períodos de alta utilização, como o lançamento simultâneo de notas e frequências por centenas de professores.

Além disso, muitas funcionalidades são fortemente acopladas, dificultando a evolução do sistema e a introdução de novas tecnologias.

O SGED nasce com o propósito de evoluir continuamente, priorizando simplicidade nas primeiras versões e adotando arquiteturas mais robustas conforme novas necessidades surgirem.

---

# Objetivos

- Centralizar a gestão acadêmica e administrativa.
- Simplificar o lançamento de notas e frequências.
- Melhorar a experiência dos usuários.
- Disponibilizar informações públicas das instituições de ensino.
- Evoluir continuamente sem comprometer a estabilidade do sistema.
- Demonstrar boas práticas de engenharia de software.

---

# Usuários

O SGED atende diferentes perfis de usuários:

## Secretaria da Educação

Responsável pela administração global do sistema.

Pode gerenciar diretorias de ensino, usuários administrativos e configurações gerais.

## Diretoria de Ensino

Responsável pela administração das escolas pertencentes à sua diretoria.

## Escola

Responsável pela administração local da instituição.

Pode gerenciar professores, alunos, turmas e demais informações acadêmicas.

## Professor

Responsável pelo gerenciamento das atividades acadêmicas, lançamento de notas, frequência e acompanhamento das turmas.

## Aluno

Pode consultar histórico escolar, notas, frequência, atividades e calendário.

## Responsável

Pode acompanhar o desempenho acadêmico do aluno.

## Público

Pode acessar o portal institucional para consultar notícias, informações das escolas, calendário e demais conteúdos públicos.

---

# Módulos

O SGED será dividido em módulos independentes, permitindo evolução contínua da plataforma.

Inicialmente serão desenvolvidos:

- Administração
- Acadêmico
- Autenticação

Futuramente poderão ser adicionados:

- Portal Público
- Comunicação
- Observabilidade
- Relatórios
- Financeiro
- Biblioteca
- Transporte Escolar

---

# Princípios

Durante todo o desenvolvimento do SGED serão seguidos os seguintes princípios:

- Simplicidade antes da complexidade.
- Evolução incremental.
- Código limpo.
- Arquitetura modular.
- Segurança.
- Escalabilidade.
- Observabilidade.
- Baixo acoplamento.
- Alta coesão.

---

# Evolução do Projeto

O SGED será desenvolvido por versões incrementais.

Cada versão deverá representar uma aplicação funcional, adicionando novas capacidades sem comprometer as funcionalidades existentes.

A evolução técnica da plataforma será documentada separadamente no documento `roadmap.md`.
