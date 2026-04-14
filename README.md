# Sistema de Emissão de Notas Fiscais

> Desafio técnico – Korp | Victor Gabriel Cruz

## Estrutura do Projeto

```
.
├── backend/
│   ├── estoque/                  # Microsserviço 1 – Serviço de Estoque (Spring Boot)
│   │   ├── src/
│   │   │   ├── main/
│   │   │   │   ├── java/com/korp/estoque/
│   │   │   │   │   ├── controller/
│   │   │   │   │   ├── service/
│   │   │   │   │   ├── repository/
│   │   │   │   │   ├── model/
│   │   │   │   │   └── dto/
│   │   │   │   └── resources/
│   │   │   │       ├── application.yml
│   │   │   │       └── db/changelog/
│   │   │   └── test/
│   │   ├── Dockerfile
│   │   └── pom.xml
│   └── faturamento/              # Microsserviço 2 – Serviço de Faturamento (Spring Boot)
│       ├── src/
│       │   ├── main/
│       │   │   ├── java/com/korp/faturamento/
│       │   │   │   ├── controller/
│       │   │   │   ├── service/
│       │   │   │   ├── repository/
│       │   │   │   ├── model/
│       │   │   │   ├── dto/
│       │   │   │   ├── client/
│       │   │   │   └── config/
│       │   │   └── resources/
│       │   │       ├── application.yml
│       │   │       └── db/changelog/
│       │   └── test/
│       ├── Dockerfile
│       └── pom.xml
├── docs/                         # Documentação técnica
│   ├── arquitetura.md
│   ├── ciclo-de-vida-angular.md
│   ├── rxjs.md
│   ├── frameworks.md
│   ├── modelo-de-dados.md
│   └── adr/
│       └── ADR-001-arquitetura-microservicos.md
├── frontend/                     # Aplicação Angular 17
│   ├── src/
│   │   ├── app/
│   │   │   ├── app.component.ts
│   │   │   ├── app.config.ts
│   │   │   ├── app.routes.ts
│   │   │   ├── core/
│   │   │   │   ├── services/
│   │   │   │   │   ├── estoque.service.ts
│   │   │   │   │   └── faturamento.service.ts
│   │   │   │   ├── interceptors/
│   │   │   │   └── guards/
│   │   │   ├── features/
│   │   │   │   ├── estoque/
│   │   │   │   │   ├── produto-list/
│   │   │   │   │   └── estoque.routes.ts
│   │   │   │   └── notas-fiscais/
│   │   │   │       ├── nota-fiscal-list/
│   │   │   │       └── notas-fiscais.routes.ts
│   │   │   └── shared/
│   │   ├── environments/
│   │   ├── assets/
│   │   ├── index.html
│   │   ├── main.ts
│   │   └── styles.scss
│   ├── angular.json
│   ├── package.json
│   ├── tsconfig.json
│   ├── Dockerfile
│   └── nginx.conf
├── infra/
│   └── postgres/
│       └── init-multiple-dbs.sh
├── .gitignore
├── docker-compose.yml
└── README.md
```

## Pré-requisitos

- [Docker](https://www.docker.com/) 24+
- [Docker Compose](https://docs.docker.com/compose/) v2+
- (Desenvolvimento local) [Node.js](https://nodejs.org/) 20+, [Java](https://adoptium.net/) 17+, [Maven](https://maven.apache.org/) 3.9+

## Como rodar com Docker

```bash
# Subir todos os serviços
docker compose up --build

# Apenas o banco de dados
docker compose up postgres

# Parar tudo
docker compose down
```

## Serviços e Portas

| Serviço     | URL                          | Descrição                      |
|-------------|------------------------------|--------------------------------|
| Frontend    | http://localhost:4200        | Interface Angular (via Nginx)  |
| Estoque     | http://localhost:8081        | API REST – Serviço de Estoque  |
| Faturamento | http://localhost:8082        | API REST – Serviço de Faturamento |
| PostgreSQL  | localhost:5432               | Banco de dados                 |

## Endpoints principais

### Serviço de Estoque (`/api/produtos`)

| Método | Endpoint                     | Descrição               |
|--------|------------------------------|-------------------------|
| GET    | /api/produtos                | Listar produtos         |
| GET    | /api/produtos/{id}           | Buscar produto por ID   |
| POST   | /api/produtos                | Criar produto           |
| PUT    | /api/produtos/{id}           | Atualizar produto       |
| PATCH  | /api/produtos/{id}/baixa     | Dar baixa no estoque    |
| DELETE | /api/produtos/{id}           | Excluir produto         |

### Serviço de Faturamento (`/api/notas-fiscais`)

| Método | Endpoint                           | Descrição                  |
|--------|------------------------------------|----------------------------|
| GET    | /api/notas-fiscais                 | Listar notas fiscais       |
| GET    | /api/notas-fiscais/{id}            | Buscar nota por ID         |
| POST   | /api/notas-fiscais                 | Emitir nota fiscal         |
| PATCH  | /api/notas-fiscais/{id}/cancelar   | Cancelar nota fiscal       |

## Documentação Técnica

Ver pasta [`docs/`](./docs/README.md) para:

- Arquitetura do sistema
- Ciclos de vida dos componentes Angular
- Padrões RxJS utilizados
- Frameworks e bibliotecas
- Modelo de dados
- ADRs (Architecture Decision Records)