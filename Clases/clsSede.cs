using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity.Migrations;


namespace VeterinariaHuellitas.Clases
{
    public class clsSede
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public SEDE sede { get; set; }
        public string Insertar ()
        {
            try
            {
                dbVeterinaria.SEDEs.Add(sede);
                dbVeterinaria.SaveChanges();
                return "Se grabo la sede";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<SEDE> ConsultarTodos()
        {
            return dbVeterinaria.SEDEs.OrderBy(s => s.nombre_sede).ToList();
        }
        public string Actualizar()
        {
            try
            {
                dbVeterinaria.SEDEs.AddOrUpdate(sede);
                dbVeterinaria.SaveChanges();
                return "Se actualizo la sede";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Eliminar(int id_sede)
        {
            SEDE _sede = Consultar(id_sede);
            if(_sede == null)
            {
                return "La sede no existe en la base de datos";
            }
            dbVeterinaria.SEDEs.Remove(_sede);
            dbVeterinaria.SaveChanges();
            return "Se elimino la sede";
        }
        public SEDE Consultar(int id_sede)
        {
            return dbVeterinaria.SEDEs.FirstOrDefault(s => s.id_sede == id_sede);
        }
    }
}