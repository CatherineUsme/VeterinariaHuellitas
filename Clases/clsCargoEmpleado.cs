using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity.Migrations;

namespace VeterinariaHuellitas.Clases
{
    public class clsCargoEmpleado
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();
        public CARGO_EMPLEADO cargo_empleado { get; set; }

        public string Insertar()
        {
            try
            {
                dbVeterinaria.CARGO_EMPLEADO.Add(cargo_empleado);
                dbVeterinaria.SaveChanges();
                return "Se grabo el cargo del empleado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<CARGO_EMPLEADO> ConsultarTodos()
        {
            return dbVeterinaria.CARGO_EMPLEADO.OrderBy(s => s.nombre_cargo).ToList();
        }
        public string Actualizar()
        {
            try
            {
                CARGO_EMPLEADO carg = Consultar(cargo_empleado.id_cargo_empleado);
                if (carg != null)
                {
                    dbVeterinaria.CARGO_EMPLEADO.AddOrUpdate(cargo_empleado);
                    dbVeterinaria.SaveChanges();
                    return "Cargo actualizado correctamente.";

                }
                else
                {
                    return "Cargo no encontrado.";
                }

            }
            catch (Exception ex)
            {
                return "Error al actualizar el cargo: " + ex.Message;
            }

        }
        public CARGO_EMPLEADO Consultar(int id_cargo_empleado)
        {
            return dbVeterinaria.CARGO_EMPLEADO.FirstOrDefault(s => s.id_cargo_empleado == id_cargo_empleado);
        }
        public string Eliminar(int id_cargo_empleado)
        {
            CARGO_EMPLEADO cargo_empleado  = Consultar(id_cargo_empleado);
            if (cargo_empleado == null)
            {
                return "El cargo no existe en la base de datos";
            }
            dbVeterinaria.CARGO_EMPLEADO.Remove(cargo_empleado);
            dbVeterinaria.SaveChanges();
            return "Se elimino el cargo";
        }

    }
}