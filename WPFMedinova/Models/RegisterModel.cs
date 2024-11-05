using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    public class RegisterModel
    {
        [Key]
        public int R_Id { get; set; }  // Primary Key for registration ID

        [Required(ErrorMessage = "Enter First Name")]
        public string? FirstName { get; set; }  // Patient's first name

        [Required(ErrorMessage = "Enter Last Name")]
        public string? LastName { get; set; }  // Patient's last name

        [Required(ErrorMessage = "Enter Age")]
        public int? Age { get; set; }  // Patient's age

        [Required(ErrorMessage = "Select Gender")]
        public string? Gender { get; set; }  // Patient's gender

        [Required(ErrorMessage = "Enter Mobile No")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? MobileNo { get; set; }  // Patient's mobile number with validation

        [Required(ErrorMessage = "Enter Address")]
        public string? Address { get; set; }  // Patient's address

        [Required(ErrorMessage = "Select Role")]
        public string? Role { get; set; }  // Role in the system (e.g., Doctor, Patient)

        [Required(ErrorMessage = "Enter Username")]
        public string? Username { get; set; }  // Username for login

        [Required(ErrorMessage = "Enter Password")]
        public string? Password { get; set; }  // Password for login

        [Required(ErrorMessage = "Set Password")]
        public string? Confirm_Password { get; set; }  // Password confirmation field
    }
}
