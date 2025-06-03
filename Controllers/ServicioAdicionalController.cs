using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/servicio-adicional")]
    public class ServicioAdicionalController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var service = new clsServicioAdicional();
            return Ok(service.ConsultarTodos());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsServicioAdicional();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] SERVICIO_ADICIONAL_PRESTADO servicio)
        {
            var service = new clsServicioAdicional { servicio = servicio };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] SERVICIO_ADICIONAL_PRESTADO servicio)
        {
            var service = new clsServicioAdicional { servicio = servicio };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsServicioAdicional();
            var msg = service.EliminarXId(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
