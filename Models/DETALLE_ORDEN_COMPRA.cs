//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeterinariaHuellitas.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    
    public partial class DETALLE_ORDEN_COMPRA
    {
        public int id_detalle_orden_compra { get; set; }
        public int id_orden_compra { get; set; }
        public int id_producto { get; set; }
        public int cantidad_solicitada { get; set; }
        public decimal precio_unitario_compra { get; set; }
        public Nullable<decimal> subtotal { get; set; }
    
        [JsonIgnore]
        public virtual ORDEN_COMPRA ORDEN_COMPRA { get; set; }

        [JsonIgnore]
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
