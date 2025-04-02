using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs
{
    public class FilmGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }
        public string Description { get; set; }
        public int? AgeRating { get; set; }
        //public List<Screening>? Screenings { get; set; }  //TODO!!! : Change to ScreeningGetDTO
    }
}
