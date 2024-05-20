using System;
using System.Threading;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
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
        private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        private readonly ILogger<AtualizarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<AtualizarMovimentacaoRequestHandler>>();
        private readonly AtualizarMovimentacaoRequestHandler _sut;

        public AtualizarMovimentacaoRequestHandlerTests()
        {
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
                IdPessoa = 1,
                DataHora = DateTime.UtcNow.AddDays(-1),
                NomeEstabelecimento = "Exemplo Loja",
                Valor = 100.0m
            };

            var tipoMovimentacao = new TipoMovimentacaoEntity
            {
                Id = request.IdTipoMovimentacao,
                NomeTipo = "Venda"
            };

            _movimentacaoRepository.ObterMovimentacaoAsync(request.Id).Returns(Task.FromResult(movimentacaoExistente));
            _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult(tipoMovimentacao));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<AtualizarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(request.Id);
            result.Value.IdTipoMovimentacao.Should().Be(request.IdTipoMovimentacao);
            result.Value.IdPessoa.Should().Be(movimentacaoExistente.IdPessoa);
            result.Value.NomeEstabelecimento.Should().Be(request.NomeEstabelecimento);
            result.Value.Valor.Should().Be(request.Valor);

            await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(request.Id);
            await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
            await _movimentacaoRepository.Received(1).AtualizarMovimentacaoAsync(Arg.Is<MovimentacaoEntity>(
                m => m.Id == request.Id &&
                     m.IdTipoMovimentacao == request.IdTipoMovimentacao &&
                     m.DataHora == request.DataHora &&
                     m.NomeEstabelecimento == request.NomeEstabelecimento &&
                     m.Valor == request.Valor
            ));
        }

        [Fact]
        public async Task Handle_QuandoTipoMovimentacaoNaoExistir_DeveRetornarErro()
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

            _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult<TipoMovimentacaoEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<AtualizarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                  .Which.ResultadoErro.Should().Be(CadastroErros.IdTipoMovimentacaoInvalido);
        }

        [Fact]
        public async Task Handle_QuandoMovimentacaoNaoExistir_DeveRetornarErro()
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

            var tipoMovimentacao = new TipoMovimentacaoEntity
            {
                Id = request.IdTipoMovimentacao,
                NomeTipo = "Venda"
            };

            _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult(tipoMovimentacao));
            _movimentacaoRepository.ObterMovimentacaoAsync(request.Id).Returns(Task.FromResult<MovimentacaoEntity>(null));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<AtualizarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeFalse();

            result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                  .Which.ResultadoErro.Should().Be(CadastroErros.IdMovimentacaoInvalido);
        }
    }
}
