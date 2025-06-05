using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/detalle-factura")]
    public class DetalleFacturaController : ApiController
    {
        // Ver: Recepcionista, Administrador, Veterinario, Farmaceuta
        [HttpGet]
        [Route("factura/{idFactura:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Veterinario", "Farmaceuta")]
        public IHttpActionResult GetByFactura(int idFactura)
        {
            var service = new clsDetalleFactura();
            return Ok(service.ConsultarPorFactura(idFactura));
        }

        // Ver: Recepcionista, Administrador, Veterinario, Farmaceuta
        [HttpGet]
        [Route("{id:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Veterinario", "Farmaceuta")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsDetalleFactura();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Crear: Recepcionista, Administrador
        [HttpPost]
        [Route("")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult Create([FromBody] DETALLE_FACTURA detalleFactura)
        {
            var service = new clsDetalleFactura { detalleFactura = detalleFactura };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Editar: Recepcionista, Administrador
        [HttpPut]
        [Route("")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult Update([FromBody] DETALLE_FACTURA detalleFactura)
        {
            var service = new clsDetalleFactura { detalleFactura = detalleFactura };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Eliminar (anular): Recepcionista (con permisos específicos), Administrador
        [HttpDelete]
        [Route("{id:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsDetalleFactura();
            var msg = service.EliminarXId(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
