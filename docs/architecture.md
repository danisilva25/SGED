# SGED — Architecture

## 1. Visão Geral

O **SGED — Sistema de Gerenciamento Escolar Digital** é um sistema de gestão escolar projetado para centralizar os processos acadêmicos, pedagógicos e administrativos de uma rede de ensino.

O sistema será inicialmente desenvolvido como um **monólito modular**, utilizando **Clean Architecture** e princípios de **Domain-Driven Design (DDD)**.

A arquitetura deverá permitir que os módulos evoluam de forma independente, mantendo baixo acoplamento e fronteiras de domínio bem definidas. A adoção futura de microsserviços poderá ocorrer de maneira incremental, somente quando houver justificativa técnica ou operacional.

### Objetivos

- Centralizar informações da vida escolar do aluno.
- Organizar matrículas, enturmações e movimentações.
- Gerenciar currículo, matriz curricular e componentes.
- Controlar calendário e períodos letivos.
- Gerenciar turmas e grade horária.
- Controlar atribuição de aulas e professores.
- Registrar aulas, frequência e atividades.
- Gerenciar avaliações e fechamento bimestral.
- Garantir controle de acesso por perfil e escopo.
- Manter rastreabilidade das operações sensíveis.
- Permitir evolução futura para arquitetura distribuída.

---

## 2. Princípios Arquiteturais

### 2.1 Clean Architecture

A aplicação seguirá a separação de responsabilidades:

```text
┌──────────────────────────────────────────────┐
│                  Presentation                │
│                 ASP.NET Core API             │
├──────────────────────────────────────────────┤
│                 Application                  │
│       Casos de uso / Commands / Queries      │
├──────────────────────────────────────────────┤
│                    Domain                    │
│ Entidades / Aggregates / Value Objects /     │
│ Regras de negócio / Domain Services          │
├──────────────────────────────────────────────┤
│               Infrastructure                 │
│ EF Core / PostgreSQL / RabbitMQ / Redis /    │
│ serviços externos                            │
└──────────────────────────────────────────────┘
```

A regra de dependência aponta para o domínio.

```text
Presentation
     ↓
Application
     ↓
Domain
     ↑
Infrastructure
```

O domínio não deve depender de ASP.NET Core, Entity Framework Core, PostgreSQL, RabbitMQ ou qualquer outra tecnologia de infraestrutura.

### 2.2 Domain-Driven Design

O domínio escolar será modelado explicitamente através de:

- Entities;
- Aggregate Roots;
- Value Objects;
- Domain Services;
- Domain Events;
- Repositories;
- regras e invariantes de negócio.

### 2.3 Monólito Modular

O sistema não será inicialmente dividido em microsserviços.

Os módulos terão:

- responsabilidades claras;
- modelos próprios;
- regras encapsuladas;
- baixo acoplamento;
- comunicação através de contratos bem definidos.

A futura extração de um módulo para microsserviço deverá ser uma consequência da arquitetura, e não um requisito inicial.

---

# 3. Visão Geral dos Módulos

```text
SGED
│
├── Organização
│   ├── SecretariaEducacao
│   ├── DiretoriaEnsino
│   └── Escola
│
├── Acadêmico
│   ├── Aluno
│   ├── Responsavel
│   ├── Matricula
│   ├── Enturmacao
│   └── VidaEscolar
│
├── Currículo
│   ├── EtapaAnoEscolar
│   ├── ComponenteCurricular
│   ├── ObjetoConhecimento
│   ├── Habilidade
│   ├── MatrizCurricular
│   └── ItemMatrizCurricular
│
├── Calendário
│   ├── AnoLetivo
│   ├── CalendarioEscolar
│   └── Bimestre
│
├── Turmas
│   ├── Turma
│   ├── GradeHoraria
│   └── HorarioAula
│
├── Atribuição
│   └── AtribuicaoAula
│
├── Diário
│   ├── Aula
│   ├── RegistroAula
│   ├── Frequencia
│   └── Atividade
│
├── Avaliação
│   ├── Avaliacao
│   ├── ResultadoAvaliacao
│   ├── FechamentoBimestral
│   └── ResultadoBimestral
│
├── Identidade e Acesso
│   ├── Usuario
│   ├── Perfil
│   ├── Permissao
│   └── EscopoAcesso
│
└── Auditoria
    └── AuditLog
```

---

# 4. Organização da Rede de Ensino

A organização institucional segue uma hierarquia:

```text
Secretaria da Educação
        │
        ├── Diretoria de Ensino
        │       │
        │       ├── Escola
        │       ├── Escola
        │       └── Escola
        │
        └── Diretoria de Ensino
                │
                └── Escola
```

## 4.1 Secretaria da Educação

Representa a administração central da rede estadual.

Possui escopo de atuação sobre toda a rede.

Pode atuar em:

- parametrização de regras da rede;
- configuração curricular;
- acompanhamento de indicadores;
- consulta de dados consolidados;
- administração de parâmetros institucionais;
- acompanhamento das unidades subordinadas.

A Secretaria da Educação não deve ser tratada simplesmente como uma escola com mais permissões. Ela possui um **escopo organizacional próprio**.

## 4.2 Diretoria de Ensino

Representa uma unidade regional responsável por um conjunto de escolas.

É um **Aggregate Root**.

Responsabilidades incluem:

- organização regional;
- acompanhamento das escolas;
- supervisão;
- homologações;
- auditorias;
- liberações excepcionais.

## 4.3 Escola

Representa a unidade escolar.

É um **Aggregate Root**.

Uma escola pertence a uma Diretoria de Ensino.

A escola é responsável pelos processos operacionais da unidade, como:

- matrículas;
- enturmações;
- gestão de turmas;
- calendário escolar;
- atribuições;
- acompanhamento pedagógico;
- fechamento escolar.

---

# 5. Eixo Temporal

O tempo é um elemento fundamental do domínio.

```text
AnoLetivo
   │
   ├── CalendarioEscolar
   │      └── Bimestres
   │
   ├── MatrizCurricular
   │
   ├── Turmas
   │
   └── Registros pedagógicos
```

## 5.1 AnoLetivo

Define o ciclo escolar de determinado ano.

Exemplo:

```text
AnoLetivo: 2026
```

É utilizado para separar dados históricos e atuais.

## 5.2 Bimestre

Representa um período pedagógico dentro do ano letivo.

O sistema utiliza os bimestres para organizar:

- currículo;
- atividades;
- avaliações;
- frequência;
- fechamento;
- recuperação.

## 5.3 CalendarioEscolar

Define os dias válidos do ano escolar.

Classifica os dias como, por exemplo:

- Dia Letivo;
- Sábado Letivo;
- Planejamento;
- Replanejamento;
- Formação;
- Conselho de Classe;
- Feriado;
- Ponto Facultativo;
- Recesso;
- Férias.

O calendário atua como mecanismo de validação para operações do Diário de Classe.

Uma aula não deve ser registrada em um dia que não seja considerado válido para aquela operação.

---

# 6. Domínio Curricular

## 6.1 EtapaAnoEscolar

Define a etapa e o ano/série do estudante.

Exemplos:

- Ensino Fundamental — 6º Ano;
- Ensino Fundamental — 9º Ano;
- Ensino Médio — 1ª Série;
- EJA — Termo I.

A etapa é carregada pela Matrícula do aluno.

## 6.2 ComponenteCurricular

Representa a disciplina/componente curricular.

Exemplos:

- Matemática;
- História;
- Geografia;
- Ciências;
- Língua Portuguesa.

Também representa o componente utilizado na matriz curricular e no diário.

## 6.3 Unidade Temática

Representa uma organização temática/eixo curricular quando aplicável.

Exemplo:

```text
Matemática
 └── Números
```

## 6.4 ObjetoConhecimento

Representa o conteúdo, conceito ou tema que será estudado.

Exemplos:

- Frações;
- Equações do 1º grau;
- Sistemas do corpo humano.

O objeto responde principalmente:

> O que será estudado?

## 6.5 Habilidade

Representa aquilo que o aluno deve ser capaz de fazer com determinado conhecimento.

Uma habilidade possui, conceitualmente:

- processo cognitivo/verbo;
- objeto de conhecimento;
- modificador/contexto.

Exemplo:

```text
EF06MA03
Resolver e elaborar problemas que envolvam
cálculos com números naturais...
```

A habilidade responde:

> O que o aluno deve ser capaz de fazer?

A relação curricular pode ser representada como:

```text
Etapa/Ano
    │
    └── Componente Curricular
          │
          └── Unidade Temática
                │
                └── Objeto de Conhecimento
                      │
                      └── Habilidade
```

---

# 7. Matriz Curricular

A **MatrizCurricular** representa a configuração curricular utilizada em determinado contexto escolar e temporal.

Ela define:

- componentes curriculares;
- carga horária;
- quantidade de aulas;
- período;
- natureza das aulas;
- demais parâmetros curriculares necessários.

Uma mesma Etapa/Ano pode possuir diferentes matrizes.

Exemplo:

```text
6º Ano
 ├── Matriz Regular
 ├── Matriz PEI 7h
 └── Matriz PEI 9h
```

## 7.1 ItemMatrizCurricular

É uma **Entity pertencente à MatrizCurricular**.

Representa a participação de um componente dentro da matriz.

Exemplo:

```text
ItemMatrizCurricular
 ├── ComponenteCurricular
 ├── QuantidadeAulas
 ├── CargaHoraria
 └── Periodo
```

A relação é:

```text
MatrizCurricular
       │
       └── 1:N ──> ItemMatrizCurricular
                         │
                         └──> ComponenteCurricular
```

---

# 8. Aluno, Responsável e Vida Escolar

## 8.1 Aluno

Representa a identidade permanente do estudante.

O RA é a identificação de referência do aluno ao longo da sua trajetória escolar.

O histórico escolar deve permanecer associado ao aluno mesmo quando houver movimentações entre escolas ou turmas.

## 8.2 Responsável

Representa uma pessoa responsável legal pelo aluno.

A relação entre Aluno e Responsável é N:N:

```text
Aluno N ───────── N Responsável
```

Um responsável pode possuir vários alunos.

Um aluno pode possuir vários responsáveis.

A associação deve possuir informações próprias, como:

- tipo de vínculo;
- responsabilidade legal;
- responsável principal;
- vigência do vínculo.

### Invariantes

- Um responsável sem vínculo com aluno não participa do contexto escolar.
- Um responsável não pode possuir mais de um vínculo com o mesmo aluno.
- Um aluno pode possuir vários responsáveis.
- Um aluno não pode possuir mais de um responsável principal simultaneamente.
- O vínculo pode ser encerrado sem excluir o responsável.
- Alterações de dados sensíveis, como CPF/RG, devem possuir regras específicas e rastreabilidade.

## 8.3 Matrícula

A Matrícula representa o vínculo administrativo e legal do aluno com a escola em determinado ano letivo.

Conceitualmente:

```text
Matricula =
    Aluno
    + AnoLetivo
    + Escola
    + EtapaAnoEscolar
```

A Matrícula possui seu ciclo de vida e status.

Exemplos:

- Ativa;
- Pendente;
- Transferida;
- Abandonada;
- Cancelada;
- Não Comparecimento;
- outros estados definidos pelas regras da rede.

## 8.4 Enturmação

A Enturmação representa a alocação operacional da matrícula em uma Turma.

```text
Matricula
     │
     └── Enturmacao ──> Turma
```

A Etapa/Ano Escolar pertence conceitualmente à Matrícula.

A Turma deve validar a compatibilidade entre sua etapa e a etapa da matrícula quando a regra do agrupamento exigir.

A enturmação determina a participação do aluno no diário da turma.

---

# 9. Turma

A **Turma** é um **Aggregate Root**.

Representa uma organização operacional de alunos dentro de uma escola e ano letivo.

Exemplo:

```text
9º Ano A
Manhã
Ano Letivo 2026
```

A Turma está relacionada a:

- Escola;
- AnoLetivo;
- EtapaAnoEscolar;
- MatrizCurricular;
- alunos enturmados;
- grade horária.

---

# 10. Grade Horária

A **GradeHoraria** organiza a distribuição semanal das aulas.

Ela conecta:

```text
MatrizCurricular
       │
       ▼
quantidade de aulas
       │
       ▼
GradeHoraria
       │
       ▼
HorarioAula
```

A GradeHoraria deverá respeitar a carga horária definida pela MatrizCurricular.

## 10.1 HorarioAula

Representa um slot da grade.

Possui conceitualmente:

- dia da semana;
- posição da aula;
- horário inicial;
- horário final;
- componente curricular;
- professor atribuído, quando aplicável.

Exemplo:

```text
Segunda-feira
1ª aula
07:00 - 07:45
Matemática
Professor João
```

## 10.2 Regras

- Não permitir conflito de horário para o mesmo professor.
- Validar a carga horária da matriz.
- Respeitar o calendário escolar.
- Preservar histórico de alterações da grade.
- Alterações de grade devem possuir vigência.

---

# 11. Atribuição de Aulas

A **AtribuicaoAula** representa o vínculo entre professor, componente curricular, turma e período.

```text
Professor
    │
    └── AtribuicaoAula
           ├── Turma
           ├── ComponenteCurricular
           ├── DataInicio
           ├── DataFim
           └── Tipo
```

O tipo pode representar, por exemplo:

- titular;
- substituto;
- outras modalidades previstas pela regra de atribuição.

A atribuição determina quem possui autorização para registrar informações no diário correspondente durante sua vigência.

---

# 12. Diário de Classe

O Diário de Classe representa o registro oficial das atividades pedagógicas.

Os conceitos possuem responsabilidades distintas:

```text
Aula
 ├── RegistroAula
 ├── Frequencia
 └── Atividade

Avaliacao
 └── ResultadoAvaliacao

FechamentoBimestral
 └── ResultadoBimestral
```

## 12.1 Aula

A Aula representa a unidade de tempo e espaço escolar prevista na grade.

Exemplo:

```text
Matemática
8º Ano A
Professor João
Segunda-feira
1ª aula
```

## 12.2 RegistroAula

Representa o conteúdo efetivamente trabalhado pelo professor.

Pode registrar:

- conteúdo;
- objeto de conhecimento;
- habilidade;
- metodologia;
- recursos;
- observações.

Registro de Aula e Frequência são módulos independentes.

Um pode existir sem o outro tecnicamente, embora ambos sejam necessários para a completude do diário.

## 12.3 Frequência

Representa a presença ou ausência do aluno na aula.

No modelo baseado na regra definida para o SGED:

```text
Presente
Falta
```

Não existe status de "Atrasado".

A lista de frequência é determinada pela situação da matrícula e da enturmação na data da aula.

### Regras

- Aluno com matrícula/enturmação válida aparece no diário conforme a data de vigência.
- Entrada posterior não gera frequência retroativa.
- Saída encerra a participação no diário a partir da data aplicável.
- O histórico de frequência anterior não é apagado.
- Remanejamentos preservam os registros anteriores.
- Transferências preservam o histórico da escola de origem.
- A frequência anual deve ser consolidada sobre a trajetória escolar correspondente.

## 12.4 Atividade

Representa uma tarefa ou exercício pedagógico.

Pode possuir:

- título;
- descrição;
- data;
- prazo;
- tipo;
- habilidades associadas;
- vínculo com RegistroAula;
- acompanhamento de realização.

A Atividade não deve ser confundida com Avaliação.

---

# 13. Avaliação

A Avaliação representa um instrumento de mensuração do rendimento.

Pode possuir:

- título;
- data;
- tipo;
- peso;
- habilidades avaliadas;
- resultados individuais.

A estrutura deve permitir:

```text
Avaliacao
     │
     └── N ResultadoAvaliacao
             │
             ├── Aluno
             └── Nota
```

A nota deve seguir a escala definida pela regra do domínio.

---

# 14. Fechamento Bimestral

O FechamentoBimestral consolida os resultados do aluno no período.

Pode reunir:

- resultados de avaliações;
- média;
- frequência;
- resultado bimestral;
- observações;
- decisões do Conselho de Classe.

O resultado calculado automaticamente não elimina a possibilidade de intervenção autorizada no fechamento.

Alterações realizadas pelo Conselho ou pela gestão devem ser rastreáveis.

---

# 15. Vida Escolar

A Vida Escolar representa a trajetória acadêmica do aluno.

Deve preservar:

- matrículas;
- transferências;
- remanejamentos;
- resultados;
- recuperação;
- aprovação;
- reprovação;
- progressão;
- histórico escolar;
- demais movimentações relevantes.

O princípio fundamental é:

> **Movimentação não significa apagamento de histórico.**

Registros históricos devem permanecer vinculados ao período e contexto em que ocorreram.

---

# 16. Controle de Acesso

O sistema utilizará **RBAC — Role-Based Access Control**, combinado com **escopo organizacional**.

```text
Usuario
   │
   └── Perfil
          │
          └── Permissao
                 │
                 └── Escopo
```

## Perfis principais

### Professor

Escopo:

- atribuições;
- turmas;
- componentes;
- períodos de vigência.

Pode:

- registrar aula;
- registrar frequência;
- cadastrar atividades;
- lançar avaliações;
- realizar fechamento dentro da janela permitida.

### Equipe Gestora

Escopo:

- escola.

Inclui:

- Diretor;
- Vice;
- Coordenador.

Possui permissões administrativas e pedagógicas ampliadas.

### Secretaria Escolar

Escopo:

- escola.

Responsável principalmente por operações administrativas, incluindo:

- matrícula;
- movimentações;
- cadastros;
- enturmações;
- documentação.

### Supervisor de Ensino

Escopo:

- Diretoria de Ensino.

Pode:

- supervisionar escolas;
- homologar;
- auditar;
- liberar operações excepcionais conforme regras.

### Secretaria da Educação

Escopo:

- rede estadual.

Pode atuar sobre:

- parâmetros da rede;
- currículo;
- configurações institucionais;
- indicadores;
- dados consolidados;
- operações de administração central.

O escopo não deve ser confundido com permissões irrestritas sobre todos os dados operacionais.

---

# 17. Vigência

Entidades que possuem comportamento temporal devem considerar vigência.

Exemplos:

- Matrícula;
- Enturmação;
- Atribuição;
- Grade Horária;
- Responsabilidade legal;
- configurações curriculares.

Modelo conceitual:

```text
DataInicio
DataFim
```

ou uma estrutura de período equivalente.

O objetivo é permitir que o sistema responda corretamente:

> "Quem estava vinculado a quê naquela data?"

---

# 18. Auditoria

Operações de natureza administrativa, pedagógica ou legal devem possuir rastreabilidade.

A entidade `AuditLog` deve registrar operações sensíveis.

Exemplos:

- alteração de nota;
- alteração de frequência;
- alteração de matrícula;
- transferência;
- alteração de responsável;
- alteração de atribuição;
- reabertura de fechamento;
- alteração de resultado.

Informações relevantes:

```text
AuditLog
 ├── Usuario
 ├── DataHora
 ├── Operacao
 ├── Entidade
 ├── EntidadeId
 ├── ValorAnterior
 ├── ValorNovo
 ├── Justificativa
 └── contexto da operação
```

O log deve ser tratado como registro de auditoria e não como simples histórico editável.

---

# 19. Regras de Permissão por Janela Temporal

Além do perfil, determinadas operações dependem do período permitido.

Exemplo:

```text
Professor
   │
   ├── período aberto → pode editar
   │
   └── período fechado → não pode editar
```

Operações extemporâneas podem exigir:

```text
Solicitação
    ↓
Justificativa
    ↓
Autorização
    ↓
Reabertura
    ↓
Alteração
    ↓
Auditoria
    ↓
Novo fechamento
```

---

# 20. Application Layer

A camada Application será responsável pela orquestração dos casos de uso.

Exemplos:

```text
MatricularAlunoCommand
EnturmarAlunoCommand
TransferirAlunoCommand

CriarTurmaCommand
ConfigurarGradeHorariaCommand
AtribuirAulaCommand

RegistrarAulaCommand
RegistrarFrequenciaCommand
CriarAtividadeCommand

CriarAvaliacaoCommand
LancarResultadoAvaliacaoCommand
RealizarFechamentoBimestralCommand
```

Queries devem ser utilizadas para consultas:

```text
ObterAlunosDaTurmaQuery
ObterDiarioDoProfessorQuery
ObterHistoricoEscolarQuery
ObterGradeHorariaQuery
ObterResultadoBimestralQuery
```

Commands alteram estado.

Queries consultam estado.

---

# 21. Domain Layer

O domínio concentra as regras que não podem ser violadas.

Exemplo:

```text
Turma
 └── regras de turma

Matricula
 └── regras de matrícula

Enturmacao
 └── regras de enturmação

MatrizCurricular
 └── regras de composição curricular

GradeHoraria
 └── regras de distribuição

AtribuicaoAula
 └── regras de vínculo docente

Aula
 └── regras do diário

FechamentoBimestral
 └── regras de fechamento
```

As entidades não devem possuir apenas propriedades públicas sem comportamento.

As invariantes devem ser protegidas pelo próprio domínio sempre que possível.

---

# 22. Infrastructure

A infraestrutura será responsável pelas implementações técnicas.

Tecnologias previstas:

- .NET;
- ASP.NET Core;
- Entity Framework Core;
- PostgreSQL;
- Redis;
- RabbitMQ;
- Docker;
- Kubernetes.

Responsabilidades:

- persistência;
- mensageria;
- cache;
- integração externa;
- armazenamento;
- observabilidade;
- infraestrutura de execução.

---

# 23. Persistência

O banco principal será o **PostgreSQL**.

O Entity Framework Core será utilizado como ORM.

As entidades de domínio não devem depender diretamente do EF Core.

A configuração de persistência ficará na infraestrutura.

Exemplo:

```text
Domain
   │
   └── IRepository<T>

Infrastructure
   │
   └── Repository<T>
          │
          └── EF Core
                 │
                 └── PostgreSQL
```

---

# 24. Comunicação Assíncrona

O sistema poderá utilizar **RabbitMQ** para operações assíncronas e comunicação orientada a eventos.

Exemplos futuros:

```text
AlunoMatriculado
AlunoEnturmado
AlunoTransferido
AulaRegistrada
FrequenciaRegistrada
AvaliacaoLancada
FechamentoRealizado
```

Os eventos devem representar fatos que ocorreram no domínio.

Não devem ser utilizados simplesmente como mecanismo genérico de chamadas entre classes.

---

# 25. Redis

Redis poderá ser utilizado para:

- cache;
- controle temporário;
- filas específicas;
- operações que não necessitem de persistência relacional direta;
- suporte a processamento assíncrono.

O uso de Redis não substitui o PostgreSQL como fonte principal dos dados transacionais.

---

# 26. Observabilidade

O sistema deverá possuir observabilidade desde sua evolução arquitetural.

Componentes previstos:

```text
Aplicação
   │
   ├── OpenTelemetry
   │       ├── Traces
   │       ├── Metrics
   │       └── Logs
   │
   ├── Prometheus
   │       └── Métricas
   │
   ├── Jaeger
   │       └── Tracing
   │
   ├── Grafana
   │       └── Dashboards
   │
   └── Loki
           └── Logs
```

A observabilidade deverá permitir identificar:

- erros;
- lentidão;
- gargalos;
- falhas de integração;
- comportamento de APIs;
- processamento assíncrono;
- problemas de infraestrutura.

---

# 27. Segurança

A API deverá utilizar autenticação e autorização.

Conceitos previstos:

- autenticação baseada em tokens;
- RBAC;
- escopo organizacional;
- princípio do menor privilégio;
- validação de autorização na Application/API;
- auditoria de operações sensíveis.

O fato de um usuário conseguir acessar a API não significa que ele possa executar qualquer operação.

A autorização deve considerar:

```text
Quem?
+
O quê?
+
Em qual recurso?
+
Em qual escopo?
+
Em qual período?
```

---

# 28. Integridade do Domínio

Algumas invariantes importantes:

### Responsável

```text
Aluno → N Responsáveis
Responsável → N Alunos

Máximo de 1 Responsável Principal por aluno
Máximo de 1 vínculo entre o mesmo Responsável e Aluno
```

### Matrícula

```text
Aluno
 +
AnoLetivo
 +
Escola
 +
Etapa
```

A matrícula determina a etapa/série do aluno.

### Enturmação

```text
Matrícula → Turma
```

A enturmação representa a alocação operacional.

### Matriz

```text
MatrizCurricular
    └── N ItemMatrizCurricular
```

### Grade

```text
Turma
    └── GradeHoraria
           └── N HorarioAula
```

### Atribuição

```text
Professor
    +
ComponenteCurricular
    +
Turma
    +
Vigência
```

### Diário

```text
Aula
 ├── RegistroAula
 ├── Frequencia
 └── Atividade
```

### Fechamento

```text
Bimestre
    └── Fechamento
           └── ResultadoBimestral
```

---

# 29. Fluxo Acadêmico Principal

O fluxo principal do aluno é:

```text
Aluno
  │
  ▼
Matrícula
  │
  ▼
Enturmação
  │
  ▼
Turma
  │
  ▼
Grade Horária
  │
  ▼
Aula
  │
  ├── Frequência
  ├── Registro de Aula
  └── Atividade
          │
          ▼
      Avaliação
          │
          ▼
   Fechamento Bimestral
          │
          ▼
      Vida Escolar
```

---

# 30. Fluxo Curricular

```text
Etapa/Ano Escolar
        │
        ▼
Componente Curricular
        │
        ▼
Unidade Temática
        │
        ▼
Objeto de Conhecimento
        │
        ▼
Habilidade
```

A Matriz Curricular atua como configuração operacional:

```text
Etapa/Ano
    │
    ▼
MatrizCurricular
    │
    └── ItemMatrizCurricular
            │
            └── ComponenteCurricular
```

---

# 31. Fluxo Temporal

```text
AnoLetivo
   │
   ├── CalendarioEscolar
   │       └── Dias
   │
   ├── Bimestres
   │
   ├── MatrizCurricular
   │
   ├── Turmas
   │
   └── Registros
```

O calendário e os períodos temporais devem ser utilizados para validar operações que dependem da data.

---

# 32. Regras de Evolução Arquitetural

O SGED deverá seguir uma estratégia evolutiva.

### Fase 1 — Monólito Modular

```text
ASP.NET Core
     │
     ├── Organização
     ├── Acadêmico
     ├── Currículo
     ├── Calendário
     ├── Turmas
     ├── Atribuição
     ├── Diário
     ├── Avaliação
     ├── Acesso
     └── Auditoria
```

### Fase 2 — Comunicação por eventos

Introdução gradual de:

- RabbitMQ;
- Domain Events;
- integração assíncrona;
- processamento de tarefas.

### Fase 3 — Extração seletiva

Somente módulos que apresentarem necessidade real poderão ser extraídos.

Possíveis candidatos futuros:

```text
Notificações
Relatórios
Auditoria
Processamento assíncrono
Integrações externas
```

A extração deve preservar os limites de domínio já definidos.

---

# 33. Estrutura de Projeto

Uma possível estrutura inicial:

```text
src/
├── SGED.Api/
│
├── SGED.Application/
│   ├── Abstractions/
│   ├── Behaviors/
│   ├── Common/
│   └── Features/
│
├── SGED.Domain/
│   ├── Common/
│   ├── Organization/
│   ├── Academic/
│   ├── Curriculum/
│   ├── Calendar/
│   ├── Classes/
│   ├── Assignment/
│   ├── Diary/
│   ├── Assessment/
│   ├── Identity/
│   └── Audit/
│
├── SGED.Infrastructure/
│   ├── Persistence/
│   ├── Messaging/
│   ├── Cache/
│   ├── Identity/
│   └── Observability/
│
└── SGED.Contracts/
    ├── Events/
    └── DTOs/
```

A organização interna poderá evoluir conforme o domínio crescer.

---

# 34. Regras de Dependência

As dependências devem respeitar:

```text
SGED.Api
   ↓
SGED.Application
   ↓
SGED.Domain

SGED.Infrastructure
   ↓
SGED.Application
   ↓
SGED.Domain
```

O domínio não deve depender das demais camadas.

A infraestrutura implementa abstrações definidas pelo domínio ou pela aplicação quando apropriado.

---

# 35. Decisões Arquiteturais

## ADR-001 — Clean Architecture

**Decisão:** Utilizar Clean Architecture.

**Motivo:** Separar domínio, casos de uso, apresentação e infraestrutura, reduzindo acoplamento tecnológico.

## ADR-002 — DDD

**Decisão:** Utilizar DDD como abordagem de modelagem.

**Motivo:** O domínio possui regras complexas de matrícula, currículo, frequência, atribuição, fechamento e vida escolar.

## ADR-003 — Monólito Modular

**Decisão:** Iniciar como monólito modular.

**Motivo:** Reduz complexidade operacional inicial e mantém a possibilidade de futura distribuição.

## ADR-004 — PostgreSQL

**Decisão:** Utilizar PostgreSQL como banco relacional principal.

**Motivo:** Atende às necessidades transacionais e relacionais do domínio.

## ADR-005 — RabbitMQ

**Decisão:** Utilizar RabbitMQ para mensageria assíncrona quando houver necessidade.

**Motivo:** Desacoplar processamento e permitir evolução para arquitetura orientada a eventos.

## ADR-006 — Auditoria

**Decisão:** Operações sensíveis devem possuir rastreabilidade.

**Motivo:** Dados escolares possuem importância administrativa, pedagógica e legal.

---

# 36. Princípios Finais

O SGED deve seguir os seguintes princípios:

1. **O domínio é independente da tecnologia.**
2. **Regras de negócio devem estar no domínio ou em casos de uso apropriados.**
3. **Módulos devem possuir fronteiras claras.**
4. **Não criar microsserviços prematuramente.**
5. **Dados históricos não devem ser apagados como consequência de movimentações.**
6. **Operações sensíveis devem ser auditáveis.**
7. **Autorização deve considerar perfil, recurso, escopo e vigência.**
8. **Entidades temporais devem preservar seu período de validade.**
9. **O modelo deve refletir o vocabulário do domínio escolar.**
10. **A arquitetura deve permitir evolução sem reescrever o domínio.**

---

# 37. Resumo da Arquitetura

```text
                         ┌───────────────────────┐
                         │       Frontends       │
                         │   Web / Desktop etc.  │
                         └───────────┬───────────┘
                                     │
                                     ▼
                         ┌───────────────────────┐
                         │      ASP.NET Core     │
                         │          API          │
                         └───────────┬───────────┘
                                     │
                                     ▼
                    ┌────────────────────────────────┐
                    │          Application            │
                    │ Commands / Queries / Use Cases │
                    └────────────────┬───────────────┘
                                     │
                                     ▼
                    ┌────────────────────────────────┐
                    │             Domain             │
                    │                                │
                    │ Organização                    │
                    │ Acadêmico                     │
                    │ Currículo                      │
                    │ Calendário                     │
                    │ Turmas                         │
                    │ Atribuição                     │
                    │ Diário                         │
                    │ Avaliação                      │
                    │ Acesso                         │
                    │ Auditoria                      │
                    └────────────────┬───────────────┘
                                     ▲
                                     │
                    ┌────────────────────────────────┐
                    │         Infrastructure          │
                    │                                │
                    │ EF Core / PostgreSQL            │
                    │ RabbitMQ / Redis                │
                    │ OpenTelemetry                   │
                    │ Serviços externos               │
                    └────────────────────────────────┘
```

**Estado atual da decisão arquitetural:** o SGED será construído como um **monólito modular em .NET, baseado em Clean Architecture e DDD**, com PostgreSQL como persistência principal, mensageria e processamento assíncrono preparados para evolução, controle de acesso baseado em perfil + escopo, e auditoria das operações sensíveis.
