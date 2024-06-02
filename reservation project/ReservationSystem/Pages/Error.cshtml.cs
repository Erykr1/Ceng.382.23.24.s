using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Reservation.Pages;

// Bu sınıf, hata sayfaları için kullanılır ve otomatik olarak önbelleğe alma ve CSRF koruması devre dışı bırakılır.
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    // RequestId'nin gösterilip gösterilmeyeceğini belirler.
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;

    // Logger, hata detaylarının loglanması için enjekte edilir.
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Aktif isteğin ID'sini veya HttpContext'ten gelen Trace Identifier'ı alır.
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
