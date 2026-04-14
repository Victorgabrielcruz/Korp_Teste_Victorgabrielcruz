package com.korp.faturamento.controller;

import com.korp.faturamento.dto.NotaFiscalDTO;
import com.korp.faturamento.service.NotaFiscalService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/notas-fiscais")
@CrossOrigin(origins = "*")
public class NotaFiscalController {

    private final NotaFiscalService service;

    public NotaFiscalController(NotaFiscalService service) {
        this.service = service;
    }

    @GetMapping
    public List<NotaFiscalDTO> findAll() {
        return service.findAll();
    }

    @GetMapping("/{id}")
    public NotaFiscalDTO findById(@PathVariable Long id) {
        return service.findById(id);
    }

    @PostMapping
    public ResponseEntity<NotaFiscalDTO> emitir(@Valid @RequestBody NotaFiscalDTO.EmitirNotaRequest request) {
        return ResponseEntity.status(HttpStatus.CREATED).body(service.emitir(request));
    }

    @PatchMapping("/{id}/cancelar")
    public NotaFiscalDTO cancelar(@PathVariable Long id) {
        return service.cancelar(id);
    }
}
