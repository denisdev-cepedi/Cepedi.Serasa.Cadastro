using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Handlers.Movimentacao
{
    public class CriarMovimentacaoRequestHandler : IRequestHandler<CriarMovimentacaoRequest, Result<CriarMovimentacaoResponse>>
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly ILogger<CriarMovimentacaoRequestHandler> _logger;
        private readonly IPessoaRepository _pessoaRepository; // Repositório de pessoas

        public CriarMovimentacaoRequestHandler(IMovimentacaoRepository movimentacaoRepository, 
                                               ILogger<CriarMovimentacaoRequestHandler> logger,
                                               IPessoaRepository pessoaRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
            _logger = logger;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<Result<CriarMovimentacaoResponse>> Handle(CriarMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Buscar a pessoa associada à movimentação pelo ID
                var pessoa = await _pessoaRepository.ObterPessoaAsync(request.PessoaId);
                if (pessoa == null)
                {
                    return Result.Error<CriarMovimentacaoResponse>(new ExcecaoAplicacao(CadastroErros.ErroGravacaoMovimentacao));
                }

                var movimentacao = new MovimentacaoEntity
                {
                    PessoaId = request.PessoaId,
                    Pessoa = pessoa,
                    DataHora = DateTime.UtcNow,
                    TipoMovimentacaoId = request.TipoMovimentacaoId,
                    Valor = request.Valor,
                    NomeEstabelecimento = request.NomeEstabelecimento
                };

                await _movimentacaoRepository.CriarMovimentacaoAsync(movimentacao);

                return Result.Success(new CriarMovimentacaoResponse(movimentacao.Id, movimentacao.TipoMovimentacaoId, movimentacao.Valor, movimentacao.DataHora, movimentacao.NomeEstabelecimento));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro durante a execução");
                return Result.Error<CriarMovimentacaoResponse>(new ExcecaoAplicacao(CadastroErros.ErroGravacaoMovimentacao));
            }
        }
    }
}
