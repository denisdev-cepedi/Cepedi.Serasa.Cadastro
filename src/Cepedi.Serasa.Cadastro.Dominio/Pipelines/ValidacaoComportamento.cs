﻿using Cepedi.Serasa.Cadastro.Compartilhado;
using Cepedi.Serasa.Cadastro.Compartilhado.Excecoes;
using FluentValidation;
using MediatR;

namespace Cepedi.Serasa.Cadastro.Domain.Pipelines;
public sealed class ValidacaoComportamento<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IValida
{
    private AbstractValidator<TRequest> _validator;
    public ValidacaoComportamento(AbstractValidator<TRequest> validator) => _validator = validator;
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await _validator.ValidateAsync(request, cancellationToken);

        if (resultadoValidacao.IsValid)
        {
            return await next.Invoke();
        }

        var context = new ValidationContext<TRequest>(request);
        var erros = resultadoValidacao
            .Errors
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);

        if (erros.Any())
        {
            throw new RequestInvalidaExcecao(erros);
        }

        return await next();
    }
}
