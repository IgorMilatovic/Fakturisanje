namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faktura")]
    public partial class Faktura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faktura()
        {
            Unosi = new HashSet<Unosi>();
        }

        [Key]
        [StringLength(10)]
        public string IdFakture { get; set; }

        [Column(TypeName = "date")]
        public DateTime Datum { get; set; }

        public double Ukupno { get; set; }

        public bool Obrisana { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unosi> Unosi { get; set; }
    }
}
