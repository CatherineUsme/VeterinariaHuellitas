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
    [RoutePrefix("api/especiesmascota")]
    public class EspeciesMascotaController : ApiController
    {
        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarTodos")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public List<ESPECIE_MASCOTA> ConsultarTodos()
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarTodos();
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXId")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public ESPECIE_MASCOTA ConsultarXId(int id_especie)
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarXId(id_especie);
        }

        // Ver: Administrador, Veterinario, Recepcionista
        [HttpGet]
        [Route("ConsultarXNombre")]
        [AuthorizeRoles("Administrador", "Veterinario", "Recepcionista")]
        public ESPECIE_MASCOTA ConsultarXNombre(string nombre_especie)
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarXNombre(nombre_especie);
        }

        // Insertar: solo Administrador
        [HttpPost]
        [Route("Insertar")]
        [AuthorizeRoles("Administrador")]
        public string Insertar([FromBody]ESPECIE_MASCOTA especieMascota)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            especie.especieMascota = especieMascota;
            return especie.Insertar();
        }

        // Actualizar: solo Administrador
        [HttpPut]
        [Route("Actualizar")]
        [AuthorizeRoles("Administrador")]
        public string Actualizar([FromBody] ESPECIE_MASCOTA especieMascota)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            especie.especieMascota = especieMascota;
            return especie.Actualizar();
        }

        // Eliminar: solo Administrador
        [HttpDelete]
        [Route("EliminarXNombre")]
        [AuthorizeRoles("Administrador")]
        public string EliminarXNombre(string nombre_especie)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            return especie.EliminarXNombre(nombre_especie);
        }
    }
}