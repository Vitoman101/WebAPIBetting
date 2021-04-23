using System;
using System.Collections.Generic;

namespace WebAPIBetting.Models
{
    public partial class League
    {
        public League()
        {
            Fixture = new HashSet<Fixture>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fixture> Fixture { get; set; }
    }
}
