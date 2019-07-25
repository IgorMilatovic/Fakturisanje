namespace Fakturisanje.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Unosi")]
    public partial class Unosi
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string IdFakture { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdStavke { get; set; }

        public int Kolicina { get; set; }

        public virtual Faktura Faktura { get; set; }

        public virtual Stavka Stavka { get; set; }
    }
}
