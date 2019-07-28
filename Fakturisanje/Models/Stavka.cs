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
        [Display(Name="Naziv: ")]
        public string NazivStavke { get; set; }

        [Required]
        [Display(Name = "Cena: ")]
        public double CenaStavke { get; set; }

        public bool Obrisana { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unosi> Unosi { get; set; }

        [Display(Name = "Količina: ")]
        public int? kolicina { get; set; }

        public static implicit operator Stavka(HashSet<Stavka> v)
        {
            throw new NotImplementedException();
        }
    }
}
