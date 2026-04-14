package com.korp.faturamento.repository;

import com.korp.faturamento.model.NotaFiscal;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface NotaFiscalRepository extends JpaRepository<NotaFiscal, Long> {
    Optional<NotaFiscal> findByNumero(String numero);
    boolean existsByNumero(String numero);
}
