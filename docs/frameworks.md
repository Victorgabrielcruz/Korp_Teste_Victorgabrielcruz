# Frameworks e Bibliotecas

## Frontend

| Tecnologia        | Versão mínima | Papel                                         |
|-------------------|---------------|-----------------------------------------------|
| Angular           | 17+           | Framework SPA principal                       |
| Angular Material  | 17+           | Componentes de UI (tabelas, forms, dialogs)   |
| RxJS              | 7+            | Programação reativa / gerenciamento de estado |
| TypeScript        | 5+            | Tipagem estática                              |
| Tailwind CSS      | 3+ (opcional) | Utilitários de estilização                    |

## Backend (ambos os microsserviços)

| Tecnologia         | Versão mínima | Papel                                          |
|--------------------|---------------|------------------------------------------------|
| Java               | 17+           | Linguagem principal                            |
| Spring Boot        | 3+            | Framework de aplicação                         |
| Spring Data JPA    | 3+            | Persistência / ORM                             |
| Spring Web         | 3+            | API REST                                       |
| Spring Validation  | 3+            | Validação de DTOs                              |
| PostgreSQL Driver  | 42+           | Conector JDBC para PostgreSQL                  |
| Liquibase          | 4+            | Migrações de schema do banco                   |
| MapStruct          | 1.5+          | Mapeamento Entity ↔ DTO                        |
| JUnit 5            | 5+            | Testes unitários                               |
| Mockito            | 5+            | Mocks para testes                              |

## Infraestrutura

| Tecnologia    | Papel                                           |
|---------------|-------------------------------------------------|
| Docker        | Containerização dos serviços                    |
| Docker Compose| Orquestração local dos contêineres              |
| PostgreSQL 16 | Banco de dados relacional persistente           |
| Nginx         | Servidor HTTP para o build de produção Angular  |

## Decisão: Comunicação entre microsserviços

Foi escolhida comunicação **síncrona via REST HTTP** por ser o padrão mais simples para o escopo do desafio. Em produção, considerar mensageria assíncrona (ex.: RabbitMQ / Kafka) para desacoplamento.
