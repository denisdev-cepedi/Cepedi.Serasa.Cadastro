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

public class DeletarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<DeletarTipoMovimentacaoRequestHandler> _logger;
    private readonly DeletarTipoMovimentacaoRequestHandler _sut;

    public DeletarTipoMovimentacaoRequestHandlerTests()
    {
        _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        _logger = Substitute.For<ILogger<DeletarTipoMovimentacaoRequestHandler>>();
        _sut = new DeletarTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task QuandoDeletarTipoMovimentacaoExistenteDeveRetornarSucesso()
    {
        var request = new DeletarTipoMovimentacaoRequest { Id = 1 };

        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            Id = 1,
            NomeTipo = "Depósito"
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacao));
        _tipoMovimentacaoRepository.DeletarTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacao));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Should().BeOfType<Result<DeletarTipoMovimentacaoResponse>>();
        result.Value.Id.Should().Be(tipoMovimentacao.Id);
        result.Value.NomeTipo.Should().Be(tipoMovimentacao.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
        await _tipoMovimentacaoRepository.Received(1).DeletarTipoMovimentacaoAsync(request.Id);
    }

    [Fact]
    public async Task QuandoDeletarTipoMovimentacaoInexistenteDeveRetornarNulo()
    {
        var request = new DeletarTipoMovimentacaoRequest { Id = 10 };

        _tipoMovimentacaoRepository.DeletarTipoMovimentacaoAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<DeletarTipoMovimentacaoResponse>>();
        result.IsSuccess.Should().BeFalse();

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
    }
}
