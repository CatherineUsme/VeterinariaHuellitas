using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class UsuarioController : ApiController
    {
        public List<USUARIO> Get()
        {
            clsUsuario usuario = new clsUsuario();
            return usuario.ConsultarTodos();
        }

        public USUARIO Get(int id)
        {
            clsUsuario usuario = new clsUsuario();
            return usuario.ConsultarXId(id);
        }

        public string Post([FromBody] USUARIO usuario)
        {
            clsUsuario clsUsuario = new clsUsuario();
            clsUsuario.usuario = usuario;
            return clsUsuario.Insertar();
        }

        public string Put([FromBody] USUARIO usuario)
        {
            clsUsuario clsUsuario = new clsUsuario();
            clsUsuario.usuario = usuario;
            return clsUsuario.Actualizar();
        }

        public string Delete(int id)
        {
            clsUsuario clsUsuario = new clsUsuario();
            return clsUsuario.EliminarXId(id);
        }

        [HttpPut]
        [Route("api/Usuario/ModificarEstado/{id}")]
        public string ModificarEstado(int id, [FromBody] bool activo)
        {
            clsUsuario clsUsuario = new clsUsuario();
            return clsUsuario.ModificarEstado(id, activo);
        }
    }
}
