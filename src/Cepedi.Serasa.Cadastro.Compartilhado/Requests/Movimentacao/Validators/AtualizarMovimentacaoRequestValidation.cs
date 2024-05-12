using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao.Validators
{
    public class AtualizarMovimentacaoRequestValidation : AbstractValidator<AtualizarMovimentacaoRequest>
    {
        private readonly IValidacao _validacao;

        public AtualizarMovimentacaoRequestValidation(IValidacao validacao)
        {
            _validacao = validacao;

            RuleFor(movimentacao => movimentacao.Id)
                .NotEmpty().WithMessage("O ID deve ser informado")
                .GreaterThan(0).WithMessage("ID de movimentação inválido");

            RuleFor(movimentacao => movimentacao.IdTipoMovimentacao)
                .NotEmpty().WithMessage("O ID do tipo de movimentação deve ser informado")
                .GreaterThan(0).WithMessage("ID do tipo de movimentação inválido");

            RuleFor(movimentacao => movimentacao.DataHora)
                .NotEmpty().WithMessage("A data e hora devem ser informadas")
                .MustAsync(async (movimentacao, dataHora, cancellationToken) => await _validacao.BeValidDateTimeAsync(dataHora))
                .WithMessage("Data e hora informadas são inválidas");

            RuleFor(movimentacao => movimentacao.NomeEstabelecimento)
                .NotEmpty().WithMessage("O nome do estabelecimento deve ser informado")
                .MinimumLength(3).WithMessage("Nome do estabelecimento muito curto")
                .MaximumLength(100).WithMessage("Nome do estabelecimento muito longo");

            RuleFor(movimentacao => movimentacao.Valor)
                .NotEmpty().WithMessage("O valor deve ser informado")
                .MustAsync(async (valor, cancellationToken) => await _validacao.ValidarValorTransacaoAsync(valor))
                .WithMessage("O valor de movimentação é inválido ou não foi especificado corretamente");
        }
    }
}
