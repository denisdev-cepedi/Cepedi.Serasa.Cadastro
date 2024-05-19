using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.Score;
public class AtualizarScoreRequestValidaton : AbstractValidator<AtualizarScoreRequest>
{
    public AtualizarScoreRequestValidaton()
    {
        RuleFor(score => score.Id)
            .NotNull().WithMessage("O ID deve ser informado")
            .GreaterThan(0).WithMessage("ID de score inválido");

        RuleFor(score => score.Score)
            .NotNull().WithMessage("O Score deve ser informado")
            .GreaterThanOrEqualTo(0).WithMessage("O valor deve ser maior ou igual a zero")
            .LessThanOrEqualTo(1000)
            .WithMessage("O valor deve ser menor ou igual a mil");
    }
}
