using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

        [HttpGet("download/{id}")]
        [Authorize(Roles = "PROFESSOR, ALUNO")]
        public async Task<IActionResult> DownloadAnexo([FromRoute] Guid id)
        {
            try
            {
                var anexo = _anexoService.CarregarAnexo(id);
                var filePath = anexo.Url;
                if (!System.IO.File.Exists(filePath))
                    return NotFound();

                var memoryStream = new MemoryStream();
                using (var stream = System.IO.File.Open(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;

                return File(memoryStream, GetContentType(filePath), filePath);
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

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}