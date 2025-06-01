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
        [HttpGet]
        [Route("ConsultarTodos")]

        public List<ESPECIE_MASCOTA> ConsultarTodos()
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarTodos();
        }

        [HttpGet]
        [Route("ConsultarXId")]
        public ESPECIE_MASCOTA ConsultarXId(int id_especie)
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarXId(id_especie);
        }

        [HttpGet]
        [Route("ConsultarXNombre")]
        public ESPECIE_MASCOTA ConsultarXNombre(string nombre_especie)
        {
            clsEspecieMascota especieMascota = new clsEspecieMascota();
            return especieMascota.ConsultarXNombre(nombre_especie);
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody]ESPECIE_MASCOTA especieMascota)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            especie.especieMascota = especieMascota;
            return especie.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] ESPECIE_MASCOTA especieMascota)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            especie.especieMascota = especieMascota;
            return especie.Actualizar();
        }

        [HttpDelete]
        [Route("EliminarXNombre")]
        public string EliminarXNombre(string nombre_especie)
        {
            clsEspecieMascota especie = new clsEspecieMascota();
            return especie.EliminarXNombre(nombre_especie);
        }
    }
}