using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/detalle-factura")]
    public class DetalleFacturaController : ApiController
    {
        [HttpGet]
        [Route("factura/{idFactura:int}")]
        public IHttpActionResult GetByFactura(int idFactura)
        {
            var service = new clsDetalleFactura();
            return Ok(service.ConsultarPorFactura(idFactura));
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsDetalleFactura();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] DETALLE_FACTURA detalleFactura)
        {
            var service = new clsDetalleFactura { detalleFactura = detalleFactura };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] DETALLE_FACTURA detalleFactura)
        {
            var service = new clsDetalleFactura { detalleFactura = detalleFactura };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsDetalleFactura();
            var msg = service.EliminarXId(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
