
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Models
{
    public class PasswordData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Comment { get; set; } = null;
        public string? Tags { get; set; } = null;
    }
}