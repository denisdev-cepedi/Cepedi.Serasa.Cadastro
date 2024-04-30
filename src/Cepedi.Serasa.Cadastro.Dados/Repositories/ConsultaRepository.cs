using Cepedi.Serasa.Cadastro.Dados;
using Cepedi.Serasa.Cadastro.Dominio.Entidades;
using Cepedi.Serasa.Cadastro.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Serasa.Cadastro.Dados.Repositories;
public class ConsultaRepository : IConsultaRepository
{
    private readonly ApplicationDbContext _context;

    public ConsultaRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ConsultaEntity> AtualizarConsultaAsync(ConsultaEntity status)
    {
        _context.Consulta.Update(status);

        await _context.SaveChangesAsync();

        return status;
    }

    public async Task<ConsultaEntity> CriarConsultaAsync(ConsultaEntity status)
    {
        _context.Consulta.Add(status);

        await _context.SaveChangesAsync();

        return status;
    }

    public async Task<List<ConsultaEntity>> GetConsultasAsync()
    {
        return await _context.Consulta.ToListAsync();
    }

    public async Task<ConsultaEntity> ObterConsultaAsync(int id)
    {
        return await
            _context.Consulta.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<PessoaEntity> ObterPessoaConsultaAsync(int id)
    {
        return await
            _context.Pessoa.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ConsultaEntity> DeletarConsultaAsync(int id)
    {
        var consultaEntity = await ObterConsultaAsync(id);

        if (consultaEntity == null)
        {
            return null;
        }

        _context.Consulta.Remove(consultaEntity);

        await _context.SaveChangesAsync();

        return consultaEntity;
    }
}
