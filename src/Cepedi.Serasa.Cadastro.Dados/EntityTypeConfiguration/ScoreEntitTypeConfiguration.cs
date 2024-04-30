using Cepedi.Serasa.Cadastro.Dominio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Dados;

public class ScoreEntitTypeConfiguration : IEntityTypeConfiguration<ScoreEntity>
{
    public void Configure(EntityTypeBuilder<ScoreEntity> builder)
    {
        builder.ToTable("Score");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Score).IsRequired();
        builder.Property(e => e.IdPessoa).IsRequired();

        //builder.HasOne(e => e.Pessoa)
        //       .WithOne() 
        //       .HasForeignKey(e => e.IdPessoa);
    }
}
