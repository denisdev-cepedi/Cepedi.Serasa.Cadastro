﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cepedi.Serasa.Cadastro.Dominio.Repository;
using Cepedi.Serasa.Cadastro.Compartilhado.Requests;
using Cepedi.Serasa.Cadastro.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Serasa.Cadastro.Dominio.Handlers;
public class AtualizarUsuarioRequestHandler :
    IRequestHandler<AtualizarUsuarioRequest, Result<AtualizarUsuarioResponse>>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILogger<AtualizarUsuarioRequestHandler> _logger;

    public AtualizarUsuarioRequestHandler(IUsuarioRepository usuarioRepository, ILogger<AtualizarUsuarioRequestHandler> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    public async Task<Result<AtualizarUsuarioResponse>> Handle(AtualizarUsuarioRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var usuarioEntity = await _usuarioRepository.ObterUsuarioAsync(request.Id);

            if (usuarioEntity == null)
            {
                return Result.Error<AtualizarUsuarioResponse>(new Compartilhado.
                    Exececoes.SemResultadoExcecao());
            }

            usuarioEntity.Atualizar(request.Nome);

            await _usuarioRepository.AtualizarUsuarioAsync(usuarioEntity);

            return Result.Success(new AtualizarUsuarioResponse(usuarioEntity.Nome));
        }
        catch
        {
            _logger.LogError("Ocorreu um erro ao atualizar os usuários");
            throw;
        }
    }
}
