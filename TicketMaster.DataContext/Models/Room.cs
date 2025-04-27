using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketMaster.DataContext.Models;

    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        
        public String Name { get; set; }
        public string? Description { get; set; }
        
        [ForeignKey(nameof(RoomType))]
        public int? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        public int? MaxSeatRow { get; set; }
        public int? MaxSeatColumn { get; set; }
        
        [Required]
        public int Capacity { get; set; }
        public bool DisabilityFriendly { get; set; } = false;
        public int? ComfortLevel { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ConstructedAt { get; set; }
        public List<Screening> Screenings { get; set; } = new List<Screening>();
    }