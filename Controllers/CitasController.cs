using System;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        // Ver: Recepcionista, Veterinario, Administrador
        [HttpGet]
        [Route("")]
        //[AuthorizeRoles("Recepcionista", "Veterinario", "Administrador")]
        public IHttpActionResult GetAll()
        {
            var service = new clsCita();
            return Ok(service.ConsultarTodos());
        }

        // Ver: Recepcionista, Veterinario, Administrador
        [HttpGet]
        [Route("{id:int}")]
        //[AuthorizeRoles("Recepcionista", "Veterinario", "Administrador")]
        public IHttpActionResult GetById(int id)
        {
            var service = new clsCita();
            var result = service.ConsultarXId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // Crear: Recepcionista, Veterinario, Administrador
        [HttpPost]
        [Route("")]
        //[AuthorizeRoles("Recepcionista", "Veterinario", "Administrador")]
        public IHttpActionResult Create([FromBody] CITA cita)
        {
            var service = new clsCita { cita = cita };
            var msg = service.Insertar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Editar: Recepcionista, Veterinario, Administrador
        [HttpPut]
        [Route("")]
        //[AuthorizeRoles("Recepcionista", "Veterinario", "Administrador")]
        public IHttpActionResult Update([FromBody] CITA cita)
        {
            var service = new clsCita { cita = cita };
            var msg = service.Actualizar();
            if (!msg.Contains("correctamente")) return BadRequest(msg);
            return Ok(msg);
        }

        // Editar estado: Recepcionista, Veterinario, Administrador
        [HttpPut]
        [Route("{id:int}/estado")]
        //[AuthorizeRoles("Recepcionista", "Veterinario", "Administrador")]
        public IHttpActionResult CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var service = new clsCita();
            var cita = service.ConsultarXId(id);
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

        // Eliminar: Recepcionista, Administrador
        [HttpDelete]
        [Route("{id:int}")]
        //[AuthorizeRoles("Recepcionista", "Administrador")]
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
