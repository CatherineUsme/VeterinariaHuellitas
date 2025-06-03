using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        public List<PRODUCTO> Get()
        {
            clsProducto servicio = new clsProducto();
            return servicio.ConsultarTodos();
        }

        [HttpGet]
        public PRODUCTO Get(int id)
        {
            clsProducto servicio = new clsProducto();
            return servicio.ConsultarXId(id);
        }

        [HttpPost]
        public string Post([FromBody] PRODUCTO producto)
        {
            clsProducto servicio = new clsProducto();
            servicio.producto = producto;
            return servicio.Insertar();
        }

        [HttpPut]
        public string Put([FromBody] PRODUCTO producto)
        {
            clsProducto servicio = new clsProducto();
            servicio.producto = producto;
            return servicio.Actualizar();
        }

        [HttpDelete]
        public string Delete(int id)
        {
            clsProducto servicio = new clsProducto();
            return servicio.EliminarXId(id);
        }
    }
}
