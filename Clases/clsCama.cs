using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity.Migrations;

namespace VeterinariaHuellitas.Clases
{
    public class clsCama
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();

        public CAMA cama { get; set; }

        public string Insertar()
        {
            try
            {
                dbVeterinaria.CAMAs.Add(cama);
                dbVeterinaria.SaveChanges();
                return "Se grabo la cama";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<CAMA> ConsultarTodos()
        {
            return dbVeterinaria.CAMAs.OrderBy(s => s.id_cama).ToList();
        }

        public string Actualizar()
        {
            try
            {
                CAMA cam = Consultar(cama.id_cama);
                if (cam != null)
                {
                    dbVeterinaria.CAMAs.AddOrUpdate(cama);
                    dbVeterinaria.SaveChanges();
                    return "Cama actualizada correctamente.";

                }
                else
                {
                    return "Cama no encontrada.";
                }

            }
            catch (Exception ex)
            {
                return "Error al actualizar la cama: " + ex.Message;
            }

        }

        public CAMA Consultar(int id_cama)
        {
            return dbVeterinaria.CAMAs.FirstOrDefault(s => s.id_cama == id_cama);
        }

        public string Eliminar(int id_cama)
        {
            CAMA cama = Consultar(id_cama);
            if (cama == null)
            {
                return "La cama no existe en la base de datos";
            }
            dbVeterinaria.CAMAs.Remove(cama);
            dbVeterinaria.SaveChanges();
            return "Se elimino la cama";
        }
    }
}