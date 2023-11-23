using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedBrowBack.Models;
using RedBrowBack.Utilidades.Interfaces;


namespace RedBrowBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly RedBrowDbContext _dbcontext;
        private readonly IUtilidadesService _utilidadesService;

        public UsuarioController(
            RedBrowDbContext dbcontext,
            IUtilidadesService utilidadesService)
        {
            _dbcontext = dbcontext;
            _utilidadesService = utilidadesService;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista(int page = 1, int pageSize = 10, string sort = "idUsuario")
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                var query = _dbcontext.Usuarios.Include(r => r.oRol).OrderBy(u => u.IdUsuario);

                var totalCount = query.Count();

                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var usuarios = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                var metadata = new
                {
                    totalCount,
                    totalPages,
                    currentPage = page,
                    pageSize,
                    previousPage = page > 1 ? Url.Link(nameof(Lista), new {page = page - 1, pageSize, sort}) : null,
                    nextPage = page < totalPages ? Url.Link(nameof(Lista), new {page = page + 1, pageSize, sort}) : null
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return StatusCode(StatusCodes.Status200OK, new {mensaje = "OK", respose = usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, respose = new List<Usuario>() });
            }
        }


        [HttpGet]
        [Route("Obtener/{idUsuario:int}")]
        public IActionResult Obtener(int idUsuario)
        {
            Usuario oUsuario = _dbcontext.Usuarios.Find(idUsuario);

            if(oUsuario == null)
            {
                return BadRequest("Usuario no encontrado.");
            }

            try
            {
                oUsuario = _dbcontext.Usuarios.Include(r => r.oRol).Where(u => u.IdUsuario == idUsuario).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", respose = oUsuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, respose = oUsuario });
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Usuario usuario)
        {
            try
            {
                string convierteClave = string.Empty;
                if (!string.IsNullOrEmpty(usuario.Clave))
                {
                    convierteClave = _utilidadesService.ConvertirSha256(usuario.Clave);
                }
                else
                {
                    convierteClave = usuario.Clave;
                }
                usuario.Clave = convierteClave;
                _dbcontext.Usuarios.Add(usuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario creado." });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPut]
        [Route("Actualizar")]
        public IActionResult Actualizar([FromBody] Usuario objeto)
        {
            try
            {
                Usuario oUsuario = _dbcontext.Usuarios.Find(objeto.IdUsuario);

                if (oUsuario == null)
                {
                    return BadRequest("Usuario no encontrado.");
                }

                oUsuario.Correo = objeto.Correo is null ? oUsuario.Correo : objeto.Correo;
                oUsuario.Clave = objeto.Clave is null ? oUsuario.Clave : _utilidadesService.ConvertirSha256(objeto.Clave);
                oUsuario.Edad = objeto.Edad is null ? oUsuario.Edad : objeto.Edad;
                oUsuario.IdRol = objeto.IdRol is null ? oUsuario.IdRol : objeto.IdRol;
                oUsuario.EsActivo = objeto.EsActivo == oUsuario.EsActivo ? oUsuario.EsActivo : objeto.EsActivo;

                _dbcontext.Usuarios.Update(oUsuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario actualizado." });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idUsuario:int}")]
        public IActionResult Eliminar(int idUsuario)
        {
            Usuario oUsuario = _dbcontext.Usuarios.Find(idUsuario);

            if (oUsuario == null)
            {
                return BadRequest("Usuario no encontrado.");
            }

            try
            {
                _dbcontext.Usuarios.Remove(oUsuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Usuario eliminado." });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }




    }
}
