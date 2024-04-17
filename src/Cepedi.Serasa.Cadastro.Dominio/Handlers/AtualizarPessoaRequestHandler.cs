using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class AtualizarPessoaRequestHandler
    : IRequestHandler<AtualizarPessoaRequest, Result<AtualizarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger _logger;

    public AtualizarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarPessoaResponse>> Handle(AtualizarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.Id);

        if(pessoa == null)
        {
            return Result.Error<AtualizarPessoaResponse>(new SemResultadoExcecao());
        }

        pessoa.Atualizar(request.Nome, request.CPF);
        await _pessoaRepository.AtualizarPessoaAsync(pessoa);

        return Result.Success(new AtualizarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
