using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class TipoCirugiaController : ApiController
    {
        public List<TIPO_CIRUGIA> Get()
        {
            clsTipoCirugia tipoCirugia = new clsTipoCirugia();
            return tipoCirugia.ConsultarTodos();
        }

        public TIPO_CIRUGIA Get(int id)
        {
            clsTipoCirugia tipoCirugia = new clsTipoCirugia();
            return tipoCirugia.ConsultarXId(id);
        }

        public string Post([FromBody] TIPO_CIRUGIA tipoCirugia)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            clsTipoCirugia.tipoCirugia = tipoCirugia;
            return clsTipoCirugia.Insertar();
        }

        public string Put([FromBody] TIPO_CIRUGIA tipoCirugia)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            clsTipoCirugia.tipoCirugia = tipoCirugia;
            return clsTipoCirugia.Actualizar();
        }

        public string Delete(int id)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            return clsTipoCirugia.EliminarXId(id);
        }
    }
}
