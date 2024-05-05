using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers.Consulta;
public class AtualizarConsultaRequestHandler : IRequestHandler<AtualizarConsultaRequest, Result<AtualizarConsultaResponse>>
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly ILogger<AtualizarConsultaRequestHandler> _logger;

    public AtualizarConsultaRequestHandler(IConsultaRepository consultaRepository, ILogger<AtualizarConsultaRequestHandler> logger)
    {
        _consultaRepository = consultaRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarConsultaResponse>> Handle(AtualizarConsultaRequest request, CancellationToken cancellationToken)
    {
        var consultaEntity = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consultaEntity == null)
        {
            return Result.Error<AtualizarConsultaResponse>(new Compartilhado.
                Exececoes.SemResultadoExcecao());
        }

        consultaEntity.Atualizar(request.Status);

        await _consultaRepository.AtualizarConsultaAsync(consultaEntity);

        return Result.Success(new AtualizarConsultaResponse(consultaEntity.Id, consultaEntity.IdPessoa, consultaEntity.Status, consultaEntity.Data));
    }
}
