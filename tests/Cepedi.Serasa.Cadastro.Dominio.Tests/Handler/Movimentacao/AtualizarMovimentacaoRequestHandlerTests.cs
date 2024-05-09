using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Xunit;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Movimentacao
{
    public class AtualizarMovimentacaoRequestHandlerTests
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
        private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<AtualizarMovimentacaoRequestHandler>>();
        private readonly AtualizarMovimentacaoRequestHandler _sut;

        public AtualizarMovimentacaoRequestHandlerTests()
        {
            _sut = new AtualizarMovimentacaoRequestHandler(_logger, _movimentacaoRepository);
        }

        [Fact]
        public async Task Handle_QuandoAtualizarMovimentacao_DeveRetornarSucesso()
        {
            // Arrange
            var request = new AtualizarMovimentacaoRequest
            {
                IdMovimentacao = 1,
                IdTipoMovimentacao = 2,
                IdPessoa = 1,
                NomeEstabelecimento = "Nova Loja",
                Valor = 200.0m
            };

            var movimentacaoExistente = new MovimentacaoEntity
            {
                Id = request.IdMovimentacao,
                IdTipoMovimentacao = 1,
                IdPessoa = request.IdPessoa,
                DataHora = DateTime.UtcNow.AddDays(-1),
                NomeEstabelecimento = "Exemplo Loja",
                Valor = 100.0m
            };

            var movimentacaoAtualizada = new MovimentacaoEntity
            {
                Id = request.IdMovimentacao,
                IdTipoMovimentacao = request.IdTipoMovimentacao,
                IdPessoa = request.IdPessoa,
                DataHora = DateTime.UtcNow,
                NomeEstabelecimento = request.NomeEstabelecimento,
                Valor = request.Valor
            };

            _movimentacaoRepository.ObterMovimentacaoAsync(request.IdMovimentacao)
                                    .Returns(Task.FromResult(movimentacaoExistente));

            _movimentacaoRepository.AtualizarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>())
                                    .Returns(Task.FromResult(movimentacaoAtualizada));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<AtualizarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(request.IdMovimentacao);
            result.Value.IdTipoMovimentacao.Should().Be(request.IdTipoMovimentacao);
            result.Value.IdPessoa.Should().Be(request.IdPessoa);
            result.Value.NomeEstabelecimento.Should().Be(request.NomeEstabelecimento);
            result.Value.Valor.Should().Be(request.Valor);

            // Verificar se os métodos no repositório foram chamados corretamente
            await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(request.IdMovimentacao);
            await _movimentacaoRepository.Received(1).AtualizarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>());
        }
    }
}
