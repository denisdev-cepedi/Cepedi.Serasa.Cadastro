using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.TipoMovimentacao;

public class ObterTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<ObterTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<ObterTipoMovimentacaoRequestHandler>>();
    private readonly ObterTipoMovimentacaoRequestHandler _sut;

    public ObterTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new ObterTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterTipoMovimentacaoExistente_DeveRetornarTipoMovimentacao()
    {
        // Arrange
        var idTipoMovimentacao = 1;

        var tipoMovimentacaoExistente = new TipoMovimentacaoEntity
        {
            Id = idTipoMovimentacao,
            NomeTipo = "Compra"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(idTipoMovimentacao)
                                    .Returns(Task.FromResult(tipoMovimentacaoExistente));

        var request = new ObterTipoMovimentacaoRequest { Id = idTipoMovimentacao };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Id.Should().Be(tipoMovimentacaoExistente.Id);
        result.Value.NomeTipo.Should().Be(tipoMovimentacaoExistente.NomeTipo);

        // Verifica se o método no repositório foi chamado corretamente
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(idTipoMovimentacao);
    }

    [Fact]
    public async Task Handle_QuandoObterTipoMovimentacaoInexistente_DeveRetornarErro()
    {
        // Arrange
        var idTipoMovimentacaoInexistente = 99;

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(idTipoMovimentacaoInexistente)
                                    .Returns(Task.FromResult<TipoMovimentacaoEntity>(null));

        var request = new ObterTipoMovimentacaoRequest { Id = idTipoMovimentacaoInexistente };

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.IdTipoMovimentacaoInvalido);

        // Verifica se o método no repositório foi chamado corretamente
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(idTipoMovimentacaoInexistente);
    }
}
