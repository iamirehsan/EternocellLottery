using System.ComponentModel.DataAnnotations;

namespace EternocellLottery
{
    public record CreateUserCommand
    {
        [Required]
        public string FullName { get; set; }
        public string InstagramId { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
