using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Consulta;

public class CriarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<CriarConsultaRequestHandler> _logger = Substitute.For<ILogger<CriarConsultaRequestHandler>>();
    private readonly CriarConsultaRequestHandler _sut;

    public CriarConsultaRequestHandlerTests()
    {
        _sut = new CriarConsultaRequestHandler(_consultaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoCriarConsulta_DeveRetornarSucesso()
    {
        //Arrange
        var pessoa = new PessoaEntity
        {
            Id = 1,
            Nome = "Pedro",
            CPF = "98765432110"
        };

        var request = new CriarConsultaRequest
        {
            Status = true,
            Data = DateTime.Now,
            IdPessoa = pessoa.Id
        };

        var consultaCriada = new ConsultaEntity
        {
            Id = 1,
            Status = request.Status,
            Data = request.Data,
            IdPessoa = pessoa.Id
        };

        _pessoaRepository.ObterPessoaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));

        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert

        result.Should().BeOfType<Result<CriarConsultaResponse>>()
                  .Which.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.idPessoa.Should().Be(request.IdPessoa);
        result.Value.id.Should().Be(consultaCriada.Id);
        result.Value.status.Should().Be(request.Status);
        result.Value.data.Should().Be(request.Data);

        await _pessoaRepository.Received(1).ObterPessoaAsync(request.IdPessoa);
        await _consultaRepository.Received(1).ObterConsultaAsync(consultaCriada.Id);
        await _consultaRepository.Received(1).CriarConsultaAsync(Arg.Any<ConsultaEntity>());
    }
}
