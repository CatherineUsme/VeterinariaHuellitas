using System;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var service = new clsCita();
            return Ok(service.ConsultarTodos());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsCita();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] CITA cita)
        {
            var service = new clsCita { cita = cita };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] CITA cita)
        {
            var service = new clsCita { cita = cita };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpPut]
        [Route("{id:int}/estado")]
        public IHttpActionResult CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var service = new clsCita();
            var cita = service.Cons ultarXId(id);
            if (cita == null) return NotFound();
            var citaUpdate = new CITA
            {
                id_cita = cita.id_cita,
                id_mascota = cita.id_mascota,
                id_empleado_asignado = cita.id_empleado_asignado,
                id_sede = cita.id_sede,
                id_tipo_cita = cita.id_tipo_cita,
                fecha_hora_cita = cita.fecha_hora_cita,
                estado = nuevoEstado,
                observaciones = cita.observaciones,
                id_historial_medico_asociado = cita.id_historial_medico_asociado
            };
            service.cita = citaUpdate;
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            var service = new clsCita();
            var msg = service.EliminarXId(id);
            if (msg == "Cita no encontrada.") return NotFound();
            if (msg == "No se puede eliminar la cita porque tiene historial médico asociado." || msg == "No se puede eliminar la cita porque tiene facturación asociada.")
                return BadRequest(msg);
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }
    }
}
