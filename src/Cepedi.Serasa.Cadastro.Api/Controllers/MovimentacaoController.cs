using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class MovimentacoesController : BaseController
{
    private readonly ILogger<MovimentacoesController> _logger;
    private readonly IMovimentacaoRepository _movimentacaoRepository;

    public MovimentacoesController(
        ILogger<MovimentacoesController> logger,
        IMediator mediator,
        IMovimentacaoRepository movimentacaoRepository)
        : base(mediator)
    {
        _logger = logger;
        _movimentacaoRepository = movimentacaoRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarMovimentacaoResponse>> CriarMovimentacaoAsync(
        [FromBody] CriarMovimentacaoRequest request) => await SendCommand<CriarMovimentacaoResponse>(request);

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AtualizarMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AtualizarMovimentacaoResponse>> AtualizarMovimentacaoAsync(
        int id, [FromBody] AtualizarMovimentacaoRequest request)
    {
        request.MovimentacaoId = id;
        return await SendCommand<AtualizarMovimentacaoResponse>(request);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObterMovimentacaoResponse>> ObterMovimentacaoAsync(int id)
    {
        var movimentacao = await _movimentacaoRepository.ObterMovimentacaoAsync(id);

        if (movimentacao == null)
        {
            return NotFound(new ResultadoErro
            {
                Titulo = "Movimentação não encontrada",
                Descricao = $"A movimentação com ID {id} não foi encontrada.",
                Tipo = ETipoErro.Erro
            });
        }

        var response = new ObterMovimentacaoResponse(movimentacao.MovimentacaoId, movimentacao.Valor);
        return Ok(response);
    }

}
