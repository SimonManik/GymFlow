
using System.ComponentModel.DataAnnotations;

namespace GymFlow.Models
{
    public class Member
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime MembershipExpiry { get; set; }


    }
}
