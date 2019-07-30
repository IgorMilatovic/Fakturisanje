namespace Fakturisanje.Models
{
    using Fakturisanje.Attributes;
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
        [Display(Name = "Broj: ")]
        [RegularExpression("^([a-zA-Z0-9-]+)$", ErrorMessage = "Dozvoljeni su brojevi, slova i simbol -")]
        [Required(ErrorMessage = "Obavezan broj fakture od 10 karaktera nije unet"), MaxLength(10)]
        public string IdFakture { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Datum: ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //[ProveraDatumaZaBuduceVreme(ErrorMessage = "Datum ne može biti u budućnosti")]
        public DateTime Datum { get; set; }

        public double Ukupno { get; set; }

        [Display(Name = "Arhivirana: ")]
        public bool Obrisana { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unosi> Unosi { get; set; }

        public static implicit operator Faktura(HashSet<Faktura> v)
        {
            throw new NotImplementedException();
        }
    }
}
