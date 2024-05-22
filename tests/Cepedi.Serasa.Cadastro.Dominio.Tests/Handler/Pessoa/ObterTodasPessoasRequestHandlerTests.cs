using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Pessoa;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handler.Pessoa;
public class ObterTodasPessoasRequestHandlerTests
{
    private readonly IPessoaRepository _pessoasRepository;
    private readonly ILogger<ObterTodasPessoasRequestHandler> _logger;
    private readonly ObterTodasPessoasRequestHandler _sut;

    public ObterTodasPessoasRequestHandlerTests()
    {
        _pessoasRepository = Substitute.For<IPessoaRepository>();
        _logger = Substitute.For<ILogger<ObterTodasPessoasRequestHandler>>();
        _sut = new ObterTodasPessoasRequestHandler(_pessoasRepository, _logger);
    }

    [Fact]
    public async Task QuandoObterTodasPessoasDeveRetornarListaPessoas()
    {
        //Arrange
        var pessoasNoBanco = new List<PessoaEntity>
        {
            new PessoaEntity {Id = 1, Nome = "Carlos Matos", CPF = "86088154004"},
            new PessoaEntity {Id = 2, Nome = "Joana Andrade", CPF = "45072726029"},
            new PessoaEntity {Id = 3, Nome = "Felipe Meira", CPF = "54284570072"}
        };

        _pessoasRepository.ObterPessoasAsync().Returns(Task.FromResult(pessoasNoBanco));

        var request = new ObterTodasPessoasRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().HaveCount(pessoasNoBanco.Count);

        var resultList = result.Value.ToList();
        for(int i = 0; i < resultList.Count(); i++)
        {
            resultList[i].Id.Should().Be(pessoasNoBanco[i].Id);
            resultList[i].Nome.Should().Be(pessoasNoBanco[i].Nome);
            resultList[i].CPF.Should().Be(pessoasNoBanco[i].CPF);
        }

        await _pessoasRepository.Received(1).ObterPessoasAsync();
    }

    [Fact]
    public async Task QuandoNaoExistirPessoasCadastradasDeveRetornarListaVazia()
    {
        //Arrange
        var pessoasNoBanco = new List<PessoaEntity>();

        _pessoasRepository.ObterPessoasAsync().Returns(Task.FromResult(pessoasNoBanco));

        var request = new ObterTodasPessoasRequest();

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        await _pessoasRepository.Received(1).ObterPessoasAsync();
    }
}
