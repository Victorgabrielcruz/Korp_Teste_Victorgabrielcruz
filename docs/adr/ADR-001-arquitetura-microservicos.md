# ADR-001 – Arquitetura de Microsserviços com Comunicação REST Síncrona

**Data:** 2024-01-01  
**Status:** Aceito

## Contexto

O sistema de emissão de notas fiscais precisa integrar controle de estoque e faturamento de forma que uma nota fiscal só possa ser emitida se houver saldo disponível em estoque.

## Decisão

Adotar arquitetura de microsserviços com **dois serviços independentes** (`estoque` e `faturamento`) comunicando-se via **HTTP REST síncrono** para o escopo do desafio técnico.

## Consequências

**Positivas:**
- Isolamento de domínios de negócio.
- Deploy e escalonamento independentes.
- Simplicidade de implementação para o escopo atual.

**Negativas:**
- Acoplamento temporal entre os serviços (se `estoque` estiver fora do ar, emissão de notas falha).
- Em produção, seria recomendado adotar mensageria assíncrona (RabbitMQ / Kafka).
