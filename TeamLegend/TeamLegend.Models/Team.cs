﻿namespace TeamLegend.Models
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime? DateOfFoundation { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsConceded { get; set; }

        public int GoalDifference => this.GoalsScored - this.GoalsConceded;

        public int TotalPoints { get; set; }

        public string BadgeVersion { get; set; }

        public string StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }

        public string LeagueId { get; set; }
        public virtual League League { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
