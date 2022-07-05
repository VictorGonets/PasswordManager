using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PasswordManager.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Перенапраление на сегмент Passwords после запуска
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            return RedirectToPage("Passwords/Index");
        }
    }
}