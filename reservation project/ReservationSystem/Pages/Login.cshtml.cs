using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationSystem.Models;  // Daha kısa namespace kullanımı
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ReservationContext _context;  // Sınıf ismi tekrarı kaldırıldı

        public LoginModel(ReservationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            // GET isteğinde yapılacak özel bir işlem yok
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();  // Model geçerli değilse aynı sayfaya yönlendir
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);  // Kullanıcıyı doğrula

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return Page();
            }

            // Oturum bilgisini ayarla
            HttpContext.Session.SetString("UserEmail", user.Email);

            // Başarılı girişten sonra ana sayfaya yönlendir
            return RedirectToPage("/Index");
        }
    }
}
