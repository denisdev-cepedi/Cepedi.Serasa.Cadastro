using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class Movimentacao : BaseController
{
    private readonly ILogger<Movimentacao> _logger;
    private readonly IMediator _mediator;
    public Movimentacao(ILogger<Movimentacao> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarMovimentacaoResponse>> CriarMovimentacaoAsync([FromBody] CriarMovimentacaoRequest request) => await SendCommand(request);

    [HttpPut("{Id}")]
    [ProducesResponseType(typeof(AtualizarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarMovimentacaoResponse>> AtualizarMovimentacaoAsync(
        [FromBody] AtualizarMovimentacaoRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterMovimentacaoResponse>> ObterMovimentacaoAsync(
        [FromRoute] ObterMovimentacaoRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarMovimentacaoResponse>> DeletarMovimentacaoAsync(
        [FromRoute] DeletarMovimentacaoRequest request) => await SendCommand(request);
}