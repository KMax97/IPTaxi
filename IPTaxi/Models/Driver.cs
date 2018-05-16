using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Record = new HashSet<Record>();
        }

        public int DriverId { get; set; }
        public string Fio { get; set; }
        public string PassportData { get; set; }
        public string TelephoneNumber { get; set; }
        public string RegistrationNumber { get; set; }

        public Ts RegistrationNumberNavigation { get; set; }
        public ICollection<Record> Record { get; set; }
    }
}
