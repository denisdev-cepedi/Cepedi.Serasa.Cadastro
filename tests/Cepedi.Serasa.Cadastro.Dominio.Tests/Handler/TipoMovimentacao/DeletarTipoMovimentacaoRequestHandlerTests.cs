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

public class DeletarTipoMovimentacaoRequestHandlerTests
{
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<DeletarTipoMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<DeletarTipoMovimentacaoRequestHandler>>();
    private readonly DeletarTipoMovimentacaoRequestHandler _sut;

    public DeletarTipoMovimentacaoRequestHandlerTests()
    {
        _sut = new DeletarTipoMovimentacaoRequestHandler(_logger, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoDeletarTipoMovimentacao_DeveRetornarSucesso()
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

        // Act
        var result = await _sut.Handle(new DeletarTipoMovimentacaoRequest { Id = idTipoMovimentacao }, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<DeletarTipoMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(tipoMovimentacaoExistente.Id);
        result.Value.NomeTipo.Should().Be(tipoMovimentacaoExistente.NomeTipo);

        // Verificar se o método no repositório foi chamado corretamente
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(idTipoMovimentacao);
        await _tipoMovimentacaoRepository.Received(1).DeletarTipoMovimentacaoAsync(idTipoMovimentacao);
    }

    [Fact]
    public async Task Handle_QuandoDeletarTipoMovimentacaoInexistente_DeveRetornarFalha()
    {
        // Arrange
        var idTipoMovimentacaoInexistente = 99;

        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(idTipoMovimentacaoInexistente)
                                    .Returns(Task.FromResult<TipoMovimentacaoEntity>(null));

        // Act
        var result = await _sut.Handle(new DeletarTipoMovimentacaoRequest { Id = idTipoMovimentacaoInexistente }, CancellationToken.None);

        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.IsSuccess.Should().BeFalse(); // Verifica se a operação falhou
        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.IdTipoMovimentacaoInvalido);

        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(idTipoMovimentacaoInexistente);
        await _tipoMovimentacaoRepository.DidNotReceive().DeletarTipoMovimentacaoAsync(Arg.Any<int>());
    }
}
