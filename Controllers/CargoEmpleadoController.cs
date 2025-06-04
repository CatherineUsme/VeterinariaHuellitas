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
    [RoutePrefix("api/CargoEmpleado")]
    public class CargoEmpleadoController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] CARGO_EMPLEADO cargo_empleado)
        {
            clsCargoEmpleado cargo = new clsCargoEmpleado();
            cargo.cargo_empleado = cargo_empleado;
            return cargo.Insertar();
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<CARGO_EMPLEADO> ConsultarTodos()
        {
            clsCargoEmpleado cargo = new clsCargoEmpleado();
            return cargo.ConsultarTodos();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] CARGO_EMPLEADO cargo_empleado)
        {
            clsCargoEmpleado cargo = new clsCargoEmpleado();
            cargo.cargo_empleado = cargo_empleado;
            return cargo.Actualizar();
        }

        [HttpGet]
        [Route("Consultar")]
        public CARGO_EMPLEADO Consultar(int id_cargo_empleado)
        {
            clsCargoEmpleado cargo = new clsCargoEmpleado();
            return cargo.Consultar(id_cargo_empleado);
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int id_cargo_empleado)
        {
            clsCargoEmpleado cargo = new clsCargoEmpleado();
            return cargo.Eliminar(id_cargo_empleado);
        }
    }


}