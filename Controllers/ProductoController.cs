using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class ProductoController : ApiController
    {
        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        //[AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public List<PRODUCTO> Get()
        {
            clsProducto servicio = new clsProducto();
            return servicio.ConsultarTodos();
        }

        // Ver: Farmaceuta, Administrador, Veterinario, Recepcionista
        [HttpGet]
        //[AuthorizeRoles("Farmaceuta", "Administrador", "Veterinario", "Recepcionista")]
        public PRODUCTO Get(int id)
        {
            clsProducto servicio = new clsProducto();
            return servicio.ConsultarXId(id);
        }

        // Gestionar: Farmaceuta, Administrador
        [HttpPost]
        //[AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Post([FromBody] PRODUCTO producto)
        {
            clsProducto servicio = new clsProducto();
            servicio.producto = producto;
            return servicio.Insertar();
        }

        // Gestionar: Farmaceuta, Administrador
        [HttpPut]
        //[AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Put([FromBody] PRODUCTO producto)
        {
            clsProducto servicio = new clsProducto();
            servicio.producto = producto;
            return servicio.Actualizar();
        }

        // Eliminar: Farmaceuta, Administrador
        [HttpDelete]
        //[AuthorizeRoles("Farmaceuta", "Administrador")]
        public string Delete(int id)
        {
            clsProducto servicio = new clsProducto();
            return servicio.EliminarXId(id);
        }
    }
}
