using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [AuthorizeRoles("Administrador")]
    public class UsuarioRolController : ApiController
    {
        public List<USUARIO_ROL> Get()
        {
            clsUsuarioRol usuarioRol = new clsUsuarioRol();
            return usuarioRol.ConsultarTodos();
        }

        public USUARIO_ROL Get(int id)
        {
            clsUsuarioRol usuarioRol = new clsUsuarioRol();
            return usuarioRol.ConsultarXId(id);
        }

        public string Post([FromBody] USUARIO_ROL usuarioRol)
        {
            clsUsuarioRol clsUsuarioRol = new clsUsuarioRol();
            clsUsuarioRol.usuarioRol = usuarioRol;
            return clsUsuarioRol.Insertar();
        }

        public string Put([FromBody] USUARIO_ROL usuarioRol)
        {
            clsUsuarioRol clsUsuarioRol = new clsUsuarioRol();
            clsUsuarioRol.usuarioRol = usuarioRol;
            return clsUsuarioRol.Actualizar();
        }

        public string Delete(int id)
        {
            clsUsuarioRol clsUsuarioRol = new clsUsuarioRol();
            return clsUsuarioRol.EliminarXId(id);
        }
    }
}
