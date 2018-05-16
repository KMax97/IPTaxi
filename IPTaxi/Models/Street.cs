using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Street
    {
        public Street()
        {
            OrderFinalStreet = new HashSet<Order>();
            OrderStartStreet = new HashSet<Order>();
        }

        public int StreetId { get; set; }
        public string Name { get; set; }

        public ICollection<Order> OrderFinalStreet { get; set; }
        public ICollection<Order> OrderStartStreet { get; set; }
    }
}
