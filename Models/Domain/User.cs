using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace Library.Models.Domain

{
    public class User : IdentityUser
    {
        [Key]
        [Required(ErrorMessage = "Pole UserName jest wymagane.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole Password jest wymagane.")]
        public string Password { get; set; }
      
        public string Email { get; set; }

     
    }
}
