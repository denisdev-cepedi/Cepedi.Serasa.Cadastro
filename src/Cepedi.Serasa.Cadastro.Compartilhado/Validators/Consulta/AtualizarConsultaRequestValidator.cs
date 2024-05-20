using Cepedi.Serasa.Cadastro.Compartilhado.Requests.Consulta;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.Consulta;

public class AtualizarConsultaRequestValidator : AbstractValidator<AtualizarConsultaRequest>
{

    public AtualizarConsultaRequestValidator()
    {
        RuleFor(consulta => consulta.Id)
            .NotNull().WithMessage("O Id é obrigatório.")
            .GreaterThan(0).WithMessage("O Id de consulta inválido.");

        RuleFor(consulta => consulta.Status)
            .NotNull().WithMessage("O status é obrigatório.")
            .Must(status => status == true || status == false).WithMessage("O status deve ser true ou false");

        RuleFor(consulta => consulta.Data)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .Must(dataHora => dataHora != default(DateTime)).WithMessage("Data deve ser valida");

    }
}
