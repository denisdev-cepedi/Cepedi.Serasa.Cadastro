using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Movimentacao;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Movimentacao;
public class CriarMovimentacaoRequestHandlerTests
{
    private readonly IMovimentacaoRepository _movimentacaoRepository = Substitute.For<IMovimentacaoRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository = Substitute.For<ITipoMovimentacaoRepository>();
    private readonly ILogger<CriarMovimentacaoRequestHandler> _logger = Substitute.For<ILogger<CriarMovimentacaoRequestHandler>>();
    private readonly CriarMovimentacaoRequestHandler _sut;

    public CriarMovimentacaoRequestHandlerTests()
    {
        _sut = new CriarMovimentacaoRequestHandler(_logger, _movimentacaoRepository, _pessoaRepository, _tipoMovimentacaoRepository);
    }

    [Fact]
    public async Task Handle_QuandoCriarMovimentacao_DeveRetornarSucesso()
    {
        // Arrange
        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            Id = 1,
            NomeTipo = "Compra"
        };

        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "João",
            CPF = "12345678901"
        };

        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPessoa = pessoa.Id,
            NomeEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        var movimentacaoCriada = new MovimentacaoEntity
        {
            Id = 1,
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPessoa = pessoa.Id,
            DataHora = DateTime.UtcNow,
            NomeEstabelecimento = request.NomeEstabelecimento,
            Valor = request.Valor,
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult(tipoMovimentacao));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
                .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdTipoMovimentacao.Should().Be(tipoMovimentacao.Id);
        result.Value.IdPessoa.Should().Be(pessoa.Id);
        result.Value.NomeEstabelecimento.Should().Be(request.NomeEstabelecimento);
        result.Value.Valor.Should().Be(request.Valor);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.Received(1).CriarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>());
    }

    [Fact]
    public async Task Handle_QuandoTipoMovimentacaoNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var tipoMovimentacaoId = 999; // ID inválido que não existe no repositório
        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "João",
            CPF = "12345678901"
        };

        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacaoId,
            IdPessoa = pessoa.Id,
            NomeEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult<TipoMovimentacaoEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
                .Which.ResultadoErro.Should().Be(CadastroErros.IdTipoMovimentacaoInvalido);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _tipoMovimentacaoRepository.Received(1).ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.DidNotReceiveWithAnyArgs().CriarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>());                }

    [Fact]
    public async Task Handle_QuandoPessoaNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var tipoMovimentacao = new TipoMovimentacaoEntity
        {
            Id = 1,
            NomeTipo = "Compra"
        };

        var pessoaId = 999; // ID inválido que não existe no repositório
        var request = new CriarMovimentacaoRequest
        {
            IdTipoMovimentacao = tipoMovimentacao.Id,
            IdPessoa = pessoaId,
            NomeEstabelecimento = "Exemplo Loja",
            Valor = 100.0m
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult<PessoaEntity>(null));
        _tipoMovimentacaoRepository.ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao).Returns(Task.FromResult<TipoMovimentacaoEntity>(tipoMovimentacao));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarMovimentacaoResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<Compartilhado.Exececoes.ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.IdPessoaInvalido);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _tipoMovimentacaoRepository.DidNotReceive().ObterTipoMovimentacaoAsync(request.IdTipoMovimentacao);
        await _movimentacaoRepository.DidNotReceiveWithAnyArgs().CriarMovimentacaoAsync(Arg.Any<MovimentacaoEntity>());
    }

}

