namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stavka")]
    public partial class Stavka
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stavka()
        {
            Unosi = new HashSet<Unosi>();
        }

        [Key]
        public int IdStavke { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Naziv")]
        public string NazivStavke { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Cena")]
        public string CenaStavke { get; set; }

        public bool Obrisana { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unosi> Unosi { get; set; }
    }
}
