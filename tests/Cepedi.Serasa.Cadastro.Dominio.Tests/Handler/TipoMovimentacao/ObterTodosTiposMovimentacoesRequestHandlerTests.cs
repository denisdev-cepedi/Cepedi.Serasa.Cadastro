using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.TipoMovimentacao;
public class ObterTodosTiposMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;
    private readonly ILogger<ObterTodosTiposMovimentacaoRequestHandler> _logger;
    private readonly ObterTodosTiposMovimentacaoRequestHandler _sut;

    public ObterTodosTiposMovimentacaoRequestHandlerTests()
    {
        _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
        _logger = Substitute.For<ILogger<ObterTodosTiposMovimentacaoRequestHandler>>();
        _sut = new ObterTodosTiposMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task QuandoObterTodasTipoMovimentacaosDeveRetornarListaTipoMovimentacaos()
    {
        //Arrange
        var tipoMovimentacaoNoBanco = new List<TipoMovimentacaoEntity>
        {
            new TipoMovimentacaoEntity {Id = 1, NomeTipo = "Depósito"},
            new TipoMovimentacaoEntity {Id = 2, NomeTipo = "Saque"},
            new TipoMovimentacaoEntity {Id = 3, NomeTipo = "Transferência"}
        };

        _tipoMovimentacaoRepository.ObterTodosTiposMovimentacaoAsync().Returns(Task.FromResult(tipoMovimentacaoNoBanco));

        var request = new ObterTodosTiposMovimentacaoRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(tipoMovimentacaoNoBanco.Count);

        var resultList = result.Value.ToList();
        for(int i = 0; i < resultList.Count(); i++)
        {
            resultList[i].Id.Should().Be(tipoMovimentacaoNoBanco[i].Id);
            resultList[i].NomeTipo.Should().Be(tipoMovimentacaoNoBanco[i].NomeTipo);
        }

        await _tipoMovimentacaoRepository.Received(1).ObterTodosTiposMovimentacaoAsync();
    }

    [Fact]
    public async Task QuandoNaoExistirTipoMovimentacaosCadastradasDeveRetornarListaVazia()
    {
        //Arrange
        var tipoMovimentacaoNoBanco = new List<TipoMovimentacaoEntity>();

        _tipoMovimentacaoRepository.ObterTodosTiposMovimentacaoAsync().Returns(Task.FromResult(tipoMovimentacaoNoBanco));

        var request = new ObterTodosTiposMovimentacaoRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        await _tipoMovimentacaoRepository.Received(1).ObterTodosTiposMovimentacaoAsync();
    }
}
