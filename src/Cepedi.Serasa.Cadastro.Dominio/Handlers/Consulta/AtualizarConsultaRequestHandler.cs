using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
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
        var consulta = await _consultaRepository.ObterConsultaAsync(request.Id);

        if (consulta == null)
        {
            return Result.Error<AtualizarConsultaResponse>(new Compartilhado.
                Exececoes.ExcecaoAplicacao(CadastroErros.IdConsultaInvalido));
        }

        consulta.Atualizar(request.Status);

        await _consultaRepository.AtualizarConsultaAsync(consulta);
        
        var response = new AtualizarConsultaResponse(consulta.Id, 
                                                    consulta.IdPessoa, 
                                                    consulta.Status, 
                                                    consulta.Data);

        return Result.Success(response);
    }
}
