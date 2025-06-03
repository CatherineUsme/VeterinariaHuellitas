using System.Collections.Generic;
using System.Linq;
using VeterinariaHuellitas.Models;

namespace VeterinariaHuellitas.Clases
{
    public class clsProveedor
    {
        private VeterinariaEntities db = new VeterinariaEntities();
        public PROVEEDOR proveedor { get; set; }

        public List<PROVEEDOR> ConsultarTodos()
        {
            return db.PROVEEDORs.ToList();
        }

        public PROVEEDOR ConsultarXId(int id)
        {
            return db.PROVEEDORs.FirstOrDefault(p => p.id_proveedor == id);
        }

        public string Insertar()
        {
            if (proveedor == null)
                return "Proveedor inválido.";
            if (string.IsNullOrWhiteSpace(proveedor.nombre_empresa))
                return "El nombre de la empresa es obligatorio.";
            if (db.PROVEEDORs.Any(p => p.nit_ruc == proveedor.nit_ruc))
                return "Ya existe un proveedor con ese NIT/RUC.";
            db.PROVEEDORs.Add(proveedor);
            db.SaveChanges();
            return "Proveedor insertado correctamente.";
        }

        public string Actualizar()
        {
            if (proveedor == null)
                return "Proveedor inválido.";
            var existente = db.PROVEEDORs.FirstOrDefault(p => p.id_proveedor == proveedor.id_proveedor);
            if (existente == null)
                return "Proveedor no encontrado.";
            if (string.IsNullOrWhiteSpace(proveedor.nombre_empresa))
                return "El nombre de la empresa es obligatorio.";
            if (db.PROVEEDORs.Any(p => p.nit_ruc == proveedor.nit_ruc && p.id_proveedor != proveedor.id_proveedor))
                return "Ya existe un proveedor con ese NIT/RUC.";
            db.Entry(existente).CurrentValues.SetValues(proveedor);
            db.SaveChanges();
            return "Proveedor actualizado correctamente.";
        }

        public string EliminarXId(int id)
        {
            var prov = db.PROVEEDORs.FirstOrDefault(p => p.id_proveedor == id);
            if (prov == null)
                return "Proveedor no encontrado.";
            db.PROVEEDORs.Remove(prov);
            db.SaveChanges();
            return "Proveedor eliminado correctamente.";
        }
    }
}
