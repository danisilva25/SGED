# SGED - Sistema de Gerenciamento Escolar Digital

## Visão

O SGED (Sistema de Gerenciamento Escolar Digital) é uma plataforma moderna de gestão educacional desenvolvida para centralizar os processos administrativos e acadêmicos de instituições de ensino.

Seu objetivo é oferecer uma solução segura, escalável e intuitiva para a administração escolar, simplificando atividades do dia a dia e proporcionando uma melhor experiência para todos os usuários do sistema.

Além da área administrativa, o SGED contará com um portal público que permitirá à população consultar informações institucionais das escolas, notícias, calendário acadêmico e demais conteúdos públicos.

Construído para evoluir: começando simples, crescendo de forma incremental e adotando tecnologias modernas somente quando elas resolverem problemas reais.

---

# Problema

Os sistemas de gestão escolar atuais frequentemente apresentam limitações de usabilidade, desempenho e evolução tecnológica.

Em períodos críticos, como o fechamento bimestral, centenas de professores realizam lançamentos de notas e frequências simultaneamente, ocasionando lentidão e indisponibilidade.

O SGED nasce com o propósito de evoluir continuamente, priorizando simplicidade nas primeiras versões e incorporando novas arquiteturas e tecnologias conforme as necessidades do sistema aumentarem.

---

# Objetivos

- Centralizar a gestão administrativa e acadêmica.
- Simplificar o lançamento de notas e frequências.
- Melhorar a experiência dos usuários.
- Disponibilizar informações públicas das instituições de ensino.
- Evoluir continuamente sem comprometer a estabilidade da plataforma.
- Aplicar boas práticas de engenharia de software durante toda a evolução do projeto.

---

# Estrutura Organizacional

O SGED foi concebido para representar a estrutura administrativa da rede de ensino.

A hierarquia organizacional é composta por:

- Secretaria da Educação
- Diretorias de Ensino
- Escolas

Essas organizações armazenam informações institucionais e servem como base para o controle de acesso e administração do sistema.

As ações realizadas no sistema sempre são executadas por usuários vinculados a uma dessas organizações.

---

# Usuários

O sistema será utilizado pelos seguintes perfis de usuários.

## Administrador da Secretaria da Educação

Responsável pela administração global da plataforma.

Pode gerenciar diretorias de ensino, configurações gerais e usuários administrativos.

## Funcionário da Diretoria de Ensino

Responsável pela administração das escolas pertencentes à sua diretoria.

Pode cadastrar, editar e acompanhar as informações das escolas sob sua responsabilidade.

## Diretor Escolar

Responsável pela administração da escola.

Possui acesso completo às funcionalidades administrativas da instituição.

## Secretário Escolar

Responsável pelo gerenciamento operacional da escola.

Pode cadastrar alunos, professores, turmas, disciplinas e demais informações acadêmicas.

## Professor

Responsável pelo gerenciamento das atividades acadêmicas.

Pode lançar notas, registrar frequência e acompanhar as turmas sob sua responsabilidade.

## Aluno

Pode consultar notas, frequência, histórico escolar, atividades e calendário acadêmico.

## Responsável

Pode acompanhar o desempenho acadêmico dos alunos vinculados.

## Público

Pode acessar o portal público para consultar notícias, informações institucionais das escolas e demais conteúdos disponibilizados.

---

# Módulos

O SGED será desenvolvido de forma incremental.

Os primeiros módulos serão:

- Autenticação
- Administração
- Acadêmico

Ao longo da evolução do projeto poderão ser adicionados novos módulos, como:

- Portal Público
- Comunicação
- Observabilidade
- Relatórios
- Biblioteca
- Transporte Escolar
- Financeiro

---

# Princípios

O desenvolvimento do SGED seguirá os seguintes princípios:

- Simplicidade antes da complexidade.
- Evolução incremental.
- Arquitetura modular.
- Código limpo.
- Segurança.
- Escalabilidade.
- Observabilidade.
- Baixo acoplamento.
- Alta coesão.

---

# Evolução

O SGED será desenvolvido por versões incrementais.

Cada versão deverá representar uma aplicação completamente funcional, permitindo que novas funcionalidades sejam adicionadas sem comprometer a estabilidade das versões anteriores.

O planejamento da evolução será documentado separadamente no arquivo `roadmap.md`.
