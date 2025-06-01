using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
	public class clsRazaMascota
	{

        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public RAZA_MASCOTA razaMascota { get; set; }


        public string Insertar()
        {
            try
            {
                RAZA_MASCOTA razaExite = ConsultarXNombre(razaMascota.nombre_raza);
                if (razaExite != null)
                {
                    return "La raza ya está registrada en la base de datos.";
                }
                dbVeterinaria.RAZA_MASCOTA.Add(razaMascota);
                dbVeterinaria.SaveChanges();
                return "Raza de mascota insertada correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al insertar raza de mascota: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                RAZA_MASCOTA raza = ConsultarXId(razaMascota.id_especie);
                if (raza != null)
                {
                    dbVeterinaria.RAZA_MASCOTA.AddOrUpdate(razaMascota);
                    dbVeterinaria.SaveChanges();
                    return "Raza de mascota actualizada correctamente.";
                }
                else
                {
                    return "Raza de mascota no encontrada.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar raza de mascota: " + ex.Message;
            }
        }

        public RAZA_MASCOTA ConsultarXId(int id_raza)
        {

            return dbVeterinaria.ESPECIE_MASCOTA.FirstOrDefault(e => e.id_especie == id_especie);
        }

        public RAZA_MASCOTA ConsultarXNombre(string nombre_raza)
        {

            return dbVeterinaria.RAZA_MASCOTA.FirstOrDefault(e => e.nombre_raza == nombre_raza);
        }
    }
}