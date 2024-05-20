using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
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
        var consulta = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consulta == null)
        {
            return Result.Error<ObterConsultaResponse>(new Compartilhado
            .Exececoes.ExcecaoAplicacao(CadastroErros.IdConsultaInvalido));
        }

        var response = new ObterConsultaResponse(consulta.Id,
                                                consulta.IdPessoa,
                                                consulta.Status,
                                                consulta.Data);

        return Result.Success(response);

    }
}