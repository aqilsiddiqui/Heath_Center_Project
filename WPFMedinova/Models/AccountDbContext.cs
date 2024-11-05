using Microsoft.EntityFrameworkCore;

namespace WPFMedinova.Models
{
   
    public class AccountDbContext : DbContext   // DbContext class for managing database operations related to account management
    {
        
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)  // Constructor that accepts DbContextOptions and passes them to the base class
        {


        }     
        public DbSet<LoginModel> Login_Table { get; set; }   // DbSet for login information       
        public DbSet<RegisterModel> Register_Table { get; set; }  // DbSet for patient registration details (User)     
        public DbSet<Create_Doctor_Model> Doctor_Table { get; set; }  // DbSet for storing doctor information
        public DbSet<AppointmentModel> Appointment_Table { get; set; }  // DbSet for managing appointments made by patients after registration     
        public DbSet<Specialization> specializations { get; set; }  // DbSet for specializations to support cascading dropdown functionality
    }
}
