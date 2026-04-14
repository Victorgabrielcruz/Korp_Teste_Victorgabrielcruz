package com.korp.faturamento.client;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import org.springframework.web.client.RestTemplate;

import java.math.BigDecimal;

@Component
public class EstoqueClient {

    private final RestTemplate restTemplate;
    private final String estoqueUrl;

    public EstoqueClient(RestTemplate restTemplate,
                         @Value("${estoque.service.url}") String estoqueUrl) {
        this.restTemplate = restTemplate;
        this.estoqueUrl = estoqueUrl;
    }

    public ProdutoResponse getProduto(Long id) {
        return restTemplate.getForObject(estoqueUrl + "/api/produtos/" + id, ProdutoResponse.class);
    }

    public void darBaixa(Long produtoId, BigDecimal quantidade) {
        restTemplate.patchForObject(
                estoqueUrl + "/api/produtos/" + produtoId + "/baixa?quantidade=" + quantidade,
                null, Void.class);
    }

    public record ProdutoResponse(Long id, String codigo, String descricao,
                                   String unidadeMedida, BigDecimal saldo,
                                   BigDecimal precoUnitario) {}
}
