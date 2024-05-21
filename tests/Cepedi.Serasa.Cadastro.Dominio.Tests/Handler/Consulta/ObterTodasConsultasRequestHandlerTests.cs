using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Consulta;
public class ObterTodasConsultasRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<ObterTodasConsultasRequestHandler> _logger = Substitute.For<ILogger<ObterTodasConsultasRequestHandler>>();
    private readonly ObterTodasConsultasRequestHandler _sut;

    public ObterTodasConsultasRequestHandlerTests()
    {
        _sut = new ObterTodasConsultasRequestHandler(_logger, _consultaRepository);
    }

    [Fact]
    public async Task Handle_QuandoObterTodasConsultas_DeveRetornarTodasConsultas()
    {
        // Arrange
        var consultas = new List<ConsultaEntity>
        {
            new ConsultaEntity
            {
                Id = 1,
                Status = true,
                Data = DateTime.UtcNow.AddDays(-1),
                IdPessoa = 1
            },
            new ConsultaEntity
            {
                Id = 2,
                Status = true,
                Data = DateTime.UtcNow.AddDays(-1),
                IdPessoa = 2
            }
        };

        _consultaRepository.ObterTodasConsultasAsync()
                            .Returns(Task.FromResult(consultas));

        var request = new ObterTodasConsultasRequest();

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert

        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Should().HaveCount(consultas.Count);

        for (int i = 0; i < consultas.Count; i++)
        {
            result.Value[i].Id.Should().Be(consultas[i].Id);
            result.Value[i].Status.Should().Be(consultas[i].Status);
            result.Value[i].Data.Should().Be(consultas[i].Data);
            result.Value[i].IdPessoa.Should().Be(consultas[i].IdPessoa);
        }

        await _consultaRepository.Received(1).ObterTodasConsultasAsync();

    }

    [Fact]
    public async Task Handle_QuandoNaoExistemConsultas_DeveRetornarListaVazia()
    {
        //Arrange

        var consultasVazias = new List<ConsultaEntity>();

        _consultaRepository.ObterTodasConsultasAsync()
                            .Returns(Task.FromResult(consultasVazias));

        var request = new ObterTodasConsultasRequest();

        //Act 
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert

        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeEmpty();

        await _consultaRepository.Received(1).ObterTodasConsultasAsync();
    }

}
