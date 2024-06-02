using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
<paste code here>using System.Linq;
using System.Threading.Tasks;
using ReservationSystem.Data;
using ReservationSystem.Models;

namespace ReservationSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ReservationContext _context;
        private readonly ILogger<IndexModel> _logger;

        // Constructor, bağımlılıkların enjekte edilmesi
        public IndexModel(ReservationContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Room Room { get; set; } = new Room();
        public IList<Room> Rooms { get; set; } = new List<Room>();
        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();
        public IList<Reservation> ReservedRooms { get; set; } = new List<Reservation>();

        // Kullanıcı filtrelemeleri
        public string FilterRoomName { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }
        public int? FilterCapacity { get; intended>

        // Oda ve rezervasyon bilgilerinin getirilmesi
        public async Task OnGetAsync(string filterRoomName, DateTime? filterStartDate, DateTime? filterEndDate, int? filterCapacity)
        {
            ApplyFilters(filterRoomName, filterStartDate, filterEndDate, filterCapacity);

            Rooms = await _context.Rooms.Include(r => r.Reservations).ToListAsync();
            ReservedRooms = await _context.Reservations.Include(r => r.Room).ToListAsync();
        }

        // Oda ekleme işlemi
        public async Task<IActionResult> OnPostCreateRoomAsync()
        {
            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                return Page();
            }

            LogInformation("Oda ekleniyor", Room);
            _context.Rooms.Add(Room);
            await SaveChangesAsync("Oda kaydedildi", "Oda kaydedilirken hata oluştu");
            return RedirectToPage();
        }

        // Rezervasyon güncelleme işlemi
        public async Task<IActionResult> OnPostEditReservationAsync()
        {
            if (!ModelState.IsValid)
            {
                LogModelStateErrors();
                return Page();
            }

            var reservationToUpdate = await _context.Reservations.FindAsync(Reservation.Id);
            if (reservationToUpdate == null)
            {
                LogWarning("Rezervasyon bulunamadı", Reservation.Id);
                return NotFound();
            }

            UpdateReservationDates(reservationToUpdate, Reservation.StartDate, Reservation.EndDate);
            await SaveChangesAsync("Rezervasyon güncellendi", "Rezervasyon güncellenirken hata oluştu");
            return RedirectToPage();
        }

        // Rezervasyon silme işlemi
        public async Task<IActionResult> OnPostDeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                LogWarning("Rezervasyon bulunamadı", id);
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await SaveChangesAsync("Rezervasyon silindi", "Rezervasyon silinirken hata oluştu");
            return RedirectToPage();
        }

        // Oda silme işlemi
        public async Task<IActionResult> OnPostDeleteRoomAsync(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
            {
                LogWarning("Oda bulunamadı", roomId);
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await SaveChangesAsync("Oda silindi", "Oda silinirken hata oluştu");
            return RedirectToPage();
        }

        // Oda mevcutluğunu kontrol etme
        public bool IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate)
        {
            return !_context.Reservations.Any(r => r.RoomId == roomId &&
                    ((r.StartDate <= startDate && r.EndDate >= startDate) ||
                     (r.StartDate <= endDate && r.EndDate >= endDate) ||
                     (r.StartDate >= startDate && r.EndDate <= endDate)));
        }

        // Yardımcı metodlar
        private void ApplyFilters(string roomName, DateTime? startDate, DateTime? endDate, int? capacity)
        {
            if (!string.IsNullOrEmpty(roomName))
                FilterRoomName = roomName;
            if (startDate.HasValue)
                FilterStartDate = startDate;
            if (endDate.HasValue)
                FilterEndDate = endDate;
            if (capacity.HasValue)
                FilterCapacity = capacity;
        }

        private void LogModelStateErrors()
        {
            _logger.LogWarning("ModelState geçersiz: {@ModelState}", ModelState);
        }

        private void LogInformation(string message, object obj)
        {
            _logger.LogInformation($"{message}: {@obj}");
        }

        private void LogWarning(string message, object obj)
        {
    _logger.LogWarning($"{message}: {obj}");
}

private void UpdateReservationDates(Reservation reservation, DateTime startDate, DateTime endDate)
{
    reservation.StartDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
    reservation.EndDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);
}

private async Task SaveChangesAsync(string successLog, string errorLog)
{
    try
    {
        await _context.SaveChangesAsync();
        _logger.LogInformation(successLog);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, errorLog);
    }
}
