using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class EnregistrementPersonne
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public string? Adresse { get; set; }
        [NotMapped]
        public string? Province { get; set; }
        [NotMapped]
        //[RegularExpression(@"",
        // ErrorMessage = "Characters are not allowed.")]
        public string? CodePostal { get; set; }
        [NotMapped]
        public string? Telephone { get; set; }
    }
}
