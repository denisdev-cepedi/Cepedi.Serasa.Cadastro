using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Consulta;
public class ObterConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<ObterConsultaRequestHandler> _logger = Substitute.For<ILogger<ObterConsultaRequestHandler>>();
    private readonly ObterConsultaRequestHandler _sut;

    public ObterConsultaRequestHandlerTests()
    {
        _sut = new ObterConsultaRequestHandler(_consultaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoObterConsultaExistente_DeveRetornarConsulta()
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

        var request = new ObterConsultaRequest { Id = idConsulta };

        //Act

        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert

        result.Should().NotBeNull();
        result.Value.Should().NotBeNull();

        result.Value.Id.Should().Be(consultaExistente.Id);
        result.Value.Status.Should().Be(consultaExistente.Status);
        result.Value.Data.Should().Be(consultaExistente.Data);
        result.Value.IdPessoa.Should().Be(consultaExistente.IdPessoa);

        await _consultaRepository.Received(1).ObterConsultaAsync(idConsulta);

    }

    [Fact]
    public async Task Handle_QuandoObterConsultaInexistente_DeveRetornarNulo(){
         // Arrange
            var idConsultaInexistente = 123;

            _consultaRepository.ObterConsultaAsync(idConsultaInexistente)
                                    .Returns(Task.FromResult<ConsultaEntity>(null));

            var request = new ObterConsultaRequest { Id = idConsultaInexistente };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().BeNull();

            await _consultaRepository.Received(1).ObterConsultaAsync(idConsultaInexistente);
    }


}
