using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Service.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IHttpContextAccessor _accessor;

        public PostagemController(IHttpContextAccessor accessor, IPostagemService postagemService)
        {
            _accessor = accessor;
            _postagemService = postagemService;
        }

        [HttpGet("{codigo}")]
        [Authorize(Roles = "PROFESSOR, ALUNO")]
        public IActionResult GetPostagens([FromRoute] string codigo)
        {
            try
            {
                var postagens = _postagemService.CarregarPostagens(codigo);
                return Ok(postagens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{codigo}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult PostPostagem([FromBody] PostagemRequest request, [FromRoute] string codigo)
        {
            try
            {
                var usuarioId = Guid.Parse(_accessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Sid).Value);
                request.Usuario = usuarioId;

                _postagemService.AdicionarPostagem(request, codigo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{codigo}/{id}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult DeletePostagem([FromRoute] string codigo, [FromRoute] Guid id)
        {
            try
            {
                _postagemService.RemoverPostagem(id, codigo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}