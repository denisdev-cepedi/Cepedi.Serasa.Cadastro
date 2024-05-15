using FluentValidation;
using System;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao.Validators
{
    public class CriarMovimentacaoRequestValidation : AbstractValidator<CriarMovimentacaoRequest>
    {
        public CriarMovimentacaoRequestValidation()
        {
            RuleFor(movimentacao => movimentacao.IdTipoMovimentacao)
                .NotNull().WithMessage("O ID do tipo de movimentação deve ser informado")
                .GreaterThan(0).WithMessage("ID do tipo de movimentação inválido");

            RuleFor(movimentacao => movimentacao.IdPessoa)
                .NotNull().WithMessage("O ID da pessoa deve ser informado")
                .GreaterThan(0).WithMessage("ID da pessoa inválido");

            RuleFor(movimentacao => movimentacao.DataHora)
                .NotEmpty().WithMessage("A data e hora devem ser informadas")
                .Must(dataHora => dataHora != default(DateTime)).WithMessage("Data e hora devem ser válidas");

            RuleFor(movimentacao => movimentacao.NomeEstabelecimento)
                .NotEmpty().WithMessage("O nome do estabelecimento deve ser informado")
                .MinimumLength(3).WithMessage("Nome do estabelecimento deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome do estabelecimento deve ter no máximo 100 caracteres");

            RuleFor(movimentacao => movimentacao.Valor)
                .NotNull().WithMessage("O valor deve ser informado")
                .GreaterThanOrEqualTo(0).WithMessage("O valor de movimentação é inválido");
        }
    }
}
