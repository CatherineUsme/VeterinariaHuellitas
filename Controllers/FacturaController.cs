using System;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var service = new clsFactura();
            return Ok(service.ConsultarTodos());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsFactura();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("cliente/{idCliente:int}")]
        public IHttpActionResult GetByCliente(int idCliente)
        {
            var service = new clsFactura();
            return Ok(service.ConsultarPorCliente(idCliente));
        }

        [HttpGet]
        [Route("fecha/{fechaInicio}/{fechaFin}")]
        public IHttpActionResult GetByFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var service = new clsFactura();
            return Ok(service.ConsultarPorFecha(fechaInicio, fechaFin));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] FACTURA factura)
        {
            var service = new clsFactura { factura = factura };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] FACTURA factura)
        {
            var service = new clsFactura { factura = factura };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("anular/{id:int}")]
        public IHttpActionResult Anular(int id)
        {
            var service = new clsFactura();
            var msg = service.AnularFactura(id);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
