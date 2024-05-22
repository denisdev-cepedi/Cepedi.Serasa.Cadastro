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
public class ObterPessoaPorIdRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly ILogger<ObterPessoaPorIdRequestHandler> _logger;
    private readonly ObterPessoaPorIdRequestHandler _sut;

    public ObterPessoaPorIdRequestHandlerTests()
    {
        _pessoaRepository = Substitute.For<IPessoaRepository>();
        _logger = Substitute.For<ILogger<ObterPessoaPorIdRequestHandler>>();
        _sut = new ObterPessoaPorIdRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task QuandoBuscarIdExistenteDeveRetornarPessoaResponse()
    {
        var request = new ObterPessoaPorIdRequest { Id = 1 };

        var pessoa = new PessoaEntity
        {
            Id = request.Id,
            Nome = "Carlos Matos",
            CPF = "86088154004"
        };

        _pessoaRepository.ObterPessoaAsync(request.Id).Returns(Task.FromResult(pessoa));

        var result = await _sut.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeOfType<Result<ObterPessoaResponse>>();
        result.Value.Id.Should().Be(request.Id);
        result.Value.Nome.Should().Be(pessoa.Nome);
        result.Value.CPF.Should().Be(pessoa.CPF);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
    }

    [Fact]
    public async Task QuandoBuscarPorIdInexistenteDeveRetornarNulo()
    {
        //Arrange
        var request = new ObterPessoaPorIdRequest { Id = 100 };

        _pessoaRepository.ObterPessoaAsync(request.Id).ReturnsNull();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().BeNull();

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.Id);
    }
}
