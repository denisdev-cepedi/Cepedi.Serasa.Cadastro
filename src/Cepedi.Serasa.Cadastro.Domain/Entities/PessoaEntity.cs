using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepedi.Serasa.Cadastro.Domain.Entities;
public class PessoaEntity
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string CPF { get; set; }
    public ScoreEntity Score { get; set; }
    public int IdScore { get; set; }
     public ConsultaEntity Consulta { get; set; }
    public int IdConsulta { get; set; }
}
