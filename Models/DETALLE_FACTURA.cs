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
    
    public partial class DETALLE_FACTURA
    {
        public int id_detalle_factura { get; set; }
        public int id_factura { get; set; }
        public string tipo_item { get; set; }
        public Nullable<int> id_item_referencia { get; set; }
        public string descripcion_item { get; set; }
        public Nullable<int> cantidad { get; set; }
        public decimal precio_unitario { get; set; }
        public Nullable<decimal> descuento_item { get; set; }
        public Nullable<decimal> subtotal_item { get; set; }

        [JsonIgnore]
        public virtual FACTURA FACTURA { get; set; }
    }
}
