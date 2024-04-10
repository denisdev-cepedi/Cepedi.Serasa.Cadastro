using Cepedi.Serasa.Cadastro.Domain.Entities;

namespace Cepedi.Serasa.Cadastro.Domain.Repository;

public interface IUsuarioRepository
{
    Task<UsuarioEntity> CriarUsuarioAsync(UsuarioEntity usuario);
    Task<UsuarioEntity> ObterUsuarioAsync(int id);

    Task<UsuarioEntity> AtualizarUsuarioAsync(UsuarioEntity usuario);
}
