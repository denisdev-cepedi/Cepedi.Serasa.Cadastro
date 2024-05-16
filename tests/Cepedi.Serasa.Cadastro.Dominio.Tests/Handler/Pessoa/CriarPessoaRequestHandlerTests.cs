using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Domain.Tests.Handler.Pessoa;

public class CriarPessoaRequestHandlerTests
{
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<CriarPessoaRequestHandler> _logger = Substitute.For<ILogger<CriarPessoaRequestHandler>>();
    private readonly CriarPessoaRequestHandler _sut;

    public CriarPessoaRequestHandlerTests()
    {
        _sut = new CriarPessoaRequestHandler(_pessoaRepository, _logger);
    }

    [Fact]
    public async Task QuandoCriarPessoaDeveRetornarSucesso()
    {
        //Arrange
        var pessoaRequest = new CriarPessoaRequest
        {
            Nome = "Fernando Lima",
            CPF = "43669795006"
        };

        var pessoa = new PessoaEntity
        {
            Nome = pessoaRequest.Nome,
            CPF = pessoaRequest.CPF
        };

        //_pessoaRepository.CriarPessoaAsync(Arg.Any<PessoaEntity>()).Returns(pessoa);
        _pessoaRepository.CriarPessoaAsync(Arg.Is<PessoaEntity>(pessoa => pessoa.Nome == pessoaRequest.Nome
        && pessoa.CPF == pessoaRequest.CPF)).Returns(pessoa);

        //Act
        var result = await _sut.Handle(pessoaRequest, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<CriarPessoaResponse>>()
            .Which.Value.Nome.Should().Be(pessoaRequest.Nome);

        result.Should().BeOfType<Result<CriarPessoaResponse>>()
            .Which.Value.CPF.Should().Be(pessoaRequest.CPF);
    }
}
