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
    
    public partial class TIPO_PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_PRODUCTO()
        {
            this.PRODUCTOes = new HashSet<PRODUCTO>();
        }
    
        public int id_tipo_producto { get; set; }
        public string nombre_tipo { get; set; }
        public string descripcion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<PRODUCTO> PRODUCTOes { get; set; }
    }
}
