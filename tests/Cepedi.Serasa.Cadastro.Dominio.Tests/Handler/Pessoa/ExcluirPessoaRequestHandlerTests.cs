using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handler.Pessoa;

public class ExcluirPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ExcluirPessoaPorIdRequestHandler> _logger;
    private readonly ExcluirPessoaPorIdRequestHandler _sut;

    public ExcluirPessoaRequestHandlerTests()
    {
        _pessoaRepository = Substitute.For<IPessoaRepository>();
        _logger = Substitute.For<ILogger<ExcluirPessoaPorIdRequestHandler>>();
        _sut = new ExcluirPessoaPorIdRequestHandler(_logger, _pessoaRepository);
    }

    [Fact]
    public async Task QuandoExcluirPessoaPorIdExistenteDeveRetornarSucesso()
    {
        var request = new ExcluirPessoaPorIdRequest { Id = 1 };

        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "Carlos Matos",
            CPF = "86088154004"
        };

        _pessoaRepository.ObterPessoaAsync(request.Id).Returns(Task.FromResult(pessoa));
        _pessoaRepository.ExcluirPessoaAsync(request.Id).Returns(Task.FromResult(pessoa));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Should().BeOfType<Result<ExcluirPessoaPorIdResponse>>();
        result.Value.Id.Should().Be(pessoa.Id);
        result.Value.Nome.Should().Be(pessoa.Nome);
        result.Value.CPF.Should().Be(pessoa.CPF);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
        await _pessoaRepository.Received(1).ExcluirPessoaAsync(request.Id);
    }

    [Fact]
    public async Task QuandoExcluirPessoaPorIdInexistenteDeveRetornarNulo()
    {
        var request = new ExcluirPessoaPorIdRequest { Id = 10 };

        _pessoaRepository.ExcluirPessoaAsync(request.Id).ReturnsNull();

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().BeOfType<Result<ExcluirPessoaPorIdResponse>>();
        result.IsSuccess.Should().BeFalse();

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
    }
}
