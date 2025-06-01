using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
	public class clsEspecieMascota
	{

        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public ESPECIE_MASCOTA especieMascota { get; set; } 

        public string Insertar()
        {
            try
            {
                ESPECIE_MASCOTA especieExite = ConsultarXNombre(especieMascota.nombre_especie);
                if (especieExite != null)
                {
                    return "La especie ya está registrada en la base de datos.";
                }
                dbVeterinaria.ESPECIE_MASCOTA.Add(especieMascota);
                dbVeterinaria.SaveChanges();
                return "Especie de mascota insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar especie de mascota: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                ESPECIE_MASCOTA espec = ConsultarXId(especieMascota.id_especie);
                if (espec != null)
                {
                    dbVeterinaria.ESPECIE_MASCOTA.AddOrUpdate(especieMascota);
                    dbVeterinaria.SaveChanges();
                    return "Especie de mascota actualizada correctamente.";
                }
                else
                {
                    return "Especie de mascota no encontrada.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar especie de mascota: " + ex.Message;
            }
        }

        public ESPECIE_MASCOTA ConsultarXId(int id_especie)
        {
            
             return dbVeterinaria.ESPECIE_MASCOTA.FirstOrDefault(e => e.id_especie == id_especie);
        }

        public ESPECIE_MASCOTA ConsultarXNombre(string nombre_especie)
        {

            return dbVeterinaria.ESPECIE_MASCOTA.FirstOrDefault(e => e.nombre_especie == nombre_especie);
        }

        public List<ESPECIE_MASCOTA> ConsultarTodos()
        {
            return dbVeterinaria.ESPECIE_MASCOTA
                .OrderBy(n=>n.nombre_especie).ToList();
        }

        public string EliminarXNombre(string nombre_especie)
        {
            try
            {

                ESPECIE_MASCOTA espec =ConsultarXNombre(nombre_especie);
                if (espec == null)
                {
                    return "La especie ingresada no existe, por lo tanto no se puede eliminar";
                }

                dbVeterinaria.ESPECIE_MASCOTA.Remove(espec); 
                dbVeterinaria.SaveChanges(); 
                return "Se eliminó la especie correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la especie: " + ex.Message;
            }
        }


    }

}