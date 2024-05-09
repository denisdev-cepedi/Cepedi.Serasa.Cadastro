using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class ObterTodasMovimentacoesRequestHandler : IRequestHandler<ObterTodasMovimentacoesRequest, Result<List<ObterTodasMovimentacoesResponse>>>
{
    private readonly ILogger<ObterTodasMovimentacoesRequestHandler> _logger;
    private readonly IMovimentacaoRepository _MovimentacaoRepository;

    public ObterTodasMovimentacoesRequestHandler(ILogger<ObterTodasMovimentacoesRequestHandler> logger, IMovimentacaoRepository MovimentacaoRepository)
    {
        _logger = logger;
        _MovimentacaoRepository = MovimentacaoRepository;
    }

    public async Task<Result<List<ObterTodasMovimentacoesResponse>>> Handle(ObterTodasMovimentacoesRequest request, CancellationToken cancellationToken)
    {
        var movimentacoes = await _MovimentacaoRepository.ObterTodasMovimentacoesAsync();

        if (movimentacoes == null)
        {
            return Result.Error<List<ObterTodasMovimentacoesResponse>>(new Compartilhado.Exececoes.SemResultadoExcecao());
        }
        

        var response = new List<ObterTodasMovimentacoesResponse>();
        foreach (var movimentacao in movimentacoes)
        {
            response.Add(new ObterTodasMovimentacoesResponse(
                movimentacao.Id, 
                movimentacao.IdTipoMovimentacao, 
                movimentacao.IdPessoa, 
                movimentacao.DataHora, 
                movimentacao.NomeEstabelecimento, 
                movimentacao.Valor));
        }

        return Result.Success(response);
    }
}
