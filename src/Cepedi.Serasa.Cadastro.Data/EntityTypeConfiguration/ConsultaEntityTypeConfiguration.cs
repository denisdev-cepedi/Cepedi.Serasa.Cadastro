using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Data.EntityTypeConfiguration;

public class ConsultaEntityTypeConfiguration : IEntityTypeConfiguration<ConsultaEntity>{
    public void Configure(EntityTypeBuilder<ConsultaEntity> builder) {
        builder.ToTable("Consulta");
        builder.HasKey(consulta => consulta.Id);
        builder.Property(consulta => consulta.IdPessoa);
        builder.Property(consulta => consulta.Data).IsRequired();
        builder.Property(consulta => consulta.Status).IsRequired();
    }
}