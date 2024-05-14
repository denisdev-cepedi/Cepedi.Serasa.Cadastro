using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;

public class CriarPessoaRequestHandler
    : IRequestHandler<CriarPessoaRequest, Result<CriarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<CriarPessoaRequestHandler> _logger;
    private readonly AbstractValidator<CriarPessoaRequest> _validator;

    public CriarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<CriarPessoaRequestHandler> logger, AbstractValidator<CriarPessoaRequest> validator)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
        _validator = validator;
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
