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

            return dbVeterinaria.RAZA_MASCOTA.FirstOrDefault(e => e.id_raza == id_raza);
        }

        public RAZA_MASCOTA ConsultarXNombre(string nombre_raza)
        {

            return dbVeterinaria.RAZA_MASCOTA.FirstOrDefault(e => e.nombre_raza == nombre_raza);
        }

        public List<RAZA_MASCOTA> ConsultarTodos()
        {
            return dbVeterinaria.RAZA_MASCOTA
                .OrderBy(n => n.nombre_raza).ToList();
        }

        public List<RAZA_MASCOTA> ConsultarXEspecie(int id_especie)
        {
            return dbVeterinaria.RAZA_MASCOTA
                .Where(p => p.id_especie == id_especie)
                .OrderBy(p => p.nombre_raza)
                .ToList();
        }

        public string EliminarXNombre(string nombre_raza)
        {
            try
            {

                RAZA_MASCOTA raza = ConsultarXNombre(nombre_raza);
                if (raza == null)
                {
                    return "La raza ingresada no existe, por lo tanto no se puede eliminar";
                }

                dbVeterinaria.RAZA_MASCOTA.Remove(raza);
                dbVeterinaria.SaveChanges();
                return "Se eliminó la raza correctamente";
            }
            catch (Exception ex)
            {
                return "No se pudo eliminar la raza: " + ex.Message;
            }
        }

        public IQueryable ListarRazasXEspecie()
        {
            //En SQL la instrucción es SELECT - FROM - WHERE
            //En linq la instrucción es FROM - WHERE - SELECT
            return from R in dbVeterinaria.Set<RAZA_MASCOTA>()
                   join E in dbVeterinaria.Set<ESPECIE_MASCOTA>()
                   on R.id_especie equals E.id_especie
                   orderby E.nombre_especie, R.nombre_raza
                   select new //Finalmente, se presentan los campos que se van a mostrar
                   {
                       Especie = E.nombre_especie,
                       Raza = R.nombre_raza
                   };
        }
    }
}