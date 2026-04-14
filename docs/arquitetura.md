# Visão Geral da Arquitetura

## Estilo Arquitetural

O sistema adota uma arquitetura de **microsserviços** em que cada domínio de negócio é isolado em seu próprio serviço deployável de forma independente.

```
┌─────────────────────────────────────────────────────────┐
│                        Cliente                          │
│              (Navegador / Angular SPA)                  │
└────────────────────────┬────────────────────────────────┘
                         │ HTTP / REST
          ┌──────────────┼──────────────┐
          ▼                             ▼
┌──────────────────┐         ┌──────────────────────┐
│  Serviço de      │◄───────►│  Serviço de          │
│  Estoque         │  REST   │  Faturamento         │
│  :8081           │         │  :8082               │
└────────┬─────────┘         └──────────┬───────────┘
         │                              │
         ▼                              ▼
┌──────────────────┐         ┌──────────────────────┐
│  estoque_db      │         │  faturamento_db       │
│  (PostgreSQL)    │         │  (PostgreSQL)         │
└──────────────────┘         └──────────────────────┘
```

## Serviços

| Serviço        | Porta | Responsabilidade                          |
|----------------|-------|-------------------------------------------|
| frontend       | 4200  | Interface Angular (SPA)                   |
| estoque        | 8081  | CRUD de produtos e controle de saldo      |
| faturamento    | 8082  | Emissão e gestão de notas fiscais         |
| postgres       | 5432  | Banco de dados relacional persistente     |

## Comunicação entre Serviços

O **Serviço de Faturamento** chama o **Serviço de Estoque** via HTTP REST (usando `RestTemplate` / `WebClient`) para:

- Verificar saldo disponível antes de emitir uma nota.
- Dar baixa no estoque após a emissão confirmada.

## Padrão de Resposta da API

Todas as respostas seguem o envelope:

```json
{
  "data": { ... },
  "message": "OK",
  "timestamp": "2024-01-01T00:00:00Z"
}
```
