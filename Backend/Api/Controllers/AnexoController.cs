using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnexoController : ControllerBase
    {
        private readonly IAnexoService _anexoService;

        public AnexoController(IAnexoService anexoService)
        {
            _anexoService = anexoService;
        }

        [HttpGet("{idPostagem}")]
        [Authorize(Roles = "PROFESSOR, ALUNO")]
        public IActionResult GetAnexos([FromRoute] Guid idPostagem)
        {
            try
            {
                var anexos = _anexoService.CarregarAnexos(idPostagem);
                return Ok(anexos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult PostAnexo(IFormFile arquivo)
        {
            try
            {
                _anexoService.AdicionarAnexo(arquivo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "PROFESSOR")]
        public IActionResult DeleteAnexo([FromRoute] Guid id)
        {
            try
            {
                _anexoService.RemoverAnexo(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}