using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Pipelines
{
    public sealed class ExcecaoPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<ExcecaoPipeline<TRequest, TResponse>> _logger;

        public ExcecaoPipeline(ILogger<ExcecaoPipeline<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogInformation("Request handler {dados}", request);
                }

                var response = await next();

                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception occurred during the request handling.");

                var erro = Result.Error<TResponse>(e);

                var retorno = AlterarPropriedadeComReflection<TResponse>(erro, e);
                return retorno != null ? (TResponse)retorno : default!;
            }
        }

        private static object? AlterarPropriedadeComReflection<T>(Result<T> erro, Exception exception)
        {
            var valueProperty = typeof(Result<T>).GetProperty("Value", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (valueProperty != null)
            {
                var value = valueProperty.GetValue(erro);

                if (value != null)
                {
                    Type valueType = value.GetType();
                    var exceptionField = valueType.GetField("_exception", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (exceptionField != null)
                    {
                        exceptionField.SetValue(value, exception);
                    }
                }

                return value;
            }

            return erro;
        }
    }
}
