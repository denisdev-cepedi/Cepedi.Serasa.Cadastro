using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
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

public class AtualizarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<AtualizarTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<AtualizarTipoMovimentacaoRequestHandler>>();
    private readonly AtualizarTipoMovimentacaoRequestHandler _sut;

    public AtualizarTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new AtualizarTipoMovimentacaoRequestHandler(_tipoMovimentacaoRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoAtualizarTipoMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 1,
            NomeTipo = "Nova Venda"
        };

        var tipoMovimentacaoExistente = new TipoMovimentacaoEntity
        {
            Id = request.Id,
            NomeTipo = "Venda"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacaoExistente));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(tipoMovimentacaoExistente.Id);
        result.Value.NomeTipo.Should().Be(request.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
        await _tipoMovimentacaoRepository.Received(1).AtualizarTipoMovimentacaoAsync(Arg.Is<TipoMovimentacaoEntity>(
            tm => tm.Id == request.Id &&
                  tm.NomeTipo == request.NomeTipo
        ));
    }

    [Fact]
    public async Task Handle_QuandoTipoMovimentacaoNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 1,
            NomeTipo = "Nova Venda"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult<TipoMovimentacaoEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                .Which.ResultadoErro.Should().Be(CadastroErros.IdTipoMovimentacaoInvalido);
    }
}
