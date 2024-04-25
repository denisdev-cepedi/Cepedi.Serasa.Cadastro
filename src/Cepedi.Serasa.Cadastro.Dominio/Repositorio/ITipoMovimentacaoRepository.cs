﻿using Cepedi.Serasa.Cadastro.Dominio.Entidades;

namespace Cepedi.Serasa.Cadastro.Dominio.Repositorio;

public interface ITipoMovimentacaoRepository
{
    Task<TipoMovimentacaoEntity> CriarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao);
    Task<TipoMovimentacaoEntity> ObterTipoMovimentacaoAsync(int id);
    Task<TipoMovimentacaoEntity> AtualizarTipoMovimentacaoAsync(TipoMovimentacaoEntity tipoMovimentacao);
    Task<TipoMovimentacaoEntity> DeletarTipoMovimentacaoAsync(int id);
}