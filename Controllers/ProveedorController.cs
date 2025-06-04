using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class ProveedorController : ApiController
    {
        [HttpGet]
        public List<PROVEEDOR> Get()
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.ConsultarTodos();
        }

        [HttpGet]
        public PROVEEDOR Get(int id)
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.ConsultarXId(id);
        }

        [HttpPost]
        public string Post([FromBody] PROVEEDOR proveedor)
        {
            clsProveedor servicio = new clsProveedor();
            servicio.proveedor = proveedor;
            return servicio.Insertar();
        }

        [HttpPut]
        public string Put([FromBody] PROVEEDOR proveedor)
        {
            clsProveedor servicio = new clsProveedor();
            servicio.proveedor = proveedor;
            return servicio.Actualizar();
        }

        [HttpDelete]
        public string Delete(int id)
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.EliminarXId(id);
        }
    }
}
