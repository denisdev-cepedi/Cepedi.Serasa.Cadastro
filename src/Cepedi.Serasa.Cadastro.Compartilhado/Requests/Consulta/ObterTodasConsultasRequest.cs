using Cepedi.Serasa.Cadastro.Compartilhado.Responses.Consulta;
using MediatR;
using OperationResult;
using System.Collections.Generic;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta
{
    public class ObterTodasConsultasRequest : IRequest<Result<List<ObterTodasConsultasResponse>>>
    {
    }
}
