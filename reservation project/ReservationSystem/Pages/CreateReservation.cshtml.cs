using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Pages
{
    public class CreateReservationModel : PageModel
    {
        private readonly HotelReservationContext _context;
        private readonly ILogger<CreateReservationModel> _logger;

        // Constructor with dependency injection for database context and logger
        public CreateReservationModel(HotelReservationContext context, ILogger<CreateReservationModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public IList<Room> Rooms { get; set; }

        // GET handler to initialize the Reservation object with room ID and user email
        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            _logger.LogInformation("OnGetAsync called with roomId: {RoomId}", roomId);
            Rooms = await _context.Rooms.ToListAsync();
            Reservation = new Reservation
            {
                RoomId = roomId,
                UserName = HttpContext.Session.GetString("UserEmail") ?? "Guest"
            };

            return Page();
        }

        // POST handler to add a new reservation
        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called with Reservation: {@Reservation}", Reservation);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid, reloading the page with errors.");
                Rooms = await _context.Rooms.ToListAsync(); // Reload rooms to maintain form data
                return Page();
            }

            // Ensure date values are set to UTC to avoid timezone issues
            Reservation.StartDate = DateTime.SpecifyKind(Reservation.StartDate, DateTimeKind.Utc);
            Reservation.EndDate = DateTime.SpecifyKind(Reservation.EndDate, DateTimeKind.Utc);

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync(); // Save the new reservation to the database

            _logger.LogInformation("Reservation created successfully for user {UserName}.", Reservation.UserName);
            return RedirectToPage("/Index"); // Redirect user after successful operation
        }
    }
}
