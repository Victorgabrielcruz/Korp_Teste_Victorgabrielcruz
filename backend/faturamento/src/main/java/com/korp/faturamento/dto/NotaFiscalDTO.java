package com.korp.faturamento.dto;

import jakarta.validation.Valid;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Positive;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

public class NotaFiscalDTO {

    private Long id;
    private String numero;
    private String serie;
    private LocalDateTime dataEmissao;
    private BigDecimal valorTotal;
    private String status;
    private List<ItemNotaFiscalDTO> itens;

    // --- Request DTO (inner) ---
    public static class EmitirNotaRequest {
        @NotBlank
        private String serie;

        @Valid
        @NotEmpty
        private List<ItemRequest> itens;

        public String getSerie() { return serie; }
        public void setSerie(String serie) { this.serie = serie; }
        public List<ItemRequest> getItens() { return itens; }
        public void setItens(List<ItemRequest> itens) { this.itens = itens; }
    }

    public static class ItemRequest {
        @NotNull
        private Long produtoId;

        @NotNull
        @Positive
        private BigDecimal quantidade;

        public Long getProdutoId() { return produtoId; }
        public void setProdutoId(Long produtoId) { this.produtoId = produtoId; }
        public BigDecimal getQuantidade() { return quantidade; }
        public void setQuantidade(BigDecimal quantidade) { this.quantidade = quantidade; }
    }

    public static class ItemNotaFiscalDTO {
        private Long produtoId;
        private BigDecimal quantidade;
        private BigDecimal precoUnitario;
        private BigDecimal valorTotal;

        public Long getProdutoId() { return produtoId; }
        public void setProdutoId(Long produtoId) { this.produtoId = produtoId; }
        public BigDecimal getQuantidade() { return quantidade; }
        public void setQuantidade(BigDecimal quantidade) { this.quantidade = quantidade; }
        public BigDecimal getPrecoUnitario() { return precoUnitario; }
        public void setPrecoUnitario(BigDecimal precoUnitario) { this.precoUnitario = precoUnitario; }
        public BigDecimal getValorTotal() { return valorTotal; }
        public void setValorTotal(BigDecimal valorTotal) { this.valorTotal = valorTotal; }
    }

    // Getters and Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    public String getNumero() { return numero; }
    public void setNumero(String numero) { this.numero = numero; }
    public String getSerie() { return serie; }
    public void setSerie(String serie) { this.serie = serie; }
    public LocalDateTime getDataEmissao() { return dataEmissao; }
    public void setDataEmissao(LocalDateTime dataEmissao) { this.dataEmissao = dataEmissao; }
    public BigDecimal getValorTotal() { return valorTotal; }
    public void setValorTotal(BigDecimal valorTotal) { this.valorTotal = valorTotal; }
    public String getStatus() { return status; }
    public void setStatus(String status) { this.status = status; }
    public List<ItemNotaFiscalDTO> getItens() { return itens; }
    public void setItens(List<ItemNotaFiscalDTO> itens) { this.itens = itens; }
}
