using System.ComponentModel.DataAnnotations;

namespace EternocellLottery
{
    public record CreateUserCommand
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
