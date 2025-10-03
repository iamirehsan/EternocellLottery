using System.ComponentModel.DataAnnotations;

namespace EternocellLottery.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string FullName { get; set; }
        public string? InstagramId { get; set; }
        public string? PhoneNumber { get; set; }

        public User(string fullName, string instagramId, string? phoneNumber)
        {

            FullName = fullName;
            InstagramId = instagramId;
            PhoneNumber = phoneNumber;
        }
    }
}
