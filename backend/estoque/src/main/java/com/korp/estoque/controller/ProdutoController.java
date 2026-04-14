package com.korp.estoque.controller;

import com.korp.estoque.dto.ProdutoDTO;
import com.korp.estoque.service.ProdutoService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.math.BigDecimal;
import java.util.List;

@RestController
@RequestMapping("/api/produtos")
@CrossOrigin(origins = "*")
public class ProdutoController {

    private final ProdutoService service;

    public ProdutoController(ProdutoService service) {
        this.service = service;
    }

    @GetMapping
    public List<ProdutoDTO> findAll() {
        return service.findAll();
    }

    @GetMapping("/{id}")
    public ProdutoDTO findById(@PathVariable Long id) {
        return service.findById(id);
    }

    @PostMapping
    public ResponseEntity<ProdutoDTO> create(@Valid @RequestBody ProdutoDTO dto) {
        return ResponseEntity.status(HttpStatus.CREATED).body(service.create(dto));
    }

    @PutMapping("/{id}")
    public ProdutoDTO update(@PathVariable Long id, @Valid @RequestBody ProdutoDTO dto) {
        return service.update(id, dto);
    }

    @PatchMapping("/{id}/baixa")
    public ResponseEntity<Void> darBaixa(@PathVariable Long id,
                                          @RequestParam BigDecimal quantidade) {
        service.darBaixa(id, quantidade);
        return ResponseEntity.noContent().build();
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable Long id) {
        service.delete(id);
        return ResponseEntity.noContent().build();
    }
}
