using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs
{
    public class FilmPostDTO
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Length { get; set; }
        public string Description { get; set; }
        public int? AgeRating { get; set; }
        public IFormFile? Picture { get; set; }
        public byte[]? PictureBytes { get; set; }
    }
}
