package com.korp.faturamento.service;

import com.korp.faturamento.client.EstoqueClient;
import com.korp.faturamento.dto.NotaFiscalDTO;
import com.korp.faturamento.model.ItemNotaFiscal;
import com.korp.faturamento.model.NotaFiscal;
import com.korp.faturamento.model.StatusNotaFiscal;
import com.korp.faturamento.repository.NotaFiscalRepository;
import jakarta.persistence.EntityNotFoundException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class NotaFiscalService {

    private final NotaFiscalRepository repository;
    private final EstoqueClient estoqueClient;

    public NotaFiscalService(NotaFiscalRepository repository, EstoqueClient estoqueClient) {
        this.repository = repository;
        this.estoqueClient = estoqueClient;
    }

    public List<NotaFiscalDTO> findAll() {
        return repository.findAll().stream().map(this::toDTO).collect(Collectors.toList());
    }

    public NotaFiscalDTO findById(Long id) {
        return toDTO(findNotaById(id));
    }

    @Transactional
    public NotaFiscalDTO emitir(NotaFiscalDTO.EmitirNotaRequest request) {
        NotaFiscal nota = new NotaFiscal();
        nota.setNumero(gerarNumero());
        nota.setSerie(request.getSerie());
        nota.setDataEmissao(LocalDateTime.now());
        nota.setStatus(StatusNotaFiscal.EMITIDA);

        BigDecimal total = BigDecimal.ZERO;

        for (NotaFiscalDTO.ItemRequest itemReq : request.getItens()) {
            EstoqueClient.ProdutoResponse produto = estoqueClient.getProduto(itemReq.getProdutoId());
            BigDecimal valorItem = produto.precoUnitario().multiply(itemReq.getQuantidade());

            ItemNotaFiscal item = new ItemNotaFiscal();
            item.setNotaFiscal(nota);
            item.setProdutoId(produto.id());
            item.setQuantidade(itemReq.getQuantidade());
            item.setPrecoUnitario(produto.precoUnitario());
            item.setValorTotal(valorItem);

            nota.getItens().add(item);
            total = total.add(valorItem);

            estoqueClient.darBaixa(produto.id(), itemReq.getQuantidade());
        }

        nota.setValorTotal(total);
        return toDTO(repository.save(nota));
    }

    @Transactional
    public NotaFiscalDTO cancelar(Long id) {
        NotaFiscal nota = findNotaById(id);
        if (nota.getStatus() != StatusNotaFiscal.EMITIDA) {
            throw new IllegalStateException("Somente notas com status EMITIDA podem ser canceladas.");
        }
        nota.setStatus(StatusNotaFiscal.CANCELADA);
        return toDTO(repository.save(nota));
    }

    private NotaFiscal findNotaById(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Nota fiscal não encontrada: " + id));
    }

    private String gerarNumero() {
        String prefixo = DateTimeFormatter.ofPattern("yyyyMMdd").format(LocalDateTime.now());
        return prefixo + "-" + UUID.randomUUID().toString().substring(0, 8).toUpperCase();
    }

    private NotaFiscalDTO toDTO(NotaFiscal n) {
        NotaFiscalDTO dto = new NotaFiscalDTO();
        dto.setId(n.getId());
        dto.setNumero(n.getNumero());
        dto.setSerie(n.getSerie());
        dto.setDataEmissao(n.getDataEmissao());
        dto.setValorTotal(n.getValorTotal());
        dto.setStatus(n.getStatus().name());
        dto.setItens(n.getItens().stream().map(item -> {
            NotaFiscalDTO.ItemNotaFiscalDTO i = new NotaFiscalDTO.ItemNotaFiscalDTO();
            i.setProdutoId(item.getProdutoId());
            i.setQuantidade(item.getQuantidade());
            i.setPrecoUnitario(item.getPrecoUnitario());
            i.setValorTotal(item.getValorTotal());
            return i;
        }).collect(Collectors.toList()));
        return dto;
    }
}
