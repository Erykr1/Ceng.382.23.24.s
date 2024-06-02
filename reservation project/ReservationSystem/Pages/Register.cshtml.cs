using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationSystem.Models;  // Güncellenmiş isim alanı
using System.Threading.Tasks;
using ReservationSystem.Data;  // İsim alanı açıklamayla doğrulanmalı

namespace ReservationSystem.Pages
{
    public class RegisterModel : PageUser
    {
        private readonly ReservationContext _context; // Sınıf ismi tekrarı kaldırıldı

        public RegisterModel(ReservationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }  // User modeli bağlandı

        public void OnGet()  // Get işlemi için boş metod
        {
        }

        public async Task<IActionResult> OnPostAsync()  // Post işlemi asenkron olarak işleniyor
        {
            if (!ModelState.IsValid)
            {
                return Page();  // Model geçerli değilse aynı sayfaya yönlendir
            }

            _context.Users.Add(User);  // Kullanıcıyı veritabanına ekle
            await _context.SaveChangesAsync();  // Değişiklikleri kaydet

            return RedirectToPage("/Login");  // Giriş sayfasına yönlendir
        }
    }
}
