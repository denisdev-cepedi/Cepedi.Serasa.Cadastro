using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovimentacaoController : BaseController
{
    private readonly IMediator _mediator;
    private readonly ILogger<MovimentacaoController> _logger;

    public MovimentacaoController(IMediator mediator, ILogger<MovimentacaoController> logger) : base(mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ObterMovimentacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ObterMovimentacaoResponse>>> ObterTodasMovimentacoesAsync()
        => await SendCommand(new ObterTodasMovimentacoesRequest());

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObterMovimentacaoResponse>> ObterMovimentacaoAsync(
        [FromRoute] int id)
    {
        var request = new ObterMovimentacaoRequest { Id = id };
        return await SendCommand(request);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarMovimentacaoResponse>> CriarMovimentacaoAsync(
        [FromBody] CriarMovimentacaoRequest request)
        => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarMovimentacaoResponse>> AtualizarMovimentacaoAsync(
        [FromBody] AtualizarMovimentacaoRequest request)
        => await SendCommand(request);

    [HttpDelete]
    [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObterMovimentacaoResponse>> DeletarMovimentacao(
        [FromBody] DeletarMovimentacaoRequest request)
        => await SendCommand(request);
}
