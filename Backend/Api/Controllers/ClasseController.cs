using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasseController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IClasseService _classeService;

        public ClasseController(IHttpContextAccessor accessor, IClasseService classeService)
        {
            _accessor = accessor;
            _classeService = classeService;
        }

        [HttpGet]
        [Authorize(Roles = "PROFESSOR, ALUNO")]
        public IActionResult GetClasses()
        {
            try
            {
                var usuarioId = Convert.ToInt16(_accessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Sid).Value);
                var response = _classeService.CarregarClasses(usuarioId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{nome}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult PostClasse([FromRoute] string nome)
        {
            try
            {
                var usuarioId = Convert.ToInt16(_accessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Sid).Value);
                var response = _classeService.CriarClasse(HttpUtility.UrlDecode(nome), usuarioId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{codigo}/{nome}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult UpdateClasse([FromRoute] string codigo, [FromRoute] string nome)
        {
            try
            {
                _classeService.AtualizarClasse(codigo, HttpUtility.UrlDecode(nome));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{codigo}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult DeleteClasse([FromRoute] string codigo)
        {
            try
            {
                _classeService.RemoverClasse(codigo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}