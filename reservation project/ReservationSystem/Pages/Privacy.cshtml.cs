using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// 'Reservation.Pages' ad alanını kullanarak Privacy sayfası için PageModel sınıfı.
namespace Reservation.Pages
{
    // PrivacyModel, Gizlilik Politikası sayfasının yönetimi için kullanılır.
    public class PrivacyModel : PageModel
    {
        // ILogger interface'ini kullanarak hata ayıklama ve bilgi kaydı yapabilme.
        private readonly ILogger<PrivacyModel> _logger;

        // Constructor ile logger nesnesi alınır.
        public PrivacyModel(ILogger<PrivacyStore> logger)
        {
            _logger = logger;
        }

        // GET isteği üzerine çalışır, özel bir işlem yapılmasına gerek yoktur.
        public void OnGet()
        {
            // Gizlilik politikası sayfasına yapılan GET isteği bu noktada yönetilir.
        }
    }
}
