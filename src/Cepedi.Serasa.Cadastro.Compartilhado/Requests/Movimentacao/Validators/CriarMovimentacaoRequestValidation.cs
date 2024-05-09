using FluentValidation;

namespace Cepedi.Serasa.Cadastro.Compartilhado.Requests.Movimentacao.Validators
{
    public class CriarMovimentacaoRequestValidation : AbstractValidator<CriarMovimentacaoRequest>
    {
        private readonly IValidacao _validacao;

        public CriarMovimentacaoRequestValidation(IValidacao validacao)
        {
            _validacao = validacao;

            RuleFor(movimentacao => movimentacao.DataHora)
                .NotEmpty().WithMessage("A data e hora devem ser informadas")
                .MustAsync(async (dataHora, cancellationToken) => await _validacao.BeValidDateTimeAsync(dataHora))
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
