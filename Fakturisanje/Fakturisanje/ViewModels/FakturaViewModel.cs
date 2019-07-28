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
            Faktura = new HashSet<Faktura>();
            Stavka = new HashSet<Stavka>();
            Unosi = new HashSet<Unosi>();
        }

        [Key]
        public int Id { get; set; }
        public Faktura Faktura { get; set; }
        public ICollection<Stavka> Stavka { get; set; }
        public ICollection<Unosi> Unosi { get; set; }
    }
}