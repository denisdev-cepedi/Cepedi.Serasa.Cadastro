using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Pessoa;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.Pessoa;

public class AtualizarPessoaRequestValidator : AbstractValidator<AtualizarPessoaRequest>
{
    public AtualizarPessoaRequestValidator()
    {
        RuleFor(pessoa => pessoa.Id)
            .NotNull()
            .WithMessage("O Id é obrigatório.");

        When(pessoa => pessoa.CPF is not null, () =>
        {
            RuleFor(pessoa => pessoa.CPF)
            .Matches("^[0-9]{11}$")
            .WithMessage("O CPF deve conter 11 digitos de 0 a 9.");
        });
    }
}
