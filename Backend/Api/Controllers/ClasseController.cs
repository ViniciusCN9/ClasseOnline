using System;
using System.Linq;
using System.Security.Claims;
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
    }
}