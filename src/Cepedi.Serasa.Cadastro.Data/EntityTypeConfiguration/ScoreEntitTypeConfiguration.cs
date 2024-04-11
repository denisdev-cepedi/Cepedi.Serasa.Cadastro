using Cepedi.Serasa.Cadastro.Domain;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data;

public class ScoreEntitTypeConfiguration : IEntityTypeConfiguration<ScoreEntity>
{
    public void Configure(EntityTypeBuilder<ScoreEntity> builder)
    {
        builder.ToTable("Score");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Score).IsRequired();
        builder.Property(e => e.IdPessoa).IsRequired();

        builder.HasOne(e => e.Pessoa)
               .WithOne() 
               .HasForeignKey(e => e.IdPessoa);
    }
}
