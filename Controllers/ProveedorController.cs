using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class ProveedorController : ApiController
    {
        // Ver: Administrador, Farmaceuta
        [HttpGet]
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public List<PROVEEDOR> Get()
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.ConsultarTodos();
        }

        // Ver: Administrador, Farmaceuta
        [HttpGet]
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public PROVEEDOR Get(int id)
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.ConsultarXId(id);
        }

        // Crear: Administrador, Farmaceuta
        [HttpPost]
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public string Post([FromBody] PROVEEDOR proveedor)
        {
            clsProveedor servicio = new clsProveedor();
            servicio.proveedor = proveedor;
            return servicio.Insertar();
        }

        // Editar: Administrador, Farmaceuta
        [HttpPut]
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public string Put([FromBody] PROVEEDOR proveedor)
        {
            clsProveedor servicio = new clsProveedor();
            servicio.proveedor = proveedor;
            return servicio.Actualizar();
        }

        // Eliminar: solo Administrador
        [HttpDelete]
        //[AuthorizeRoles("Administrador")]
        public string Delete(int id)
        {
            clsProveedor servicio = new clsProveedor();
            return servicio.EliminarXId(id);
        }
    }
}
