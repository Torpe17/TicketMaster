using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMaster.Services.DTOs.FilmDTOs
{
    public class FilmPutDTO
    {
        public string? Title { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }
        public int? Length { get; set; }
        public string? Description { get; set; }
        public int? AgeRating { get; set; }
        public bool SetAgeRating { get; set; } = false;
        public string? PictureBase64 { get; set; }
        public bool RemovePicture { get; set; }
    }
}
