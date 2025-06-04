using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsServicioAdicional
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public SERVICIO_ADICIONAL_PRESTADO servicio { get; set; }

        public List<SERVICIO_ADICIONAL_PRESTADO> ConsultarTodos()
        {
            return db.SERVICIO_ADICIONAL_PRESTADO.ToList();
        }

        public SERVICIO_ADICIONAL_PRESTADO ConsultarXId(int id)
        {
            return db.SERVICIO_ADICIONAL_PRESTADO.FirstOrDefault(s => s.id_servicio_prestado == id);
        }

        public string Insertar()
        {
            if (servicio == null)
                return "Servicio inválido.";
            if (servicio.id_mascota <= 0 || servicio.id_tipo_servicio_adicional <= 0 || servicio.id_empleado_realiza <= 0 || servicio.id_sede <= 0)
                return "Faltan datos obligatorios.";
            db.SERVICIO_ADICIONAL_PRESTADO.Add(servicio);
            db.SaveChanges();
            return "Servicio adicional registrado correctamente.";
        }

        public string Actualizar()
        {
            if (servicio == null)
                return "Servicio inválido.";
            var existente = db.SERVICIO_ADICIONAL_PRESTADO.FirstOrDefault(s => s.id_servicio_prestado == servicio.id_servicio_prestado);
            if (existente == null)
                return "Servicio no encontrado.";
            db.Entry(existente).CurrentValues.SetValues(servicio);
            db.SaveChanges();
            return "Servicio actualizado correctamente.";
        }

        public string EliminarXId(int id)
        {
            var s = db.SERVICIO_ADICIONAL_PRESTADO.FirstOrDefault(x => x.id_servicio_prestado == id);
            if (s == null)
                return "Servicio no encontrado.";
            db.SERVICIO_ADICIONAL_PRESTADO.Remove(s);
            db.SaveChanges();
            return "Servicio eliminado correctamente.";
        }
    }
}
