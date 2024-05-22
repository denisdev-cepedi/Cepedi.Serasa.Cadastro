using Cepedi.Serasa.Cadastro.Compartilhado.Excecoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Domain.Repositorio.Queries;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Pessoa;
public class ObterPessoaPorCpfRequestHandler : IRequestHandler<ObterPessoaPorCpfRequest, Result<IEnumerable<ObterPessoaResponse>>>
{
    private readonly IPessoaQueryRepository _pessoaQueryRepository;
    private readonly ILogger<ObterPessoaPorIdRequestHandler> _logger;

    public ObterPessoaPorCpfRequestHandler(IPessoaQueryRepository pessoaQueryRepository, ILogger<ObterPessoaPorIdRequestHandler> logger)
    {
        _pessoaQueryRepository = pessoaQueryRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ObterPessoaResponse>>> Handle(ObterPessoaPorCpfRequest request, CancellationToken cancellationToken)
    {
        var pessoas = await _pessoaQueryRepository.ObterPessoaPorCpfAsync(request.Cpf);

        return !pessoas.Any()
            ? Result.Error<IEnumerable<ObterPessoaResponse>>(new SemResultadoExcecao())
            : Result.Success(pessoas.Select(pessoa => new ObterPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF)));
    }
}
