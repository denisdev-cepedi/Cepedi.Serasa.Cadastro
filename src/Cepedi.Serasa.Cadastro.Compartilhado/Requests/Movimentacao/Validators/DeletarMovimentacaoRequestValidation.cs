using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao.Validators
{
    public class DeletarMovimentacaoRequestValidation : AbstractValidator<DeletarMovimentacaoRequest>
    {
        private readonly IValidacao _valida;

        public DeletarMovimentacaoRequestValidation(IValidacao valida)
        {
            _valida = valida;

            RuleFor(movimentacao => movimentacao.Id)
                .NotEmpty().WithMessage("O ID deve ser informado")
                .GreaterThan(0).WithMessage("ID de movimentação inválido");
        }
    }
}
