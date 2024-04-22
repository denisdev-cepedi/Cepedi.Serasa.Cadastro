using System;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Cepedi.Serasa.Cadastro.Compartilhado.Exececoes;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Enums;

namespace Cepedi.Serasa.Cadastro.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentacoesController : BaseController
    {
        private readonly ILogger<MovimentacoesController> _logger;
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacoesController(
            ILogger<MovimentacoesController> logger,
            IMediator mediator,
            IMovimentacaoRepository movimentacaoRepository)
            : base(mediator)
        {
            _logger = logger;
            _movimentacaoRepository = movimentacaoRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterMovimentacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObterMovimentacaoResponse>> ObterMovimentacaoAsync(int id)
        {
            var movimentacao = await _movimentacaoRepository.ObterMovimentacaoAsync(id);

            if (movimentacao == null)
            {
                return NotFound(new ResultadoErro
                {
                    Titulo = "Movimentação não encontrada",
                    Descricao = $"A movimentação com ID {id} não foi encontrada.",
                    Tipo = ETipoErro.Erro
                });
            }

            var response = new ObterMovimentacaoResponse(movimentacao.MovimentacaoId, movimentacao.Valor);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CriarMovimentacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CriarMovimentacaoResponse>> CriarMovimentacaoAsync(
            [FromBody] CriarMovimentacaoRequest request)
        {
            return await SendCommand<CriarMovimentacaoResponse>(request);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AtualizarMovimentacaoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AtualizarMovimentacaoResponse>> AtualizarMovimentacaoAsync(
            int id, [FromBody] AtualizarMovimentacaoRequest request)
        {
            request.MovimentacaoId = id;
            return await SendCommand<AtualizarMovimentacaoResponse>(request);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarMovimentacaoAsync(int id)
        {
            try
            {
                var movimentacaoEntity = await _movimentacaoRepository.ObterMovimentacaoAsync(id);

                if (movimentacaoEntity == null)
                {
                    return NotFound();
                }

                await _movimentacaoRepository.DeletarMovimentacaoAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar movimentação");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
