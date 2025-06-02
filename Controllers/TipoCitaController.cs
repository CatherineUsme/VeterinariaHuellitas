using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class TipoCitaController : ApiController
    {
        public List<TIPO_CITA> Get()
        {
            clsTipoCita tipoCita = new clsTipoCita();
            return tipoCita.ConsultarTodos();
        }

        public TIPO_CITA Get(int id)
        {
            clsTipoCita tipoCita = new clsTipoCita();
            return tipoCita.ConsultarXId(id);
        }

        public string Post([FromBody] TIPO_CITA tipoCita)
        {
            clsTipoCita clsTipoCita = new clsTipoCita();
            clsTipoCita.tipoCita = tipoCita;
            return clsTipoCita.Insertar();
        }

        public string Put([FromBody] TIPO_CITA tipoCita)
        {
            clsTipoCita clsTipoCita = new clsTipoCita();
            clsTipoCita.tipoCita = tipoCita;
            return clsTipoCita.Actualizar();
        }

        public string Delete(int id)
        {
            clsTipoCita clsTipoCita = new clsTipoCita();
            return clsTipoCita.EliminarXId(id);
        }
    }
}
