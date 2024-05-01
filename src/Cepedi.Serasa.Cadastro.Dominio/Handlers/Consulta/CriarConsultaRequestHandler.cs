using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
public class CriarConsultaRequestHandler
    : IRequestHandler<CriarConsultaRequest, Result<CriarConsultaResponse>>
{
    private readonly ILogger<CriarConsultaRequestHandler> _logger;
    private readonly IConsultaRepository _consultaRepository;

    public CriarConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<CriarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<CriarConsultaResponse>> Handle(CriarConsultaRequest request, CancellationToken cancellationToken)
    {
        var credorEntity = await _consultaRepository.ObterPessoaConsultaAsync(request.IdPessoa);

        var consulta = new ConsultaEntity()
        {
            Status = request.Status,
            Data = request.Data,
            IdPessoa = request.IdPessoa,
        };

        await _consultaRepository.CriarConsultaAsync(consulta);

        return Result.Success(new CriarConsultaResponse(consulta.Id, consulta.Status));
    }
}