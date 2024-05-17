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
    public class DeletarMovimentacaoRequestHandlerTests
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
        private readonly ILogger<DeletarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<DeletarMovimentacaoRequestHandler>>();
        private readonly DeletarMovimentacaoRequestHandler _sut;

        public DeletarMovimentacaoRequestHandlerTests()
        {
            _sut = new DeletarMovimentacaoRequestHandler(_logger, _movimentacaoRepository);
        }

        [Fact]
        public async Task Handle_QuandoDeletarMovimentacao_DeveRetornarSucesso()
        {
            // Arrange
            var idMovimentacao = 1;

            var movimentacaoExistente = new MovimentacaoEntity
            {
                Id = idMovimentacao,
                IdTipoMovimentacao = 1,
                IdPessoa = 1,
                DataHora = DateTime.UtcNow.AddDays(-1),
                NomeEstabelecimento = "Exemplo Loja",
                Valor = 100.0m
            };

            _movimentacaoRepository.ObterMovimentacaoAsync(idMovimentacao)
                                    .Returns(Task.FromResult(movimentacaoExistente));

            // Act
            var result = await _sut.Handle(new DeletarMovimentacaoRequest { Id = idMovimentacao }, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<DeletarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            // Verificar se o método no repositório foi chamado corretamente
            await _movimentacaoRepository.Received(1).ObterMovimentacaoAsync(idMovimentacao);
            await _movimentacaoRepository.Received(1).DeletarMovimentacaoAsync(idMovimentacao);
        }

        [Fact]
        public async Task Handle_QuandoDeletarMovimentacaoInexistente_DeveRetornarFalha()
        {
            // Arrange
            var idMovimentacaoInexistente = 99;

            _movimentacaoRepository.ObterMovimentacaoAsync(idMovimentacaoInexistente)
                                    .Returns(Task.FromResult<MovimentacaoEntity>(null));

            // Act
            var result = await _sut.Handle(new DeletarMovimentacaoRequest { Id = idMovimentacaoInexistente }, CancellationToken.None);

            // Assert
            result.Should().NotBeNull(); // Verifica se o resultado não é nulo
            result.IsSuccess.Should().BeFalse(); // Verifica se a operação falhou
        }
    }
}
