using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
public class DeletarConsultaRequestHandler :
    IRequestHandler<DeletarConsultaRequest, Result<DeletarConsultaResponse>>
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly ILogger<DeletarConsultaRequestHandler> _logger;

    public DeletarConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<DeletarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarConsultaResponse>> Handle(DeletarConsultaRequest request, CancellationToken cancellationToken)
    {
        var consultaEntity = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consultaEntity == null)
        {
            return Result.Error<DeletarConsultaResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        await _consultaRepository.DeletarConsultaAsync(consultaEntity.Id);

        return Result.Success(new DeletarConsultaResponse(consultaEntity.Id));
    }
}