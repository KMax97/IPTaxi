using System;
using System.Collections.Generic;

namespace IPTaxi.Models
{
    public partial class Color
    {
        public Color()
        {
            Ts = new HashSet<Ts>();
        }

        public int ColorId { get; set; }
        public string Name { get; set; }

        public ICollection<Ts> Ts { get; set; }
    }
}
