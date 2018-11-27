﻿namespace TeamLegend.Models
{
    using System;

    public class Player
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.UtcNow.Year - this.DateOfBirth.Year;
                if (this.DateOfBirth > DateTime.UtcNow.AddYears(-age))
                    age--;

                return age;
            }
        }

        public string PlayingPosition { get; set; }

        public decimal? Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public double Height { get; set; }

        public int Appearances { get; set; }

        public int GoalsScored { get; set; }

        public string CurrentTeamId { get; set; }
        public virtual Team CurrentTeam { get; set; }
    }
}
