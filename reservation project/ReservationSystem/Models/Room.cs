using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Room name must be between 1 and 100 characters.")]
        public string Name { get; set; } = string.Empty;  // Null atanamaz özellikler

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000.")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "View description is required.")]
        [StringLength(200, ErrorMessage = "View description can not exceed 200 characters.")]
        public string View { get; set; } = string.Empty;

        // Entity Framework için navigasyon özelliği, odaya ait rezervasyonları listeler
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();  // Null atanamaz özellikler
    }
}
