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
    
    public partial class HISTORIAL_MEDICO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HISTORIAL_MEDICO()
        {
            this.CIRUGIAs = new HashSet<CIRUGIA>();
            this.CITAs = new HashSet<CITA>();
            this.HOSPITALIZACIONs = new HashSet<HOSPITALIZACION>();
            this.SERVICIO_ADICIONAL_PRESTADO = new HashSet<SERVICIO_ADICIONAL_PRESTADO>();
            this.TRATAMIENTO_APLICADO = new HashSet<TRATAMIENTO_APLICADO>();
        }
    
        public int id_historial { get; set; }
        public int id_mascota { get; set; }
        public Nullable<int> id_empleado_veterinario { get; set; }
        public int id_sede { get; set; }
        public Nullable<System.DateTime> fecha_atencion { get; set; }
        public string motivo_consulta { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento_indicado { get; set; }
        public string observaciones { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CIRUGIA> CIRUGIAs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CITA> CITAs { get; set; }
        public virtual EMPLEADO EMPLEADO { get; set; }
        public virtual MASCOTA MASCOTA { get; set; }
        public virtual SEDE SEDE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOSPITALIZACION> HOSPITALIZACIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICIO_ADICIONAL_PRESTADO> SERVICIO_ADICIONAL_PRESTADO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRATAMIENTO_APLICADO> TRATAMIENTO_APLICADO { get; set; }
    }
}
