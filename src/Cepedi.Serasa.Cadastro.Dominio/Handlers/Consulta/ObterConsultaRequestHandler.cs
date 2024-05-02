using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
public class ObterConsultaRequestHandler :
    IRequestHandler<ObterConsultaRequest, Result<ObterConsultaResponse>>
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly ILogger<ObterConsultaRequestHandler> _logger;

    public ObterConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<ObterConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<ObterConsultaResponse>> Handle(ObterConsultaRequest request, CancellationToken cancellationToken)
    {
        var consultaEntity = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consultaEntity == null)
        {
            return Result.Error<ObterConsultaResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        return Result.Success(new ObterConsultaResponse(consultaEntity.Id, consultaEntity.Status, consultaEntity.Data));

    }
}