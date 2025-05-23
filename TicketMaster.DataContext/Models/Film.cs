﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TicketMaster.DataContext.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }
        public string Description { get; set; }
        public int? AgeRating { get; set; }
        public List<Screening> Screenings { get; set; } = new();
        [Column(TypeName = "varbinary(max)")]
        public byte[]? Picture { get; set; }
    }
}
