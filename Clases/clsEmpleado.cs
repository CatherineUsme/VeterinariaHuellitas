using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeterinariaHuellitas.Models;
using System.Data.Entity.Migrations;

namespace VeterinariaHuellitas.Clases
{
    public class clsEmpleado
    {
        private VeterinariaEntities dbVeterinaria = new VeterinariaEntities();

        public EMPLEADO empleado { get; set; }

        public string Insertar ()
        {
            try
            {
                dbVeterinaria.EMPLEADOes.Add(empleado);
                dbVeterinaria.SaveChanges();
                return "Se grabo el cargo del empleado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<EMPLEADO> ConsultarTodos()
        {
            return dbVeterinaria.EMPLEADOes.OrderBy(s => s.id_empleado).ToList();
        }

        public string Actualizar()
        {
            try
            {
                EMPLEADO emple = Consultar(empleado.id_empleado);
                if (emple != null)
                {
                    dbVeterinaria.EMPLEADOes.AddOrUpdate(empleado);
                    dbVeterinaria.SaveChanges();
                    return "Cargo actualizado correctamente.";

                }
                else
                {
                    return "Empleado no encontrado.";
                }

            }
            catch (Exception ex)
            {
                return "Error al actualizar el empleado: " + ex.Message;
            }

        }

        public EMPLEADO Consultar(int id_empleado)
        {
            return dbVeterinaria.EMPLEADOes.FirstOrDefault(s => s.id_empleado == id_empleado);
        }

        public string Eliminar(int id_empleado)
        {
            EMPLEADO empleado = Consultar(id_empleado);
            if (empleado == null)
            {
                return "El empleado no existe en la base de datos";
            }
            dbVeterinaria.EMPLEADOes.Remove(empleado);
            dbVeterinaria.SaveChanges();
            return "Se elimino el Empleado";
        }

        public string ModificarEstado(int id_empleado, bool Activo)
        {
            try
            {
                EMPLEADO empl = Consultar (id_empleado);
                if (empl == null)
                {
                    return "El Empleado ingresado no existe en la Base de Datos";
                }
                empl.activo = Activo;
                dbVeterinaria.SaveChanges();
                if (Activo)
                {
                    return "Se activó correctamente el empleado";
                }
                else
                {
                    return "Se inactivó correctamente el empleado";
                }
            }
            catch (Exception ex)
            {
                return "Hubo un error al modificar el estado de el empleado: " + ex.Message;
            }
        }
    }
}