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
        /// ��������� ������ � ������ �� id ��� ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // ��������� ������ �� id
            PasswordData = await _context.PasswordData.FindAsync(id);

            if (PasswordData == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        /// <summary>
        /// ��������� ������ � ������ � ��
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // �������� �� ���������� ������
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ������������� PasswordData � ���������
            _context.Attach(PasswordData).State = EntityState.Modified;

            // ���������� ��������� � �� � ������������� �� �������� Index
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