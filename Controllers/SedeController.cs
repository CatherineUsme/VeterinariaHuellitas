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
    [RoutePrefix("api/Sede")]
    //[AuthorizeRoles("Administrador")]
    public class SedeController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] SEDE sede)
        {
            clsSede sed = new clsSede();
            sed.sede = sede;
            return sed.Insertar();
        }
        
        [HttpGet]
        [Route("ConsultarTodos")]
        public List <SEDE> ConsultarTodos()
        {
            clsSede sede = new clsSede();
            return sede.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] SEDE sede)
        {
            clsSede sed = new clsSede();
            sed.sede = sede;
            return sed.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id_sede)
        {
            clsSede sed = new clsSede();
            return sed.Eliminar(id_sede);
        }

        [HttpGet]
        [Route("Consultar")]
        public SEDE Consultar(int id_sede)
        {
            clsSede sed = new clsSede();
            return sed.Consultar(id_sede);
        }
    } 

}