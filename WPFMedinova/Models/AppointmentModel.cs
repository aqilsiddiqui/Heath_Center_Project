using System.ComponentModel.DataAnnotations; // Importing data annotations for model validation attributes

namespace WPFMedinova.Models
{
    public class AppointmentModel
    {
        [Key]                                                 // Marks Id as the primary key of the model
        public int Id { get; set; }                          // Unique identifier for each appointment

        public string? Patient_Name { get; set; }            // Stores the patient's name (nullable)

        public string? Specialization { get; set; }          // Stores the doctor's specialization (nullable)

        public string? Doctor { get; set; }                 // Stores the doctor's name (nullable)

        [DataType(DataType.DateTime)]                       // Specifies that this property represents a DateTime value
        public DateTime Appointment_Date { get; set; }      // Appointment date, changed to non-nullable to ensure a date is always set

        public string? Status { get; set; }                // Stores the status of the appointment (e.g., Confirmed, Pending)
    }
}
