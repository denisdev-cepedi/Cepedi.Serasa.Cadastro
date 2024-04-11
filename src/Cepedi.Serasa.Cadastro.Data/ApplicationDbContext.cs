﻿using System.Diagnostics.CodeAnalysis;
using Cepedi.Serasa.Cadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
