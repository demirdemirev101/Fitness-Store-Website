using System.ComponentModel.DataAnnotations;

namespace Fitness_Store_Website.Models.User
{
    public class BecomeAdminViewModel
    {
        [Required(ErrorMessage = "Моля, въведи телефонен номер")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string PhoneNumber { get; set; } = null!;
    }
}
