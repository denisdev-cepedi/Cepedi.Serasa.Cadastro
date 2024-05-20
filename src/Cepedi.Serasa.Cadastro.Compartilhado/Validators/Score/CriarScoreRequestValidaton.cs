using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Score;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.Score;
public class CriarScoreRequestValidaton : AbstractValidator<CriarScoreRequest>
{
    public CriarScoreRequestValidaton()
    {
        RuleFor(score => score.IdPessoa)
            .NotNull().WithMessage("O ID da pessoa deve ser informado")
            .GreaterThan(0).WithMessage("ID da pessoa inválido");

        RuleFor(score => score.Score)
            .NotNull().WithMessage("O Score deve ser informado")
            .GreaterThanOrEqualTo(0).WithMessage("O valor deve ser maior ou igual a zero")
            .LessThanOrEqualTo(1000)
            .WithMessage("O valor deve ser menor ou igual a mil");
    }
}
