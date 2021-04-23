using System;
using System.Collections.Generic;

namespace WebAPIBetting.Models
{
    public partial class Fixture
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public virtual League League { get; set; }
    }
}
