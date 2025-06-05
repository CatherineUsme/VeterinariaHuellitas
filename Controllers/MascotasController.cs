using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [RoutePrefix("api/mascotas")]
    public class MascotasController : ApiController
    {
        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarTodos")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public List<MASCOTA> ConsultarTodos()
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarTodos();
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXDueno")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public List<MASCOTA> ConsultarXdueno(int id_dueno)
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarXDueno(id_dueno);
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXId")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public MASCOTA ConsultarXId(int id_mascota)
        {
            clsMascota mascota = new clsMascota();
            return mascota.ConsultarXId(id_mascota);
        }

        // Crear: Administrador, Veterinario, Recepcionista
        [HttpPost]
        [Route("Insertar")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public string Insertar([FromBody] MASCOTA mascota)
        {
            clsMascota masc = new clsMascota();
            masc.mascota = mascota;
            return masc.Insertar();
        }

        // Editar: Administrador, Veterinario, Recepcionista
        [HttpPut]
        [Route("Actualizar")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public string Actualizar([FromBody] MASCOTA mascota)
        {
            clsMascota masc = new clsMascota();
            masc.mascota = mascota;
            return masc.Actualizar();
        }

        // Inactivar: Administrador, Veterinario
        [HttpPut]
        [Route("Inactivar")]
        //[AuthorizeRoles("Administrador", "Veterinario")]
        public string Inactivar(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.ModificarEstado(id_mascota, false);
        }

        // Activar: Administrador, Veterinario
        [HttpPut]
        [Route("Activar")]
        //[AuthorizeRoles("Administrador", "Veterinario")]
        public string Activar(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.ModificarEstado(id_mascota, true);
        }

        // Eliminar: Administrador
        [HttpDelete]
        [Route("EliminarXId")]
        //[AuthorizeRoles("Administrador")]
        public string EliminarXId(int id_mascota)
        {
            clsMascota masc = new clsMascota();
            return masc.EliminarXId(id_mascota);
        }
    }
}