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

        RuleFor(pessoa => pessoa.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(3)
            .WithMessage("O nome deve ter pelo menos 3 caracteres.");

        RuleFor(pessoa => pessoa.CPF)
            .Matches("^[0-9]{11}$")
            .WithMessage("O CPF deve conter 11 digitos de 0 a 9.");
    }
}
