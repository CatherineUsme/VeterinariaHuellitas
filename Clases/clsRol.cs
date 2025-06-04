using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsRol
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public ROL rol { get; set; }

        public List<ROL> ConsultarTodos()
        {
            return db.ROLs.ToList();
        }

        public ROL ConsultarXId(int id)
        {
            return db.ROLs.FirstOrDefault(r => r.id_rol == id);
        }

        public string Insertar()
        {
            if (rol == null)
                return "Rol inválido.";
            if (string.IsNullOrWhiteSpace(rol.nombre_rol))
                return "El nombre es obligatorio.";
            if (db.ROLs.Any(r => r.nombre_rol == rol.nombre_rol))
                return "Ya existe un rol con ese nombre.";
            db.ROLs.Add(rol);
            db.SaveChanges();
            return "Rol insertado correctamente.";
        }

        public string Actualizar()
        {
            if (rol == null)
                return "Rol inválido.";
            var existente = db.ROLs.FirstOrDefault(r => r.id_rol == rol.id_rol);
            if (existente == null)
                return "Rol no encontrado.";
            if (string.IsNullOrWhiteSpace(rol.nombre_rol))
                return "El nombre es obligatorio.";
            if (db.ROLs.Any(r => r.nombre_rol == rol.nombre_rol && r.id_rol != rol.id_rol))
                return "Ya existe un rol con ese nombre.";
            db.Entry(existente).CurrentValues.SetValues(rol);
            db.SaveChanges();
            return "Rol actualizado correctamente.";
        }

        public string EliminarXId(int id)
        {
            var r = db.ROLs.FirstOrDefault(x => x.id_rol == id);
            if (r == null)
                return "Rol no encontrado.";
            db.ROLs.Remove(r);
            db.SaveChanges();
            return "Rol eliminado correctamente.";
        }
    }
}
