using Cepedi.Serasa.Cadastro.Dominio;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario)
        {
            _context.Usuario.Update(usuario);

            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario)
        {
            _context.Usuario.Add(usuario);
            
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<List<UsuarioEntity>> GetUsuariosAsync()
        {
            return await _context.Usuario.ToListAsync();
        }

        public async Task<UsuarioEntity> ObterUsuarioAsync(int id)
        {
            return await
                _context.Usuario.Where(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
