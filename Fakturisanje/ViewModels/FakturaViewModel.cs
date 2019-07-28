using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Fakturisanje.Models;

namespace Fakturisanje.ViewModels
{
    public class FakturaViewModel
    {
        public FakturaViewModel()
        {
            Stavka = new HashSet<Stavka>();
        }

        [Key]
        public string Id { get; set; }
        public Faktura Faktura { get; set; }
        public ICollection<Stavka> Stavka { get; set; }
        public Unosi Unosi { get; set; }
        public string skupStavkizaUnos { get; set; }
        public string skupKolicinazaUnos { get; set; }
    }
}