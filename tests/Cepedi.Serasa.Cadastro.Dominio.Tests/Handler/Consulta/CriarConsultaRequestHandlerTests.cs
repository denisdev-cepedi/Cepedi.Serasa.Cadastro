using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;

namespace Cepedi.Serasa.Cadastro.Dominio.Tests.Handlers.Consulta;
public class CriarConsultaRequestHandlerTests
{
    private readonly IConsultaRepository _consultaRepository = Substitute.For<IConsultaRepository>();
    private readonly IPessoaRepository _pessoaRepository = Substitute.For<IPessoaRepository>();
    private readonly ILogger<CriarConsultaRequestHandler> _logger = Substitute.For<ILogger<CriarConsultaRequestHandler>>();
    private readonly CriarConsultaRequestHandler _sut;

    public CriarConsultaRequestHandlerTests()
    {
        _sut = new CriarConsultaRequestHandler(_consultaRepository, _pessoaRepository, _logger);
    }

    [Fact]
    public async Task Handle_QuandoCriarConsulta_DeveRetornarSucesso()
    {
        // Arrange
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

        _consultaRepository.ObterPessoaConsultaAsync(request.IdPessoa).Returns(Task.FromResult(pessoa));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarConsultaResponse>>()
                    .Which.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.IdPessoa.Should().Be(request.IdPessoa);
        result.Value.Status.Should().Be(request.Status);
        result.Value.Data.Should().Be(request.Data);

        await _consultaRepository.Received(1).ObterPessoaConsultaAsync(request.IdPessoa);
        await _consultaRepository.Received(1).CriarConsultaAsync(Arg.Any<ConsultaEntity>());
    }

    [Fact]
    public async Task Handle_QuandoPessoaNaoExistir_DeveRetornarErro()
    {
        // Arrange
        var pessoaId = 999; // ID inválido que não existe no repositório
        var request = new CriarConsultaRequest
        {
            Status = true,
            Data = DateTime.Now,
            IdPessoa = pessoaId
        };

        _consultaRepository.ObterPessoaConsultaAsync(request.IdPessoa).Returns(Task.FromResult<PessoaEntity>(null));

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<Result<CriarConsultaResponse>>()
            .Which.IsSuccess.Should().BeFalse();

        result.Exception.Should().BeOfType<ExcecaoAplicacao>()
            .Which.ResultadoErro.Should().Be(CadastroErros.IdPessoaInvalido);

        await _consultaRepository.Received(1).ObterPessoaConsultaAsync(request.IdPessoa);
        await _consultaRepository.DidNotReceive().CriarConsultaAsync(Arg.Any<ConsultaEntity>());
    }
}

