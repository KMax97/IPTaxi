using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Dispetcher
    {
        public Dispetcher()
        {
            Order = new HashSet<Order>();
        }

        public int DispetcherId { get; set; }
        public string Fio { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
