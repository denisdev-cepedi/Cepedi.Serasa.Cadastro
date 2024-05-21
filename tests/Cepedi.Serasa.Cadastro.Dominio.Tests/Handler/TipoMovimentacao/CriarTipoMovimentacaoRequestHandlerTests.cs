using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Validators.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.TipoMovimentacao;

public class CriarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<CriarTipoMovimentacaoRequestHandler> _logger;
    private readonly CriarTipoMovimentacaoRequestHandler _sut;

    public CriarTipoMovimentacaoRequestHandlerTests()
    {
        _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        _logger = Substitute.For<ILogger<CriarTipoMovimentacaoRequestHandler>>();
        _sut = new CriarTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task QuandoCriarTipoMovimentacaoDeveRetornarSucesso()
    {
        //Arrange
        var tipoMovimentacaoRequest = new CriarTipoMovimentacaoRequest
        {
            NomeTipo = "Novo Pix"
        };

        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            NomeTipo = "Novo Pix"
        };

        _tipoMovimentacaoRepository.CriarTipoMovimentacaoAsync(Arg.Is<TipoMovimentacaoEntity>(tipoMovimentacao => tipoMovimentacao.NomeTipo == tipoMovimentacaoRequest.NomeTipo)).Returns(tipoMovimentacao);

        //Act
        var result = await _sut.Handle(tipoMovimentacaoRequest, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<CriarTipoMovimentacaoResponse>>()
            .Which.Value.NomeTipo.Should().Be(tipoMovimentacaoRequest.NomeTipo);

        await _tipoMovimentacaoRepository.Received(1)
            .CriarTipoMovimentacaoAsync(Arg.Is<TipoMovimentacaoEntity>(tipoMovimentacao => tipoMovimentacao.NomeTipo == tipoMovimentacaoRequest.NomeTipo));
    }

    [Fact]
    public async Task QuandoCriarTipoMovimentacaoComDadosInvalidosDeveRetornarErro()
    {
        //Arrange
        var tipoMovimentacaoRequest = new CriarTipoMovimentacaoRequest
        {
            NomeTipo = "La"
        };

        var validator = new CriarTipoMovimentacaoRequestValidation();

        //Act
        var validationResult = validator.Validate(tipoMovimentacaoRequest);
        var result = await _sut.Handle(tipoMovimentacaoRequest, CancellationToken.None);

        //Assert
        await _tipoMovimentacaoRepository.Received(1)
            .CriarTipoMovimentacaoAsync(Arg.Is<TipoMovimentacaoEntity>(tipoMovimentacao => tipoMovimentacao.NomeTipo == tipoMovimentacaoRequest.NomeTipo));

        validationResult.IsValid.Should().BeFalse();
        result.Should().BeOfType<Result<CriarTipoMovimentacaoResponse>>();
        result.IsSuccess.Should().BeTrue();
    }
}
