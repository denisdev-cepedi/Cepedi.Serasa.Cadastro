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
public class DeletarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<DeletarConsultaRequestHandler> _logger = Substitute.For<ILogger<DeletarConsultaRequestHandler>>();
    private readonly DeletarConsultaRequestHandler _sut;


    public DeletarConsultaRequestHandlerTests()
    {
        _sut = new DeletarConsultaRequestHandler(_consultaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoDeletarConsulta_DeveRetornarSucesso()
    {
        //Arrange

        var idConsulta = 1;

        var consultaExistente = new ConsultaEntity
        {
            Id = idConsulta,
            Status = true,
            Data = DateTime.UtcNow.AddDays(-1),
            IdPessoa = 1
        };

        _consultaRepository.ObterConsultaAsync(idConsulta)
                            .Returns(Task.FromResult(consultaExistente));

        var request = new DeletarConsultaRequest{ Id = idConsulta };
        //Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<DeletarConsultaResponse>>()
                 .Which.IsSuccess.Should().BeTrue();

        await _consultaRepository.Received(1).ObterConsultaAsync(idConsulta);
        await _consultaRepository.Received(1).DeletarConsultaAsync(idConsulta);


    }
    [Fact]
    public async Task Handle_QuandoDeletarConsultaInexistente_DeveRetornarFalha()
    {
        //Arrange
        var idConsultaInexistente = 123;

        _consultaRepository.ObterConsultaAsync(idConsultaInexistente)
                            .Returns(Task.FromResult<ConsultaEntity>(null));

        //Act
        var result = await _sut.Handle(new DeletarConsultaRequest { Id = idConsultaInexistente }, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull(); // Verifica se o resultado não é nulo
        result.IsSuccess.Should().BeFalse(); // Verifica se a operação falhou
    }
}

