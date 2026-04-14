package com.korp.faturamento.model;

import jakarta.persistence.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "nota_fiscal")
public class NotaFiscal {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false, unique = true, length = 20)
    private String numero;

    @Column(nullable = false, length = 5)
    private String serie;

    @Column(name = "data_emissao", nullable = false)
    private LocalDateTime dataEmissao;

    @Column(name = "valor_total", nullable = false, precision = 15, scale = 2)
    private BigDecimal valorTotal;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false, length = 20)
    private StatusNotaFiscal status = StatusNotaFiscal.RASCUNHO;

    @OneToMany(mappedBy = "notaFiscal", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<ItemNotaFiscal> itens = new ArrayList<>();

    @Column(name = "criado_em", nullable = false, updatable = false)
    private LocalDateTime criadoEm;

    @PrePersist
    void prePersist() {
        criadoEm = LocalDateTime.now();
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
    public StatusNotaFiscal getStatus() { return status; }
    public void setStatus(StatusNotaFiscal status) { this.status = status; }
    public List<ItemNotaFiscal> getItens() { return itens; }
    public void setItens(List<ItemNotaFiscal> itens) { this.itens = itens; }
    public LocalDateTime getCriadoEm() { return criadoEm; }
}
