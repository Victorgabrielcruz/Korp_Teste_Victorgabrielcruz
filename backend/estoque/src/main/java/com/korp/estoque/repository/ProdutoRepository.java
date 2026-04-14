package com.korp.estoque.repository;

import com.korp.estoque.model.Produto;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface ProdutoRepository extends JpaRepository<Produto, Long> {
    Optional<Produto> findByCodigo(String codigo);
    boolean existsByCodigo(String codigo);
}
