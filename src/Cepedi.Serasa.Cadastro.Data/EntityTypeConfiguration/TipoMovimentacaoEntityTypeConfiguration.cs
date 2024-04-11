using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration;
public class TipoMovimentacaoTypeConfiguration : IEntityTypeConfiguration<TipoMovimentacaoEntity>
{
    public void Configure(EntityTypeBuilder<TipoMovimentacaoEntity> builder)
    {
        builder.ToTable("TipoMovimentacao");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.NomeTipo).IsRequired().HasMaxLength(255);
    }
}

