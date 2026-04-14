# Modelo de Dados

## Serviço de Estoque (`estoque_db`)

### Tabela `produto`

| Coluna         | Tipo          | Restrições                  | Descrição                     |
|----------------|---------------|-----------------------------|-------------------------------|
| id             | BIGSERIAL     | PK                          | Identificador único           |
| codigo         | VARCHAR(50)   | NOT NULL, UNIQUE            | Código SKU do produto         |
| descricao      | VARCHAR(255)  | NOT NULL                    | Descrição do produto          |
| unidade_medida | VARCHAR(10)   | NOT NULL                    | Ex.: UN, KG, L                |
| saldo          | DECIMAL(15,4) | NOT NULL, DEFAULT 0         | Saldo em estoque              |
| preco_unitario | DECIMAL(15,2) | NOT NULL                    | Preço unitário de venda       |
| criado_em      | TIMESTAMP     | NOT NULL, DEFAULT NOW()     | Data de criação               |
| atualizado_em  | TIMESTAMP     | NOT NULL, DEFAULT NOW()     | Data da última atualização    |

---

## Serviço de Faturamento (`faturamento_db`)

### Tabela `nota_fiscal`

| Coluna          | Tipo          | Restrições              | Descrição                      |
|-----------------|---------------|-------------------------|--------------------------------|
| id              | BIGSERIAL     | PK                      | Identificador único            |
| numero          | VARCHAR(20)   | NOT NULL, UNIQUE        | Número da nota fiscal          |
| serie           | VARCHAR(5)    | NOT NULL                | Série da nota                  |
| data_emissao    | TIMESTAMP     | NOT NULL                | Data/hora de emissão           |
| valor_total     | DECIMAL(15,2) | NOT NULL                | Valor total da nota            |
| status          | VARCHAR(20)   | NOT NULL                | RASCUNHO, EMITIDA, CANCELADA   |
| criado_em       | TIMESTAMP     | NOT NULL, DEFAULT NOW() | Data de criação                |

### Tabela `item_nota_fiscal`

| Coluna          | Tipo          | Restrições              | Descrição                      |
|-----------------|---------------|-------------------------|--------------------------------|
| id              | BIGSERIAL     | PK                      | Identificador único            |
| nota_fiscal_id  | BIGINT        | FK → nota_fiscal.id     | Nota fiscal pai                |
| produto_id      | BIGINT        | NOT NULL                | ID do produto (estoque_db)     |
| quantidade      | DECIMAL(15,4) | NOT NULL                | Quantidade vendida             |
| preco_unitario  | DECIMAL(15,2) | NOT NULL                | Preço no momento da emissão    |
| valor_total     | DECIMAL(15,2) | NOT NULL                | quantidade × preco_unitario    |
