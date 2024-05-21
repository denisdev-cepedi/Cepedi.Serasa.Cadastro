using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
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
            ? Result.Error<ObterPessoaResponse>(new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido))
            : Result.Success(new ObterPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
