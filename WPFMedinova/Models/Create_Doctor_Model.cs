using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    public class Create_Doctor_Model
    {
        [Key]
        public int D_Id { get; set; }

        [Required(ErrorMessage = "Enter First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Enter Age")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Select Gender")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Enter Mobile No")]

        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage = "Enter Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Enter Select Role")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "Enter Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Set Password")]
        public string ?Confirm_Password { get; set; }

        [Required(ErrorMessage = "Enter Specialization")]
        public string ?Specialization { get; set; }

        [Required(ErrorMessage = "Enter Qualification")]
        public string ?Qualification { get; set; }

        [Required(ErrorMessage = "Enter Experience")]
        public int Experience { get; set; }
    }
}
