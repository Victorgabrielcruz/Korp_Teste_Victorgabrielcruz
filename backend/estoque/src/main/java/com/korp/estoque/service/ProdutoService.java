package com.korp.estoque.service;

import com.korp.estoque.dto.ProdutoDTO;
import com.korp.estoque.model.Produto;
import com.korp.estoque.repository.ProdutoRepository;
import jakarta.persistence.EntityNotFoundException;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class ProdutoService {

    private final ProdutoRepository repository;

    public ProdutoService(ProdutoRepository repository) {
        this.repository = repository;
    }

    public List<ProdutoDTO> findAll() {
        return repository.findAll().stream()
                .map(this::toDTO)
                .collect(Collectors.toList());
    }

    public ProdutoDTO findById(Long id) {
        return toDTO(findProdutoById(id));
    }

    @Transactional
    public ProdutoDTO create(ProdutoDTO dto) {
        if (repository.existsByCodigo(dto.getCodigo())) {
            throw new IllegalArgumentException("Já existe produto com o código: " + dto.getCodigo());
        }
        Produto produto = toEntity(dto);
        return toDTO(repository.save(produto));
    }

    @Transactional
    public ProdutoDTO update(Long id, ProdutoDTO dto) {
        Produto produto = findProdutoById(id);
        produto.setDescricao(dto.getDescricao());
        produto.setUnidadeMedida(dto.getUnidadeMedida());
        produto.setPrecoUnitario(dto.getPrecoUnitario());
        return toDTO(repository.save(produto));
    }

    @Transactional
    public void darBaixa(Long id, BigDecimal quantidade) {
        Produto produto = findProdutoById(id);
        if (produto.getSaldo().compareTo(quantidade) < 0) {
            throw new IllegalStateException("Saldo insuficiente para o produto: " + produto.getCodigo());
        }
        produto.setSaldo(produto.getSaldo().subtract(quantidade));
        repository.save(produto);
    }

    @Transactional
    public void delete(Long id) {
        repository.delete(findProdutoById(id));
    }

    private Produto findProdutoById(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new EntityNotFoundException("Produto não encontrado: " + id));
    }

    private ProdutoDTO toDTO(Produto p) {
        ProdutoDTO dto = new ProdutoDTO();
        dto.setId(p.getId());
        dto.setCodigo(p.getCodigo());
        dto.setDescricao(p.getDescricao());
        dto.setUnidadeMedida(p.getUnidadeMedida());
        dto.setSaldo(p.getSaldo());
        dto.setPrecoUnitario(p.getPrecoUnitario());
        return dto;
    }

    private Produto toEntity(ProdutoDTO dto) {
        Produto p = new Produto();
        p.setCodigo(dto.getCodigo());
        p.setDescricao(dto.getDescricao());
        p.setUnidadeMedida(dto.getUnidadeMedida());
        p.setSaldo(dto.getSaldo() != null ? dto.getSaldo() : BigDecimal.ZERO);
        p.setPrecoUnitario(dto.getPrecoUnitario());
        return p;
    }
}
