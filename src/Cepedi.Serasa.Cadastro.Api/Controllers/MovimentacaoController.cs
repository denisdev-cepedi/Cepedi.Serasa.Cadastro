using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Movimentacao;
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

    [HttpGet]
    [ProducesResponseType(typeof(List<ObterTodasMovimentacoesResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ObterTodasMovimentacoesResponse>>> ObterTodasMovimentacoesAsync()
        => await SendCommand(new ObterTodasMovimentacoesRequest());

    [HttpGet("{Id}")]
    [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterMovimentacaoResponse>> ObterPessoaAsync(
        [FromRoute] ObterMovimentacaoRequest request) => await SendCommand(request);

    [HttpPost]
    [ProducesResponseType(typeof(CriarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarMovimentacaoResponse>> CriarPessoaAsync([FromBody] CriarMovimentacaoRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarMovimentacaoResponse>> AtualizarPessoaAsync(
        [FromBody] AtualizarMovimentacaoRequest request) => await SendCommand(request);

    [HttpDelete("{Id}")]
    [ProducesResponseType(typeof(DeletarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarMovimentacaoResponse>> DeletarPessoaAsync(
        [FromRoute] DeletarMovimentacaoRequest request) => await SendCommand(request);
}