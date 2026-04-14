package com.korp.faturamento.model;

import jakarta.persistence.*;

import java.math.BigDecimal;

@Entity
@Table(name = "item_nota_fiscal")
public class ItemNotaFiscal {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "nota_fiscal_id", nullable = false)
    private NotaFiscal notaFiscal;

    @Column(name = "produto_id", nullable = false)
    private Long produtoId;

    @Column(nullable = false, precision = 15, scale = 4)
    private BigDecimal quantidade;

    @Column(name = "preco_unitario", nullable = false, precision = 15, scale = 2)
    private BigDecimal precoUnitario;

    @Column(name = "valor_total", nullable = false, precision = 15, scale = 2)
    private BigDecimal valorTotal;

    // Getters and Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    public NotaFiscal getNotaFiscal() { return notaFiscal; }
    public void setNotaFiscal(NotaFiscal notaFiscal) { this.notaFiscal = notaFiscal; }
    public Long getProdutoId() { return produtoId; }
    public void setProdutoId(Long produtoId) { this.produtoId = produtoId; }
    public BigDecimal getQuantidade() { return quantidade; }
    public void setQuantidade(BigDecimal quantidade) { this.quantidade = quantidade; }
    public BigDecimal getPrecoUnitario() { return precoUnitario; }
    public void setPrecoUnitario(BigDecimal precoUnitario) { this.precoUnitario = precoUnitario; }
    public BigDecimal getValorTotal() { return valorTotal; }
    public void setValorTotal(BigDecimal valorTotal) { this.valorTotal = valorTotal; }
}
