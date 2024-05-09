using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Movimentacao
{
    public class CriarMovimentacaoRequestHandlerTests
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
        private readonly ILogger<CriarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<CriarMovimentacaoRequestHandler>>();
        private readonly CriarMovimentacaoRequestHandler _sut;

        public CriarMovimentacaoRequestHandlerTests()
        {
            _sut = new CriarMovimentacaoRequestHandler(_logger, _movimentacaoRepository);
        }

        [Fact]
        public async Task Handle_QuandoCriarMovimentacao_DeveRetornarSucesso()
        {
            // Arrange
            var request = new CriarMovimentacaoRequest
            {
                IdTipoMovimentacao = 1,
                IdPessoa = 1,
                NomeEstabelecimento = "Exemplo Loja",
                Valor = 100.0m
            };

            var movimentacaoCriada = new MovimentacaoEntity
            {
                IdTipoMovimentacao = request.IdTipoMovimentacao,
                IdPessoa = request.IdPessoa,
                DataHora = DateTime.UtcNow,
                NomeEstabelecimento = request.NomeEstabelecimento,
                Valor = request.Valor
            };

            _movimentacaoRepository.CriarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>())
                                    .Returns(Task.FromResult(movimentacaoCriada));

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
                  .Which.IsSuccess.Should().BeTrue();

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(movimentacaoCriada.Id);
            result.Value.IdTipoMovimentacao.Should().Be(request.IdTipoMovimentacao);
            result.Value.IdPessoa.Should().Be(request.IdPessoa);
            result.Value.NomeEstabelecimento.Should().Be(request.NomeEstabelecimento);
            result.Value.Valor.Should().Be(request.Valor);

            // Verificar se o método no repositório foi chamado corretamente
            await _movimentacaoRepository.Received(1).CriarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>());
        }
    }
}
