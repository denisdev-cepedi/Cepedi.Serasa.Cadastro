using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Validators.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Humanizer;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.TipoMovimentacao;
public class AtualizarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<AtualizarTipoMovimentacaoRequestHandler> _logger;
    private readonly AtualizarTipoMovimentacaoRequestHandler _sut;

    public AtualizarTipoMovimentacaoRequestHandlerTests()
    {
        _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        _logger = Substitute.For<ILogger<AtualizarTipoMovimentacaoRequestHandler>>();
        _sut = new AtualizarTipoMovimentacaoRequestHandler(_tipoMovimentacaoRepository, _logger);
    }

    [Fact]
    public async Task QuandoAtualizarTipoMovimentacaoDeveRetornarSucesso()
    {
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 1,
            NomeTipo = "Transferência"
        };

        var tipoMovimentacaoDoBanco = new TipoMovimentacaoEntity
        {
           Id = 1,
            NomeTipo = "Transferência"
        };

        var tipoMovimentacaoAtualizada = new TipoMovimentacaoEntity
        {
            Id = request.Id,
            NomeTipo = request.NomeTipo
        };

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.Id).Returns(Task.FromResult(tipoMovimentacaoDoBanco));
        _tipoMovimentacaoRepository.AtualizarTipoMovimentacaoAsync(tipoMovimentacaoDoBanco).Returns(Task.FromResult(tipoMovimentacaoAtualizada));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>().Which.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(request.Id);
        result.Value.NomeTipo.Should().Be(request.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);
        await _tipoMovimentacaoRepository.Received(1).AtualizarTipoMovimentacaoAsync(tipoMovimentacaoDoBanco);
    }

    [Fact]
    public async Task QuandoTentarAtualizarTipoMovimentacaoComIdQueNaoExisteDeveRetornarErro()
    {
        var request = new AtualizarTipoMovimentacaoRequest
        {
            Id = 50,
            NomeTipo = "LA"
        };

        _tipoMovimentacaoRepository
            .ObterTipoMovimentacaoAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.Id);

        result.Should().BeOfType<Result<AtualizarTipoMovimentacaoResponse>>();
        result.IsSuccess.Should().BeFalse();
    }
}
