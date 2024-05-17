using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
        private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger;
        private readonly AtualizarMovimentacaoRequestHandler _sut;

        public AtualizarMovimentacaoRequestHandlerTests()
        {
            _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
            _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
            _logger = Substitute.For<ILogger<AtualizarMovimentacaoRequestHandler>>();
            _sut = new AtualizarMovimentacaoRequestHandler(_tipoMovimentacaoRepository, _movimentacaoRepository, _logger);
        }

        [Fact]
        public async Task Handle_QuandoAtualizarMovimentacao_DeveRetornarSucesso()
        {
            // Arrange
            var request = new AtualizarMovimentacaoRequest
            {
                Id = 1,
                IdTipoMovimentacao = 2,
                DataHora = DateTime.Parse("2024-05-17T16:58:27.845Z"),
                NomeEstabelecimento = "Nova Loja",
                Valor = 200.0m
            };

            var movimentacaoExistente = new MovimentacaoEntity
            {
                Id = request.Id,
                IdTipoMovimentacao = 1,
                DataHora = DateTime.UtcNow.AddDays(-1),
                NomeEstabelecimento = "Exemplo Loja",
                Valor = 100.0m
            };

            var movimentacaoAtualizada = new MovimentacaoEntity
            {
                Id = request.Id,
                IdTipoMovimentacao = request.IdTipoMovimentacao,
                DataHora = request.DataHora,
                NomeEstabelecimento = request.NomeEstabelecimento,
                Valor = request.Valor
            };

            _movimentacaoRepository.ObterMovimentacaoAsync(request.Id)
                                    .Returns(Task.FromResult(movimentacaoExistente));

            _movimentacaoRepository.AtualizarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>())
                                    .Returns(Task.FromResult(movimentacaoAtualizada));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<AtualizarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(request.Id);
            result.Value.IdTipoMovimentacao.Should().Be(request.IdTipoMovimentacao);
            result.Value.NomeEstabelecimento.Should().Be(request.NomeEstabelecimento);
            result.Value.Valor.Should().Be(request.Valor);

            // Verificar se os métodos no repositório foram chamados corretamente
            await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(request.Id);
            await _movimentacaoRepository.Received(1).AtualizarMovimentacaoAsync(Arg.Is<MovimentacaoEntity>(
                m => m.Id == request.Id &&
                     m.IdTipoMovimentacao == request.IdTipoMovimentacao &&
                     m.DataHora == request.DataHora &&
                     m.NomeEstabelecimento == request.NomeEstabelecimento &&
                     m.Valor == request.Valor
            ));
        }
    }
}
