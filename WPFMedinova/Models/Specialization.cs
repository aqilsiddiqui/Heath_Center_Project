using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    // Class representing a medical specialization
    public class Specialization
    {
        // Primary key for the Specialization entity
        [Key]
        public int S_Id { get; set; }

        // Name of the specialization (optional)
        public string? S_Name { get; set; }
    }
}
