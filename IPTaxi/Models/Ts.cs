using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Ts
    {
        public Ts()
        {
            Driver = new HashSet<Driver>();
        }

        public string RegistrationNumber { get; set; }
        public int MarkId { get; set; }
        public int ColorId { get; set; }

        public Color Color { get; set; }
        public Mark Mark { get; set; }
        public ICollection<Driver> Driver { get; set; }
    }
}
