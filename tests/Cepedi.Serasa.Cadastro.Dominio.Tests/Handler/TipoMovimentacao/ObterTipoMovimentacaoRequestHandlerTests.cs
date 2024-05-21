using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.TipoMovimentacao;
public class ObterTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<ObterTipoMovimentacaoRequestHandler> _logger;
    private readonly ObterTipoMovimentacaoRequestHandler _sut;

    public ObterTipoMovimentacaoRequestHandlerTests()
    {
        _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        _logger = Substitute.For<ILogger<ObterTipoMovimentacaoRequestHandler>>();
        _sut = new ObterTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task QuandoBuscarIdExistenteDeveRetornarTipoMovimentacaoResponse()
    {
        var request = new ObterTipoMovimentacaoRequest { Id = 1 };

        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            Id = request.Id,
            NomeTipo = "Depósito"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacao));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterTipoMovimentacaoResponse>>();
        result.Value.Id.Should().Be(request.Id);
        result.Value.NomeTipo.Should().Be(tipoMovimentacao.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
    }

    [Fact]
    public async Task QuandoBuscarInexistenteDeveRetornarNulo()
    {
        //Arrange
        var request = new ObterTipoMovimentacaoRequest { Id = 100 };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).ReturnsNull();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
    }
}
