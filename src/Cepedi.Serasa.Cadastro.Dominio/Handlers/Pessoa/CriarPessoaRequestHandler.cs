using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;

public class CriarPessoaRequestHandler
    : IRequestHandler<CriarPessoaRequest, Result<CriarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<CriarPessoaRequestHandler> _logger;

    public CriarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<CriarPessoaRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarPessoaResponse>> Handle(CriarPessoaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var pessoa = new PessoaEntity
            {
                Nome = request.Nome,
                CPF = request.CPF
            };

            await _pessoaRepository.CriarPessoaAsync(pessoa);

            return Result.Success(new CriarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro durante a execução");
            return Result.Error<CriarPessoaResponse>(new ExcecaoAplicacao(CadastroErros.ErroGravacaoPessoa));
        }
    }
}
