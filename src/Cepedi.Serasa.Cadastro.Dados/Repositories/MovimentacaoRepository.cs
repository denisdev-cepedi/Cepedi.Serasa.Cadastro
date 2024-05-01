using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Dados.Repositories;
public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly ApplicationDbContext _context;

    public MovimentacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoEntity> ObterMovimentacaoAsync(int id)
        => await _context.Movimentacao.Where(movimentacao => movimentacao.Id == id).FirstOrDefaultAsync();

    public async Task<List<MovimentacaoEntity>> ObterMovimentacoesAsync()
        => await _context.Movimentacao.ToListAsync();

    public async Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao)
    {
        await _context.Movimentacao.AddAsync(movimentacao);
        await _context.SaveChangesAsync();

        return movimentacao;
    }

    public async Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao)
    {
        _context.Movimentacao.Update(movimentacao);
        await _context.SaveChangesAsync();

        return movimentacao;
    }

    public async Task DeletarMovimentacaoAsync(MovimentacaoEntity movimentacao)
    {
        _context.Movimentacao.Remove(movimentacao);
        await _context.SaveChangesAsync();
    }

}
