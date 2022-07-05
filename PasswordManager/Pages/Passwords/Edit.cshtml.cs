using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Pages.Passwords
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;

        [BindProperty]
        public PasswordData PasswordData { get; set; }

        public EditModel(ApplicationContext db)
        {
            _context = db;
        }

        /// <summary>
        /// Получение данных о пароле по id для вывода
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Получение данных по id
            PasswordData = await _context.PasswordData.FindAsync(id);

            if (PasswordData == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        /// <summary>
        /// Изменение данных о пароле в БД
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Проверка на валидность модели
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Присоединение PasswordData к контексту
            _context.Attach(PasswordData).State = EntityState.Modified;

            // Сохранение изменения в БД и переадресация на страницу Index
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PasswordData.Any(e => e.Id == PasswordData.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}