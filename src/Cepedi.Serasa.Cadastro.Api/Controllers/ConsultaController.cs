using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsultaController : BaseController
{
    private readonly ILogger<ConsultaController> _logger;
    private readonly IMediator _mediator;
    public ConsultaController(ILogger<ConsultaController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ObterTodasConsultasResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ObterTodasConsultasResponse>>> ObterTodasConsultasAsync()
        => await SendCommand(new ObterTodasConsultasRequest());

    [HttpPost]
    [ProducesResponseType(typeof(CriarConsultaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarConsultaResponse>> CriarConsultaAsync(
        [FromBody] CriarConsultaRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarConsultaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarConsultaResponse>> AtualizarConsultaAsync(
        [FromBody] AtualizarConsultaRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterConsultaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterConsultaResponse>> ObterConsultaAsync(
        [FromRoute] ObterConsultaRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarConsultaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarConsultaResponse>> DeletarConsultaAsync(
        [FromRoute] DeletarConsultaRequest request) => await SendCommand(request);
}
