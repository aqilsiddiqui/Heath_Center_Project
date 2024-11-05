using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    public class LoginModel
    {
        [Key]
        public int L_Id { get; set; }
        public string ?Username { get; set; }
        public string ?Password { get; set; }
        public string ?Role { get; set; }
        public string ?IsRemember { get; set; }
    }
}
