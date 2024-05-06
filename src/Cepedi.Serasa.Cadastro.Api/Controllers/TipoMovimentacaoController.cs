using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.TipoMovimentacao;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("[controller]")]

public class TipoMovimentacao : BaseController
{
    private readonly ILogger<TipoMovimentacao> _logger;
    private readonly IMediator _mediator;
    public TipoMovimentacao(ILogger<TipoMovimentacao> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ObterTodosTiposMovimentacaoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ObterTodosTiposMovimentacaoResponse>>> ObterTodosTiposMovimentacaoAsync()
        => await SendCommand(new ObterTodosTiposMovimentacaoRequest());

    [HttpPost]
    [ProducesResponseType(typeof(CriarTipoMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarTipoMovimentacaoResponse>> CriarPessoaAsync([FromBody] CriarTipoMovimentacaoRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarTipoMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarTipoMovimentacaoResponse>> AtualizarPessoaAsync(
        [FromBody] AtualizarTipoMovimentacaoRequest request) => await SendCommand(request);

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterTipoMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterTipoMovimentacaoResponse>> ObterPessoaAsync(
        [FromRoute] ObterTipoMovimentacaoRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarTipoMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarTipoMovimentacaoResponse>> DeletarPessoaAsync(
        [FromRoute] DeletarTipoMovimentacaoRequest request) => await SendCommand(request);
}