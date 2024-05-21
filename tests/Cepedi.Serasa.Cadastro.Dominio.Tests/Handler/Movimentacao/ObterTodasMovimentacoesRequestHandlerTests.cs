using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Movimentacao;

public class ObterTodasMovimentacoesRequestHandlerTests
{
    private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
    private readonly ILogger<ObterTodasMovimentacoesRequestHandler> _logger = Substitute.For<ILogger<ObterTodasMovimentacoesRequestHandler>>();
    private readonly ObterTodasMovimentacoesRequestHandler _sut;

    public ObterTodasMovimentacoesRequestHandlerTests()
    {
        _sut = new ObterTodasMovimentacoesRequestHandler(_logger, _movimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterTodasMovimentacoes_DeveRetornarListaMovimentacoes()
    {
        // Arrange
        var movimentacoes = new List<MovimentacaoEntity>
        {
            new MovimentacaoEntity { Id = 1, IdTipoMovimentacao = 1, IdPessoa = 1, DataHora = DateTime.UtcNow.AddDays(-1), NomeEstabelecimento = "Exemplo Loja 1", Valor = 100.0m },
            new MovimentacaoEntity { Id = 2, IdTipoMovimentacao = 2, IdPessoa = 2, DataHora = DateTime.UtcNow.AddDays(-2), NomeEstabelecimento = "Exemplo Loja 2", Valor = 200.0m }
        };

        _movimentacaoRepository.ObterTodasMovimentacoesAsync()
                                .Returns(Task.FromResult(movimentacoes));

        var request = new ObterTodasMovimentacoesRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        // Verifica se as listas têm o mesmo número de elementos
        result.Value.Should().HaveCount(movimentacoes.Count);

        // Verifica se cada elemento na lista resultante corresponde ao elemento correspondente na lista original
        for (int i = 0; i < movimentacoes.Count; i++)
        {
            result.Value[i].Id.Should().Be(movimentacoes[i].Id);
            result.Value[i].IdTipoMovimentacao.Should().Be(movimentacoes[i].IdTipoMovimentacao);
            result.Value[i].IdPessoa.Should().Be(movimentacoes[i].IdPessoa);
            result.Value[i].DataHora.Should().Be(movimentacoes[i].DataHora);
            result.Value[i].NomeEstabelecimento.Should().Be(movimentacoes[i].NomeEstabelecimento);
            result.Value[i].Valor.Should().Be(movimentacoes[i].Valor);
        }

        // Verifica se o método no repositório foi chamado corretamente
        await _movimentacaoRepository.Received(1).ObterTodasMovimentacoesAsync();
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemMovimentacoes_DeveRetornarListaVazia()
    {
        // Arrange
        var movimentacoesVazias = new List<MovimentacaoEntity>();

        _movimentacaoRepository.ObterTodasMovimentacoesAsync()
                                .Returns(Task.FromResult(movimentacoesVazias));

        var request = new ObterTodasMovimentacoesRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty(); // Verifica se a lista de movimentações está vazia

        // Verifica se o método no repositório foi chamado corretamente
        await _movimentacaoRepository.Received(1).ObterTodasMovimentacoesAsync();
    }
}
