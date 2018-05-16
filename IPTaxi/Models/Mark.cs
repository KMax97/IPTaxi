using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Mark
    {
        public Mark()
        {
            Ts = new HashSet<Ts>();
        }

        public int MarkId { get; set; }
        public string Name { get; set; }

        public ICollection<Ts> Ts { get; set; }
    }
}
