# Roadmap

Este documento descreve a evolução planejada do SGED (Sistema de Gerenciamento Escolar Digital).

O projeto será desenvolvido de forma incremental, onde cada versão representa uma aplicação funcional e estável. Novas funcionalidades serão adicionadas gradualmente, mantendo compatibilidade e qualidade do sistema.

---

# Versão 1.0 - Fundação

## Objetivo

Construir a primeira versão funcional do SGED, contemplando os principais cadastros acadêmicos e administrativos.

## Funcionalidades

### Autenticação

- Login simples.
- Controle básico de acesso.

### Cadastros

- Secretaria da Educação.
- Diretoria de Ensino.
- Escola.
- Diretor.
- Secretário Escolar.
- Professor.
- Aluno.
- Turma.
- Disciplina.
- Atividade.

### Acadêmico

- Lançamento de notas.
- Consulta de notas.

### Tecnologias

- .NET
- PostgreSQL
- React

---

# Versão 2.0 - Segurança e Administração

## Objetivo

Adicionar mecanismos de autenticação e autorização mais robustos.

## Funcionalidades

- Refresh Token.
- Recuperação de senha.
- Confirmação de e-mail.
- Perfis de acesso.
- Permissões.
- Hierarquia de usuários.
- Auditoria inicial.

---

# Versão 3.0 - Portal Público e Experiência do Usuário

## Objetivo

Melhorar significativamente a experiência dos usuários e disponibilizar informações públicas.

## Funcionalidades

### Portal Público

- Notícias.
- Calendário.
- Informações das escolas.
- Consulta pública.

### Interface

- Novo Dashboard.
- Melhorias de UX.
- Layout responsivo.
- Componentes reutilizáveis.

---

# Versão 4.0 - Escalabilidade

## Objetivo

Preparar o SGED para suportar maior volume de usuários e processamento.

## Funcionalidades

- Arquitetura Orientada a Eventos.
- RabbitMQ.
- Redis.
- Background Workers.
- Processamento assíncrono.
- Envio de e-mails em segundo plano.

---

# Versão 5.0 - Observabilidade

## Objetivo

Monitorar a aplicação em tempo real.

## Funcionalidades

- OpenTelemetry.
- Prometheus.
- Grafana.
- Loki.
- Jaeger.
- Logs estruturados.
- Métricas.
- Traces.

---

# Versão 6.0 - Infraestrutura

## Objetivo

Automatizar a infraestrutura e facilitar o deploy da aplicação.

## Funcionalidades

- Docker.
- Docker Compose.
- CI/CD.
- Kubernetes.
- Deploy automatizado.

---

# Versão 7.0 - Cliente Desktop

## Objetivo

Disponibilizar um cliente Desktop consumindo a API do SGED.

## Funcionalidades

- Aplicação Desktop.
- Consumo da API.
- Compartilhamento das regras de negócio.
- Sincronização com a plataforma.

---

# Evolução Contínua

O roadmap poderá ser atualizado durante o desenvolvimento do projeto.

Novas versões poderão ser adicionadas conforme surgirem novas necessidades, mantendo sempre os princípios definidos em `vision.md`.
