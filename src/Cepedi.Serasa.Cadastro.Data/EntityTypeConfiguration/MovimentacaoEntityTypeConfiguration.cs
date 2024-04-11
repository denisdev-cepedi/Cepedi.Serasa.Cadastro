using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration
{
    public class MovimentacaoEntityTypeConfiguration : IEntityTypeConfiguration<MovimentacaoEntity>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoEntity> builder)
        {
            builder.ToTable("Movimentacao");
            builder.HasKey(c => c.MovimentacaoId);

            builder.Property(c => c.PessoaId).IsRequired();
            builder.Property(c => c.DataHora).IsRequired(); // Renomeada para DataHora
            builder.Property(c => c.TipoMovimentacaoId).IsRequired();
            builder.Property(c => c.Valor).IsRequired();

            builder.Property(c => c.NomeEstabelecimento).HasMaxLength(255); // Renomeada para NomeEstabelecimento

            builder.HasOne(c => c.TipoMovimentacao)
                   .WithMany()
                   .HasForeignKey(c => c.TipoMovimentacaoId);
        }
    }
}
