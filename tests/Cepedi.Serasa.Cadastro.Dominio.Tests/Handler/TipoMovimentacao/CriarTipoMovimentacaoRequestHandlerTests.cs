using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.TipoMovimentacao;

public class CriarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<CriarTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<CriarTipoMovimentacaoRequestHandler>>();
    private readonly CriarTipoMovimentacaoRequestHandler _sut;

    public CriarTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new CriarTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarTipoMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var request = new CriarTipoMovimentacaoRequest
        {
            NomeTipo = "Venda"
        };

        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            Id = 1,
            NomeTipo = request.NomeTipo
        };

        _tipoMovimentacaoRepository
            .When(repo => repo.CriarTipoMovimentacaoAsync(Arg.Any<TipoMovimentacaoEntity>()))
            .Do(callInfo => 
            {
                var tipoMov = callInfo.Arg<TipoMovimentacaoEntity>();
                tipoMov.Id = tipoMovimentacao.Id;
            });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(tipoMovimentacao.Id);
        result.Value.NomeTipo.Should().Be(request.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1).CriarTipoMovimentacaoAsync(Arg.Is<TipoMovimentacaoEntity>(
            tm => tm.NomeTipo == request.NomeTipo
        ));
    }
}
