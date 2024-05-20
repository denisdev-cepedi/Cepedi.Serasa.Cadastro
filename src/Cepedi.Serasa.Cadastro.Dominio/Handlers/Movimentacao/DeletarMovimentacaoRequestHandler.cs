using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using System.Threading;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
public class DeletarMovimentacaoRequestHandler : IRequestHandler<DeletarMovimentacaoRequest, Result<DeletarMovimentacaoResponse>>
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger;

    public DeletarMovimentacaoRequestHandler(ILogger<DeletarMovimentacaoRequestHandler> logger, IMovimentacaoRepository movimentacaoRepository)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<Result<DeletarMovimentacaoResponse>> Handle(DeletarMovimentacaoRequest request, CancellationToken cancellationToken)
    {
        // Verificar se a movimentação existe com base no ID fornecido na solicitação
        var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(request.Id);

        // Se a movimentação não for encontrada, retornar um erro indicando a falta de resultados
        if (movimentacaoEntity == null)
        {
            return Result.Error<DeletarMovimentacaoResponse>(
                new Compartilhado.Exececoes.ExcecaoAplicacao(CadastroErros.IdMovimentacaoInvalido));
        }

        // Deletar a movimentação do repositório
        await _movimentacaoRepository.DeletarMovimentacaoAsync(movimentacaoEntity.Id);

        // Criar uma resposta com os dados da movimentação deletada
        var response = new DeletarMovimentacaoResponse(
            movimentacaoEntity.Id,
            movimentacaoEntity.IdTipoMovimentacao,
            movimentacaoEntity.IdPessoa,
            movimentacaoEntity.DataHora,
            movimentacaoEntity.NomeEstabelecimento,
            movimentacaoEntity.Valor
        );

        // Retornar um resultado de sucesso com a resposta contendo os dados da movimentação deletada
        return Result.Success(response);
    }
}

