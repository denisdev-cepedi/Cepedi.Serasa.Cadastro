﻿using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Serasa.Cadastro.Dados.EntityTypeConfiguration
{
    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<PessoaEntity>
    {
        public void Configure(EntityTypeBuilder<PessoaEntity> builder)
        {
            builder.ToTable("Pessoa");
            builder.HasKey(pessoa => pessoa.Id);

            builder.Property(pessoa => pessoa.Nome).IsRequired().HasMaxLength(100);
            builder.Property(pessoa => pessoa.CPF).IsRequired().HasMaxLength(11);

            // Se necessário, adicione outras configurações da entidade PessoaEntity aqui
        }
    }
}
