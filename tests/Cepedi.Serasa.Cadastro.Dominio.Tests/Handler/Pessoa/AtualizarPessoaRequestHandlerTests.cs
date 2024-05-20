using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Validators.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Pessoa;
public class AtualizarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<AtualizarPessoaRequestHandler> _logger;
    private readonly AtualizarPessoaRequestHandler _sut;

    public AtualizarPessoaRequestHandlerTests()
    {
        _pessoaRepository = Substitute.For<IPessoaRepository>();
        _logger = Substitute.For<ILogger<AtualizarPessoaRequestHandler>>();
        _sut = new AtualizarPessoaRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task QuandoAtualizarPessoaDeveRetornarSucesso()
    {
        var request = new AtualizarPessoaRequest
        {
            Id = 1,
            Nome = "Carlos Matos",
            CPF = "86088154004"
        };

        var pessoaDoBanco = new PessoaEntity
        {
            Id = 1,
            Nome = "Carlos",
            CPF = "86088154002"
        };

        var pessoaAtualizada = new PessoaEntity
        {
            Id = request.Id,
            Nome = request.Nome,
            CPF = request.CPF
        };

        _pessoaRepository.ObterPessoaAsync(request.Id).Returns(Task.FromResult(pessoaDoBanco));
        _pessoaRepository.AtualizarPessoaAsync(pessoaDoBanco).Returns(Task.FromResult(pessoaAtualizada));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<AtualizarPessoaResponse>>().Which.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(request.Id);
        result.Value.Nome.Should().Be(request.Nome);
        result.Value.CPF.Should().Be(request.CPF);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
        await _pessoaRepository.Received(1).AtualizarPessoaAsync(pessoaDoBanco);
    }

    [Fact]
    public async Task QuandoTentarAtualizarPessoaComIdQueNaoExisteDeveRetornarErro()
    {
        var request = new AtualizarPessoaRequest
        {
            Id = 50,
            Nome = "Zé",
            CPF = "1234"
        };

        _pessoaRepository
            .ObterPessoaAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);

        result.Should().BeOfType<Result<AtualizarPessoaResponse>>();
        result.IsSuccess.Should().BeFalse();
    }
}
