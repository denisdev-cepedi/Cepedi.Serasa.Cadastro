﻿using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoreController : BaseController
{
    private readonly ILogger<ScoreController> _logger;
    private readonly IMediator _mediator;

    public ScoreController(
        ILogger<ScoreController> logger, IMediator mediator)
        : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarScoreResponse>> CriarScoreAsync(
        [FromBody] CriarScoreRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarScoreResponse>> AtualizarScoreAsync(
        [FromBody] AtualizarScoreRequest request) => await SendCommand(request);

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ObterScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterScoreResponse>> ObterScoreAsync(
        [FromRoute] ObterScoreRequest request) => await SendCommand(request);

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeletarScoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarScoreResponse>> DeletarScoreAsync(
        [FromRoute] DeletarScoreRequest request) => await SendCommand(request);

}
