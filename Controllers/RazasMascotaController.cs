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
    [RoutePrefix("api/razasmascota")]
    public class RazasMascotaController : ApiController
    {
        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarTodos")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public List<RAZA_MASCOTA> ConsultarTodos()
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarTodos();
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXEspecie")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public List<RAZA_MASCOTA> ConsultarXEspecie(int id_especie)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXEspecie(id_especie);
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXId")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public RAZA_MASCOTA ConsultarXId(int id_raza)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXId(id_raza);
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXNombre")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public RAZA_MASCOTA ConsultarXNombre(string nombre_raza)
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ConsultarXNombre(nombre_raza);
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ListarRazasXEspecie")]
        //[AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public IQueryable ListarRazasXEspecie()
        {
            clsRazaMascota razaMascota = new clsRazaMascota();
            return razaMascota.ListarRazasXEspecie();
        }

        // Insertar: solo Administrador
        [HttpPost]
        [Route("Insertar")]
        //[AuthorizeRoles("Administrador")]
        public string Insertar([FromBody] RAZA_MASCOTA razaMascota)
        {
            clsRazaMascota raza = new clsRazaMascota();
            raza.razaMascota = razaMascota;
            return raza.Insertar();
        }

        // Actualizar: solo Administrador
        [HttpPut]
        [Route("Actualizar")]
        //[AuthorizeRoles("Administrador")]
        public string Actualizar([FromBody] RAZA_MASCOTA razaMascota)
        {
            clsRazaMascota raza = new clsRazaMascota();
            raza.razaMascota = razaMascota;
            return raza.Actualizar();
        }

        // Eliminar: solo Administrador
        [HttpDelete]
        [Route("EliminarXNombre")]
        //[AuthorizeRoles("Administrador")]
        public string EliminarXNombre(string nombre_raza)
        {
            clsRazaMascota raza = new clsRazaMascota();
            return raza.EliminarXNombre(nombre_raza);
        }

    }
}