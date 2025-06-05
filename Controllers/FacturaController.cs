using System;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        // Ver: Recepcionista, Administrador, Farmaceuta
        [HttpGet]
        [Route("")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Farmaceuta")]
        public IHttpActionResult GetAll()
        {
            var service = new clsFactura();
            return Ok(service.ConsultarTodos());
        }

        // Ver: Recepcionista, Administrador, Farmaceuta
        [HttpGet]
        [Route("{id:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Farmaceuta")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsFactura();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Ver: Recepcionista, Administrador, Farmaceuta
        [HttpGet]
        [Route("cliente/{idCliente:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Farmaceuta")]
        public IHttpActionResult GetByCliente(int idCliente)
        {
            var service = new clsFactura();
            return Ok(service.ConsultarPorCliente(idCliente));
        }

        // Ver: Recepcionista, Administrador, Farmaceuta
        [HttpGet]
        [Route("fecha/{fechaInicio}/{fechaFin}")]
        [AuthorizeRoles("Recepcionista", "Administrador", "Farmaceuta")]
        public IHttpActionResult GetByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var service = new clsFactura();
            return Ok(service.ConsultarPorFecha(fechaInicio, fechaFin));
        }

        // Crear: Recepcionista, Administrador
        [HttpPost]
        [Route("")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult Create([FromBody] FACTURA factura)
        {
            var service = new clsFactura { factura = factura };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Editar: Recepcionista, Administrador
        [HttpPut]
        [Route("")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult Update([FromBody] FACTURA factura)
        {
            var service = new clsFactura { factura = factura };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Registrar Pago: Recepcionista, Administrador
        [HttpPut]
        [Route("pagar/{id:int}")]
        [AuthorizeRoles("Recepcionista", "Administrador")]
        public IHttpActionResult RegistrarPago(int id, [FromBody] int idMetodoPago)
        {
            var service = new clsFactura();
            var msg = service.RegistrarPago(id, idMetodoPago);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Anular: Administrador
        [HttpPut]
        [Route("anular/{id:int}")]
        [AuthorizeRoles("Administrador")]
        public IHttpActionResult Anular(int id)
        {
            var service = new clsFactura();
            var msg = service.AnularFactura(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
