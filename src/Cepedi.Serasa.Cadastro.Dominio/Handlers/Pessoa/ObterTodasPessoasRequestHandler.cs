using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;
public class ObterTodasPessoasRequestHandler : IRequestHandler<ObterTodasPessoasRequest, Result<IEnumerable<ObterPessoaResponse>>>
{
    private readonly IPessoaRepository _pessoasRepository;
    private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;

    public ObterTodasPessoasRequestHandler(IPessoaRepository pessoasRepository, ILogger<ObterTodasPessoasRequestHandler> logger)
    {
        _pessoasRepository = pessoasRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ObterPessoaResponse>>> Handle(ObterTodasPessoasRequest request, CancellationToken cancellationToken)
    {
        var pessoas = await _pessoasRepository.ObterPessoasAsync();

        return !pessoas.Any()
            ? Result.Error<IEnumerable<ObterPessoaResponse>>(new SemResultadoExcecao())
            : Result.Success(pessoas.Select(pessoa => new ObterPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF)));
    }
}
