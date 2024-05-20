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
public class AtualizarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly ILogger<AtualizarConsultaRequestHandler> _logger = Substitute.For<ILogger<AtualizarConsultaRequestHandler>>();
    private readonly AtualizarConsultaRequestHandler _sut;

    public AtualizarConsultaRequestHandlerTests()
    {
        _consultaRepository = Substitute.For<IConsultaRepository>();
        _logger = Substitute.For<ILogger<AtualizarConsultaRequestHandler>>();
        _sut = new AtualizarConsultaRequestHandler(_consultaRepository, _logger);
    }
    [Fact]
    public async Task Handle_QuandoAtualizarConsulta_DeveRetornarSucesso()
    {
        // Arrange
        var request = new AtualizarConsultaRequest
        {
            Id = 1,
            Status = true,
            Data = DateTime.Now
        };
        var consultaExistente = new ConsultaEntity
        {
            Id = request.Id,
            Status = false,
            Data = DateTime.UtcNow.AddDays(-1),
        };
        var consultaAtualizada = new ConsultaEntity
        {
            Id = request.Id,
            Status = request.Status,
            Data = request.Data,
        };
        _consultaRepository.ObterConsultaAsync(request.Id)
                                    .Returns(Task.FromResult(consultaExistente));

        _consultaRepository.AtualizarConsultaAsync(Arg.Any<ConsultaEntity>())
                                    .Returns(Task.FromResult(consultaAtualizada));
        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        //Assert
        result.Should().BeOfType<Result<AtualizarConsultaResponse>>()
            .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.id.Should().Be(request.Id);
        result.Value.status.Should().Be(request.Status);
        result.Value.data.Should().Be(request.Data);

        await _consultaRepository.Received(1).ObterConsultaAsync(request.Id);
        await _consultaRepository.Received(1).AtualizarConsultaAsync(Arg.Is<ConsultaEntity>(
                m => m.Id == request.Id &&
                    m.Data == request.Data &&
                    m.Status == request.Status
           ));
    }
}