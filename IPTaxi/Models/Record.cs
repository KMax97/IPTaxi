using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Record
    {
        public Record()
        {
            Order = new HashSet<Order>();
        }

        public int NumberOfRecord { get; set; }
        public int DriverId { get; set; }
        public DateTime Intime { get; set; }
        public DateTime? Outtime { get; set; }

        public Driver Driver { get; set; }
        public ICollection<Order> Order { get; set; }
    }
}
