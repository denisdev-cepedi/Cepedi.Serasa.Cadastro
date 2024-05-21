using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;

public class ExcluirPessoaPorIdRequestHandler : IRequestHandler<ExcluirPessoaPorIdRequest, Result<ExcluirPessoaPorIdResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ExcluirPessoaPorIdRequestHandler> _logger;
    public ExcluirPessoaPorIdRequestHandler(ILogger<ExcluirPessoaPorIdRequestHandler> logger, IPessoaRepository pessoaRepository)
    {
        _logger = logger;
        _pessoaRepository = pessoaRepository;
    }
    public async Task<Result<ExcluirPessoaPorIdResponse>> Handle(ExcluirPessoaPorIdRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaRepository.ObterPessoaAsync(request.Id);
        if (pessoaEntity == null) return Result.Error<ExcluirPessoaPorIdResponse>(new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        await _pessoaRepository.ExcluirPessoaAsync(pessoaEntity.Id);
        return Result.Success(new ExcluirPessoaPorIdResponse(pessoaEntity.Id, pessoaEntity.Nome, pessoaEntity.CPF));
    }
}