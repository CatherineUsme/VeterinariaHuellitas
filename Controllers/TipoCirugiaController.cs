using System.Collections.Generic;
using System.Web.Http;
using VeterinariaHuellitas.Clases;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Controllers
{
    public class TipoCirugiaController : ApiController
    {
        // Ver: Administrador, Veterinario
        [AuthorizeRoles("Administrador", "Veterinario")]
        public List<TIPO_CIRUGIA> Get()
        {
            clsTipoCirugia tipoCirugia = new clsTipoCirugia();
            return tipoCirugia.ConsultarTodos();
        }

        // Ver: Administrador, Veterinario
        [AuthorizeRoles("Administrador", "Veterinario")]
        public TIPO_CIRUGIA Get(int id)
        {
            clsTipoCirugia tipoCirugia = new clsTipoCirugia();
            return tipoCirugia.ConsultarXId(id);
        }

        // Gestionar: solo Administrador
        [AuthorizeRoles("Administrador")]
        public string Post([FromBody] TIPO_CIRUGIA tipoCirugia)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            clsTipoCirugia.tipoCirugia = tipoCirugia;
            return clsTipoCirugia.Insertar();
        }

        // Gestionar: solo Administrador
        [AuthorizeRoles("Administrador")]
        public string Put([FromBody] TIPO_CIRUGIA tipoCirugia)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            clsTipoCirugia.tipoCirugia = tipoCirugia;
            return clsTipoCirugia.Actualizar();
        }

        // Gestionar: solo Administrador
        [AuthorizeRoles("Administrador")]
        public string Delete(int id)
        {
            clsTipoCirugia clsTipoCirugia = new clsTipoCirugia();
            return clsTipoCirugia.EliminarXId(id);
        }
    }
}
