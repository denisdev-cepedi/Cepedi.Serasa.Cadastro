using Cepedi.Serasa.Cadastro.Compartilhado.Requests.TipoMovimentacao;
using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Validators.TipoMovimentacao;

public class AtualizarTipoMovimentacaoRequestValidation : AbstractValidator<AtualizarTipoMovimentacaoRequest>
{
    public AtualizarTipoMovimentacaoRequestValidation()
    {
        RuleFor(tipoMovimentacao => tipoMovimentacao.Id)
                .NotNull().WithMessage("O ID deve ser informado")
                .GreaterThan(0).WithMessage("ID do tipo de movimentação inválido");

        RuleFor(tipoMovimentacao => tipoMovimentacao.NomeTipo)
            .NotEmpty()
            .WithMessage("O nome do tipo de movimentação é obrigatório.")
            .MinimumLength(5)
            .WithMessage("O nome do tipo de movimentação deve ter pelo menos 5 caracteres.")
            .MaximumLength(30)
            .WithMessage("O nome do tipo de movimentação deve ter até 5 caracteres.");
    }
}