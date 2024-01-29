using System.ComponentModel.DataAnnotations;

namespace Restro.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}