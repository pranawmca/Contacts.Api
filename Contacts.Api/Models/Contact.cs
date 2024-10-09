using System.ComponentModel.DataAnnotations;

namespace Contacts.Api.Models
{
    public class Contact
    {
        public int Id { get; set; }  // Auto-incrementing integer
        [Required]
        public string FirstName { get; set; }  // Required string
        [Required]
        public string LastName { get; set; }  // Required string
        [Required]
        [EmailAddress]  // Must be a valid email format
        public string Email { get; set; }
    }
}
