using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Pages.Passwords
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public ICollection<PasswordData> DisplayedPasswords { get; set; }
        public SelectList DisplayedTags { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedTag { get; set; }

        public IndexModel(ApplicationContext db)
        {
            _context = db;
        }

        /// <summary>
        /// Вывод данных на страницу и сортировка при выводе
        /// </summary>
        public async Task OnGet()
        {      
            // Получение записей и значений тегов из них
            var passwords = from x in _context.PasswordData select x;
            var tagsQuery = from x in passwords select x.Tags;
            
            // Сортировка по выбранному тегу
            if (!string.IsNullOrEmpty(SelectedTag))
            {
                passwords = passwords.Where(x => x.Tags.Contains(SelectedTag));
            }

            // Парсинг тегов по пробелу
            List<string> tags = new();
            foreach(string t in tagsQuery)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    List<string> splittedTags = t.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                    tags.AddRange(splittedTags);
                }                
            }

            //Инициализация поля Tags, к которому привязан select тегов
            DisplayedTags = new SelectList(tags.Distinct().ToList());

            // Инициализация поля DisplayedPasswords, из которого происходит вывод таблицу
            DisplayedPasswords = await passwords.ToListAsync();
        }

        /// <summary>
        /// Удаление объекта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Получение объекта по id
            var passwordData = await _context.PasswordData.FindAsync(id);

            // Если объект найден, удаляем его из БД
            if (passwordData != null)
            {
                _context.PasswordData.Remove(passwordData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
