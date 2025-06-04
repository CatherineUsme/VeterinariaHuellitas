using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/metodo-pago")]
    public class MetodoPagoController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var service = new clsMetodoPago();
            return Ok(service.ConsultarTodos());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsMetodoPago();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] METODO_PAGO metodoPago)
        {
            var service = new clsMetodoPago { metodoPago = metodoPago };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] METODO_PAGO metodoPago)
        {
            var service = new clsMetodoPago { metodoPago = metodoPago };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsMetodoPago();
            var msg = service.EliminarXId(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
