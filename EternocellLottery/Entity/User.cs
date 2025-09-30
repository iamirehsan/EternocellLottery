using System.ComponentModel.DataAnnotations;

namespace EternocellLottery.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public User(string firstName, string lastName, string? phoneNumber)
        {

            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}
