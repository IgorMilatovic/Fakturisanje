using Fakturisanje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakturisanje.ViewModels
{
    public class EditFakturaViewModel
    {
        public string Id { get; set; }
        public Faktura Faktura { get; set; }
        public ICollection<Stavka> Stavka { get; set; }
        public ICollection<Unosi> Unosi { get; set; }
    }
}