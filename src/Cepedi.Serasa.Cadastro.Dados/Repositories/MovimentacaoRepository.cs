using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public MovimentacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MovimentacaoEntity> CriarMovimentacaoAsync(MovimentacaoEntity movimentacao)
        {
            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();
            return movimentacao;
        }

        public async Task<MovimentacaoEntity> AtualizarMovimentacaoAsync(MovimentacaoEntity movimentacao)
        {
            _context.Movimentacoes.Update(movimentacao);
            await _context.SaveChangesAsync();
            return movimentacao;
        }

        public async Task<MovimentacaoEntity?> ObterMovimentacaoAsync(int id)
        {
            return await _context.Movimentacoes.FindAsync(id);
        }

        public async Task<List<MovimentacaoEntity>> ListarMovimentacoesAsync()
        {
            return await _context.Movimentacoes.ToListAsync();
        }

        public async Task DeletarMovimentacaoAsync(int movimentacaoId)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(movimentacaoId);

            if (movimentacao != null)
            {
                _context.Movimentacoes.Remove(movimentacao);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Movimentação com ID {movimentacaoId} não encontrada para exclusão.");
            }
        }
    }
}
