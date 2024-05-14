using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Validators;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.Consulta;

public class CriarConsultaRequestValidator : AbstractValidator<CriarConsultaRequest>
{
    private readonly IValidacao _validacao;
    public CriarConsultaRequestValidator()
    {
        RuleFor(consulta => consulta.Data)
            .NotEmpty()
            .WithMessage("A data e hora é obrigatória.")
            .MustAsync(async (dataHora, cancellationToken) => await _validacao.BeValidDateTimeAsync(dataHora))
            .WithMessage("Data e hora informadas são inválidas");

        RuleFor(consulta => consulta.Status)
            .NotEmpty()
            .WithMessage("O status é obrigatório.")
            .Must(status => status == true || status == false)
            .WithMessage("O status deve ser true ou false");

        RuleFor(consulta => consulta.IdPessoa)
            .NotNull()
            .WithMessage("O Id é obrigatório.")
            .GreaterThan(0)
            .WithMessage("O Id deve ser maior que zero");
    }
}
