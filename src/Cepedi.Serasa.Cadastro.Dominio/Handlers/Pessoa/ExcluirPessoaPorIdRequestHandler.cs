using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;
internal class ExcluirPessoaPorIdRequestHandler : IRequestHandler<ExcluirPessoaPorIdRequest, Result<ObterPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ExcluirPessoaPorIdRequestHandler> _logger;

    public ExcluirPessoaPorIdRequestHandler(IPessoaRepository pessoaRepository, ILogger<ExcluirPessoaPorIdRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<ObterPessoaResponse>> Handle(ExcluirPessoaPorIdRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.Id);

        if (pessoa == null)
        {
            return Result.Error<ObterPessoaResponse>(new SemResultadoExcecao());
        }

        await _pessoaRepository.ExcluirPessoaAsync(pessoa);

        return Result.Success(new ObterPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
