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
    [RoutePrefix("api/Empleado")]
    public class EmpleadoController : ApiController
    {

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] EMPLEADO empleado)
        {
            clsEmpleado empl= new clsEmpleado();
            empl.empleado = empleado;
            return empl.Insertar();
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<EMPLEADO> ConsultarTodos()
        {
            clsEmpleado empl = new clsEmpleado();
            return empl.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] EMPLEADO empleado)
        {
            clsEmpleado empl = new clsEmpleado();
            empl.empleado = empleado;
            return empl.Actualizar();
        }

        [HttpGet]
        [Route("Consultar")]
        public EMPLEADO Consultar(int id_empleado)
        {
            clsEmpleado empl = new clsEmpleado();
            return empl.Consultar(id_empleado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id_empleado)
        {
            clsEmpleado empl = new clsEmpleado();
            return empl.Eliminar(id_empleado);
        }

        [HttpPut]
        [Route("Inactivar")]
        public string Inactivar(int id_empleado)
        {
            clsEmpleado empl = new clsEmpleado();
            return empl.ModificarEstado(id_empleado, false);
        }

        [HttpPut]
        [Route("Activar")]
        public string Activar(int id_empleado)
        {
            clsEmpleado empl = new clsEmpleado();
            return empl.ModificarEstado(id_empleado, true);
        }
    }
}