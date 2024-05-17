﻿using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Enums;
public class CadastroErros
{
    public static readonly ResultadoErro Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static readonly ResultadoErro SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro ErroGravacaoUsuario = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação do usuário. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro ErroGravacaoPessoa = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação de Pessoa. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro ErroGravacaoMovimentacao = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação de Movimentacao. Por favor tente novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro IdMovimentacaoInvalido = new()
    {
        Titulo = "ID de movimentação inválido",
        Descricao = "O ID da movimentação especificada não é válido",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro IdTipoMovimentacaoInvalido = new()
    {
        Titulo = "ID de tipo de movimentação inválido",
        Descricao = "O ID do tipo de movimentação especificado não é válido",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro IdPessoaInvalido = new()
    {
        Titulo = "ID de pessoa inválido",
        Descricao = "O ID da pessoa especificada não é válido",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro ListaMovimentacoesVazia = new()
    {
        Titulo = "Lista de movimentações vazia",
        Descricao = "A lista de movimentações retornada está vazia",
        Tipo = ETipoErro.Alerta
    };
}
