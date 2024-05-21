using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
public class AtualizarPessoaRequestHandler
    : IRequestHandler<AtualizarPessoaRequest, Result<AtualizarPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<AtualizarPessoaRequestHandler> _logger;

    public AtualizarPessoaRequestHandler(IPessoaRepository pessoaRepository, ILogger<AtualizarPessoaRequestHandler> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarPessoaResponse>> Handle(AtualizarPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _pessoaRepository.ObterPessoaAsync(request.Id);

        if (pessoa == null)
        {
            return Result.Error<AtualizarPessoaResponse>(new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdPessoaInvalido));
        }

        pessoa.Atualizar(request.Nome, request.CPF);
        await _pessoaRepository.AtualizarPessoaAsync(pessoa);

        return Result.Success(new AtualizarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.CPF));
    }
}
