using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasswordManager.Models;

namespace PasswordManager.Pages.Passwords
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationContext _context;

        [BindProperty]
        public PasswordData PasswordData { get; set; }

        public CreateModel(ApplicationContext db)
        {
            _context = db;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// Добавление объекта в Бд
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Проверка на валидность модели
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Сохранение объекта в БД и перенаправление на страницу Index
            _context.PasswordData.Add(PasswordData);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}