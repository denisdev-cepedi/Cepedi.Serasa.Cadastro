﻿using Cepedi.Serasa.Cadastro.Compartilhado.Enums;
using Cepedi.Serasa.Cadastro.Compartilhado.Excecoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Api.Controllers;
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<ActionResult> SendCommand(IRequest<Result> request, int statusCode = 200)
        => await _mediator.Send(request) switch
        {
            (true, _) => StatusCode(statusCode),
            var (_, error) => HandleError(error!)
        };

    protected async Task<ActionResult<T>> SendCommand<T>(IRequest<Result<T>> request, int statusCode = 200)
        => await _mediator.Send(request).ConfigureAwait(false) switch
        {
            (true, var result, _) => StatusCode(statusCode, result),
            var (_, res, error) => HandleError(error!)
        };

    protected ActionResult HandleError(Exception error) => error switch
    {
        SemResultadoExcecao e => NoContent(),
        RequestInvalidaExcecao e => BadRequest(FormatErrorMessage(e.ResultadoErro, e.Erros)),
        _ => BadRequest(FormatErrorMessage(CadastroErros.Generico))
    };

    private ResultadoErro FormatErrorMessage(ResultadoErro responseErro, IEnumerable<string>? errors = null)
    {
        if (errors != null)
        {
            responseErro.Descricao = $"{responseErro.Descricao} : {string.Join("; ", errors!)}";
        }

        return responseErro;
    }
}
