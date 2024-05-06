using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Dados.EntityTypeConfiguration
{
    public class ConsultaEntityTypeConfiguration : IEntityTypeConfiguration<ConsultaEntity>
    {
        public void Configure(EntityTypeBuilder<ConsultaEntity> builder)
        {
            builder.ToTable("Consulta");
            builder.HasKey(consulta => consulta.Id);

            builder.Property(consulta => consulta.IdPessoa).IsRequired();
            builder.Property(consulta => consulta.Data).IsRequired();
            builder.Property(consulta => consulta.Status).IsRequired();

            // Configurando o relacionamento com PessoaEntity
            builder.HasOne(consulta => consulta.Pessoa)
                   .WithMany()  // Uma pessoa pode ter várias consultas
                   .HasForeignKey(consulta => consulta.IdPessoa)  // Chave estrangeira
                   .IsRequired();  // É obrigatório ter uma pessoa associada a uma consulta
        }
    }
}
