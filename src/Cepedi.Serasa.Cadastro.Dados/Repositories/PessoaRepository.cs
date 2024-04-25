using Cepedi.Serasa.Cadastro.Domain.Repositorio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories;
public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PessoaEntity> AtualizarPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoas.Update(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<PessoaEntity> CriarPessoaAsync(PessoaEntity pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task ExcluirPessoaAsync(PessoaEntity pessoa)
    {
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
    }

    public async Task<PessoaEntity> ObterPessoaAsync(int id)
        => await _context.Pessoas.Where(pessoa => pessoa.Id == id).FirstOrDefaultAsync();

    public async Task<List<PessoaEntity>> ObterPessoasAsync()
        => await _context.Pessoas.ToListAsync();
}
