using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class TipoProductoController : ApiController
    {
        // Ver: Administrador, Farmaceuta
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public List<TIPO_PRODUCTO> Get()
        {
            clsTipoProducto tipoProducto = new clsTipoProducto();
            return tipoProducto.ConsultarTodos();
        }

        // Ver: Administrador, Farmaceuta
        //[AuthorizeRoles("Administrador", "Farmaceuta")]
        public TIPO_PRODUCTO Get(int id)
        {
            clsTipoProducto tipoProducto = new clsTipoProducto();
            return tipoProducto.ConsultarXId(id);
        }

        // Gestionar: solo Administrador
        //[AuthorizeRoles("Administrador")]
        public string Post([FromBody] TIPO_PRODUCTO tipoProducto)
        {
            clsTipoProducto clsTipoProducto = new clsTipoProducto();
            clsTipoProducto.tipoProducto = tipoProducto;
            return clsTipoProducto.Insertar();
        }

        // Gestionar: solo Administrador
        //[AuthorizeRoles("Administrador")]
        public string Put([FromBody] TIPO_PRODUCTO tipoProducto)
        {
            clsTipoProducto clsTipoProducto = new clsTipoProducto();
            clsTipoProducto.tipoProducto = tipoProducto;
            return clsTipoProducto.Actualizar();
        }

        // Gestionar: solo Administrador
        //[AuthorizeRoles("Administrador")]
        public string Delete(int id)
        {
            clsTipoProducto clsTipoProducto = new clsTipoProducto();
            return clsTipoProducto.EliminarXId(id);
        }
    }
}
