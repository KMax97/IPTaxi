using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
        }

        public int ClientId { get; set; }
        public string NumberOfTelephone { get; set; }
        public int? AmountOfOrders { get; set; }
        public int? AnountOfPoints { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
