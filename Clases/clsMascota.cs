using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsMascota
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public MASCOTA mascota{ get; set; }

        public string Insertar(string cedula)
        {
            try
            {
                clsDueno dueno = new clsDueno();
                DUENO duenoExite = dueno.ConsultarXCedula(cedula);
                if (duenoExite == null)
                {
                    return "El dueño no esta resgistrado, se debe registrar primero";
                }
                dbVeterinaria.MASCOTAs.Add(mascota);
                dbVeterinaria.SaveChanges();
                return "Mascota insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar la mascota: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                MASCOTA masc = ConsultarXId(mascota.id_mascota);
                if (masc != null)
                {
                    dbVeterinaria.MASCOTAs.AddOrUpdate(mascota);
                    dbVeterinaria.SaveChanges();
                    return "Mascota actualizada correctamente.";
                }
                else
                {
                    return "Mascota no encontrada.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar la mascota: " + ex.Message;
            }
        }

        public MASCOTA ConsultarXId(int id_mascota)
        {

            return dbVeterinaria.MASCOTAs.FirstOrDefault(e => e.id_mascota == id_mascota);
        }

        public List<MASCOTA> ConsultarTodos()
        {
            return dbVeterinaria.MASCOTAs
                .OrderBy(n => n.nombre).ToList();
        }

        public List<MASCOTA> ConsultarXDueno(int id_dueno)
        {
            return dbVeterinaria.MASCOTAs
                .Where(p => p.id_dueno == id_dueno)
                .OrderBy(p => p.nombre)
                .ToList();
        }

        public string EliminarXId(int id_mascota)
        {
            try
            {

                MASCOTA masc = ConsultarXId(id_mascota);
                if (masc == null)
                {
                    return "La mascota ingresada no existe, por lo tanto no se puede eliminar";
                }

                dbVeterinaria.MASCOTAs.Remove(masc);
                dbVeterinaria.SaveChanges();
                return "Se eliminó la mascota correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la mascota: " + ex.Message;
            }
        }

        public string ModificarEstado(int id_mascota, bool Activo)
        {
            try
            {
                MASCOTA masc = ConsultarXId(id_mascota);
                if (masc == null)
                {
                    return "La mascota ingresada no existe en la Base de Datos";
                }
                masc.activa = Activo;
                dbVeterinaria.SaveChanges();
                if (Activo)
                {
                    return "Se activó correctamente la mascota";
                }
                else
                {
                    return "Se inactivó correctamente la mascota";
                }
            }
            catch (Exception ex)
            {
                return "Hubo un error al modificar el estado de la mascota: " + ex.Message;
            }
        }

    }
}