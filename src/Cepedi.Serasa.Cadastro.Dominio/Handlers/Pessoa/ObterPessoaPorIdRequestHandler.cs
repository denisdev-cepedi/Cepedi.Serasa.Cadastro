using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;
public class ObterPessoaPorIdRequestHandler : IRequestHandler<ObterPessoaPorIdRequest, Result<ObterPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ObterPessoaPorIdRequestHandler> _logger;

    public ObterPessoaPorIdRequestHandler(IPessoaRepository pessoaRepository, ILogger<ObterPessoaPorIdRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<ObterPessoaResponse>> Handle(ObterPessoaPorIdRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.Id);

        return pessoa == null
            ? Result.Error<ObterPessoaResponse>(new SemResultadoExcecao())
            : Result.Success(new ObterPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
