namespace Fakturisanje.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InitModel : DbContext
    {
        public InitModel()
            : base("name=InitModel")
        {
        }

        public virtual DbSet<Faktura> Faktura { get; set; }
        public virtual DbSet<Stavka> Stavka { get; set; }
        public virtual DbSet<Unosi> Unosi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
