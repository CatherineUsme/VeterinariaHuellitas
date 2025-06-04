using System;
using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsCita
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public CITA cita { get; set; }

        private static readonly string[] EstadosValidos = { "Programada", "Confirmada", "Completada", "Cancelada", "No Asistió" };

        public string Insertar()
        {
            if (!ValidarCita(cita, true, out string error))
                return error;
            db.CITAs.Add(cita);
            db.SaveChanges();
            return "Cita registrada correctamente.";
        }

        public string Actualizar()
        {
            var citaDb = db.CITAs.FirstOrDefault(c => c.id_cita == cita.id_cita);
            if (citaDb == null)
                return "Cita no encontrada.";
            if (!ValidarCita(cita, false, out string error))
                return error;
            if (cita.estado != citaDb.estado && !EsTransicionValida(citaDb.estado, cita.estado))
                return "Transición de estado no permitida.";
            citaDb.id_mascota = cita.id_mascota;
            citaDb.id_empleado_asignado = cita.id_empleado_asignado;
            citaDb.id_sede = cita.id_sede;
            citaDb.id_tipo_cita = cita.id_tipo_cita;
            citaDb.fecha_hora_cita = cita.fecha_hora_cita;
            citaDb.observaciones = cita.observaciones;
            citaDb.id_historial_medico_asociado = cita.id_historial_medico_asociado;
            if (cita.estado != citaDb.estado)
                citaDb.estado = cita.estado;
            db.SaveChanges();
            return "Cita actualizada correctamente.";
        }

        public string EliminarXId(int id)
        {
            var citaDb = db.CITAs.FirstOrDefault(c => c.id_cita == id);
            if (citaDb == null)
                return "Cita no encontrada.";
            if (citaDb.id_historial_medico_asociado != null)
                return "No se puede eliminar la cita porque tiene historial médico asociado.";
            if (db.DETALLE_FACTURA.Any(df => df.tipo_item == "CITA" && df.id_item_referencia == id))
                return "No se puede eliminar la cita porque tiene facturación asociada.";
            db.CITAs.Remove(citaDb);
            db.SaveChanges();
            return "Cita eliminada correctamente.";
        }

        public CITA ConsultarXId(int id)
        {
            return db.CITAs.FirstOrDefault(c => c.id_cita == id);
        }

        public List<CITA> ConsultarTodos()
        {
            return db.CITAs.ToList();
        }

        private bool ValidarCita(CITA c, bool esNuevo, out string error)
        {
            error = null;
            if (c == null)
            {
                error = "Datos de cita requeridos.";
                return false;
            }
            if (!EstadosValidos.Contains(c.estado))
            {
                error = "Estado de cita inválido.";
                return false;
            }
            if (db.MASCOTAs.FirstOrDefault(m => m.id_mascota == c.id_mascota) == null)
            {
                error = "Mascota no existe.";
                return false;
            }
            if (db.EMPLEADOes.FirstOrDefault(e => e.id_empleado == c.id_empleado_asignado) == null)
            {
                error = "Empleado asignado no existe.";
                return false;
            }
            if (db.SEDEs.FirstOrDefault(s => s.id_sede == c.id_sede) == null)
            {
                error = "Sede no existe.";
                return false;
            }
            if (db.TIPO_CITA.FirstOrDefault(t => t.id_tipo_cita == c.id_tipo_cita) == null)
            {
                error = "Tipo de cita no existe.";
                return false;
            }
            if (c.fecha_hora_cita < DateTime.Now && esNuevo)
            {
                error = "La fecha de la cita debe ser futura.";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(c.observaciones) && c.observaciones.Length > 500)
            {
                error = "Observaciones demasiado largas.";
                return false;
            }
            return true;
        }

        private bool EsTransicionValida(string estadoActual, string nuevoEstado)
        {
            if (estadoActual == "Programada")
                return nuevoEstado == "Confirmada" || nuevoEstado == "Cancelada";
            if (estadoActual == "Confirmada")
                return nuevoEstado == "Completada" || nuevoEstado == "Cancelada" || nuevoEstado == "No Asistió";
            return false;
        }
    }
}
