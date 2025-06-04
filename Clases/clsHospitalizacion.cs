using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity.Migrations;

namespace VeterinariaHuellitas.Clases
{
    public class clsHospitalizacion
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public HOSPITALIZACION hospitalizacion { get; set; }

        public string Insertar()
        {
            try
            {
                dbVeterinaria.HOSPITALIZACIONs.Add(hospitalizacion);
                dbVeterinaria.SaveChanges();
                return "Se grabo la hospitalizacion";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<HOSPITALIZACION> ConsultarTodos()
        {
            return dbVeterinaria.HOSPITALIZACIONs.OrderBy(s => s.id_hospitalizacion).ToList();
        }

        public HOSPITALIZACION Consultar(int id_hospitalizacion)
        {
            return dbVeterinaria.HOSPITALIZACIONs.FirstOrDefault(s => s.id_hospitalizacion == id_hospitalizacion);
        }

        public string Actualizar()
        {
            try
            {
                HOSPITALIZACION hospi = Consultar(hospitalizacion.id_hospitalizacion);
                if (hospi != null)
                {
                    dbVeterinaria.HOSPITALIZACIONs.AddOrUpdate(hospi);
                    dbVeterinaria.SaveChanges();
                    return "Hospitalizacion actualizada correctamente.";

                }
                else
                {
                    return "Hospitalizacion no encontrada.";
                }

            }
            catch (Exception ex)
            {
                return "Error al actualizar la hospitalizacion: " + ex.Message;
            }

        }

        public string Eliminar(int id_hospitalizacion)
        {
            HOSPITALIZACION hospitalizacion = Consultar(id_hospitalizacion);
            if (hospitalizacion == null)
            {
                return "La hospitalizacion no existe en la base de datos";
            }
            dbVeterinaria.HOSPITALIZACIONs.Remove(hospitalizacion);
            dbVeterinaria.SaveChanges();
            return "Se elimino la hospitalizacion";
        }
    }
}