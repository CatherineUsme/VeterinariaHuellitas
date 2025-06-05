using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/servicio-adicional")]
    public class ServicioAdicionalController : ApiController
    {
        // Ver: Administrador, Recepcionista, Veterinario
        [HttpGet]
        [Route("")]
        //[AuthorizeRoles("Administrador", "Recepcionista", "Veterinario")]
        public IHttpActionResult GetAll()
        {
            var service = new clsServicioAdicional();
            return Ok(service.ConsultarTodos());
        }

        // Ver: Administrador, Recepcionista, Veterinario
        [HttpGet]
        [Route("{id:int}")]
        //[AuthorizeRoles("Administrador", "Recepcionista", "Veterinario")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsServicioAdicional();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Crear: Administrador, Recepcionista
        [HttpPost]
        [Route("")]
        //[AuthorizeRoles("Administrador", "Recepcionista")]
        public IHttpActionResult Create([FromBody] SERVICIO_ADICIONAL_PRESTADO servicio)
        {
            var service = new clsServicioAdicional { servicio = servicio };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Editar: Administrador, Recepcionista
        [HttpPut]
        [Route("")]
        //[AuthorizeRoles("Administrador", "Recepcionista")]
        public IHttpActionResult Update([FromBody] SERVICIO_ADICIONAL_PRESTADO servicio)
        {
            var service = new clsServicioAdicional { servicio = servicio };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Eliminar: Administrador, Recepcionista
        [HttpDelete]
        [Route("{id:int}")]
        //[AuthorizeRoles("Administrador", "Recepcionista")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsServicioAdicional();
            var msg = service.EliminarXId(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
