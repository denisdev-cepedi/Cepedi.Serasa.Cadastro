using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Servicos;
public interface ICache<T>
{
    Task<T> ObterAsync(string chave);
    Task SalvarAsync(string chave, T objeto, int tempoExpiracao = 10);
}
