using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    [AuthorizeRoles("Administrador")]
    public class TipoServicioAdicionalController : ApiController
    {
        public List<TIPO_SERVICIO_ADICIONAL> Get()
        {
            clsTipoServicioAdicional tipoServicioAdicional = new clsTipoServicioAdicional();
            return tipoServicioAdicional.ConsultarTodos();
        }

        public TIPO_SERVICIO_ADICIONAL Get(int id)
        {
            clsTipoServicioAdicional tipoServicioAdicional = new clsTipoServicioAdicional();
            return tipoServicioAdicional.ConsultarXId(id);
        }

        public string Post([FromBody] TIPO_SERVICIO_ADICIONAL tipoServicioAdicional)
        {
            clsTipoServicioAdicional clsTipoServicioAdicional = new clsTipoServicioAdicional();
            clsTipoServicioAdicional.tipoServicioAdicional = tipoServicioAdicional;
            return clsTipoServicioAdicional.Insertar();
        }

        public string Put([FromBody] TIPO_SERVICIO_ADICIONAL tipoServicioAdicional)
        {
            clsTipoServicioAdicional clsTipoServicioAdicional = new clsTipoServicioAdicional();
            clsTipoServicioAdicional.tipoServicioAdicional = tipoServicioAdicional;
            return clsTipoServicioAdicional.Actualizar();
        }

        public string Delete(int id)
        {
            clsTipoServicioAdicional clsTipoServicioAdicional = new clsTipoServicioAdicional();
            return clsTipoServicioAdicional.EliminarXId(id);
        }
    }
}
