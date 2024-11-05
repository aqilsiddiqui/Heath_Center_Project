using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
{
    public class AuthSecurityController : Controller
    {
        private readonly AccountDbContext _dbcontext;

        public AuthSecurityController(AccountDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // Admin login action
        public IActionResult Index()
        {
            return View();
        }

        // List all doctors
        public IActionResult List_Doctor()
        {
            var listofdoctors = _dbcontext.Doctor_Table.ToList();
            return View(listofdoctors);
        }

        // List all patients
        public IActionResult List_Patients()
        {
            ViewBag.role = HttpContext.Session.GetString("role");
            var listofpatients = _dbcontext.Register_Table.ToList(); // when doctor login
            return View(listofpatients);
        }

        // Patient details for patient login
        public IActionResult Patient_Details()
        {
            string username = HttpContext.Session.GetString("username");
            var PatientDetails = _dbcontext.Register_Table.FirstOrDefault(m => m.Username == username);
            return View(PatientDetails);
        }

        // List appointments based on role
        public IActionResult Appointment_List()
        {
            string username = HttpContext.Session.GetString("username");      // Retrieve the username from session
            string name = HttpContext.Session.GetString("name");
            ViewBag.role = HttpContext.Session.GetString("role");            // Retrieve the user role from session and store in ViewBag

            if (ViewBag.role == "Admin")
            {
                var list = _dbcontext.Appointment_Table.ToList();     // Get all appointments for Admin
                return View(list);                                 // Return the full appointment list to the view
            }
            else if (ViewBag.role == "Doctor")
            {
                var list = _dbcontext.Appointment_Table.Where(x=>x.Status == "Pending").OrderBy(x => x.Id).ToList();   // Get pending appointments for Doctor,ordered by ID
                return View(list);                                                                                   // Return the pending appointments list to the view
            }
            else
            {
                var list = _dbcontext.Appointment_Table.Where(x => x.Patient_Name == name).OrderBy(x => x.Id).ToList(); // Get appointments specific to the patient,ordered by ID

                return View(list);                   // Return the patient's appointment list to the view
            }
        }

        // Approve appointment (GET)
        public async Task<IActionResult> Approve(int id)
        {
            var data = await _dbcontext.Appointment_Table.FindAsync(id);  // Find the appointment by ID
            return View(data);                                              // Return the appointment data to the view
        }

        // Approve appointment (POST)
        [HttpPost]
        public async Task<IActionResult> Approve(AppointmentModel appObj)
        {   
            if (ModelState.IsValid)          // Check if model validation passes
            {
                _dbcontext.Entry(appObj).State = EntityState.Modified;                            // Mark the appointment as modified
                int n = await _dbcontext.SaveChangesAsync();                                      // Save changes to the database
                TempData["approve"] = n != 0 ?
                    "<script>alert('Status Approved Successfully!!')</script>" :                   // Show success message if save is successful
                    "<script>alert('Status Failed!!')</script>";

                return RedirectToAction("Appointment_List");                                       // Redirect to the appointment list page
            }
            ModelState.AddModelError(string.Empty, "Error in Model");                              // Add error to model state if validation fails
            return View();                                                                         // Return the view with validation error
        }

        // Reject appointment
        public IActionResult Reject(int id)
        {
            if (ModelState.IsValid)
            {
                AppointmentModel appObj = _dbcontext.Appointment_Table.Find(id);
                if (appObj != null)
                {
                    appObj.Status = "Rejected";
                    _dbcontext.Entry(appObj).State = EntityState.Modified;
                    int n = _dbcontext.SaveChanges();
                    TempData["reject"] = n != 0 ?
                        "<script>alert('Status Rejected Successfully!!')</script>" :
                        "<script>alert('Status Failed!!')</script>";
                    return RedirectToAction("List_Patients", "AuthSecurity");
                }
            }
            ModelState.AddModelError(string.Empty, "Error in Model");
            return View();
        }

        // List approved appointments
        public IActionResult Approved_List()
        {
            var approvedlist = _dbcontext.Appointment_Table.Where(m => m.Status == "Approved").ToList();
            return View(approvedlist);
        }

        // Edit Action (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _dbcontext.Appointment_Table.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // Edit Action (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(AppointmentModel appObj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Entry(appObj).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                TempData["edit"] = "<script>alert('Appointment updated successfully!')</script>";
                return RedirectToAction("Appointment_List");
            }
            return View(appObj);
        }

        // Delete Action (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _dbcontext.Appointment_Table.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // Delete Action (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _dbcontext.Appointment_Table.FindAsync(id);
            if (appointment != null)
            {
                _dbcontext.Appointment_Table.Remove(appointment);
                await _dbcontext.SaveChangesAsync();
                TempData["delete"] = "<script>alert('Appointment deleted successfully!')</script>";
            }
            return RedirectToAction("Appointment_List");
        }
        // Edit Patient Details (GET)
        public async Task<IActionResult> EditPatient(int id)
        {
            var patient = await _dbcontext.Register_Table.FindAsync(id); // Retrieve patient details
            if (patient == null)
            {
                return NotFound(); // Return 404 if patient not found
            }
            return View(patient); // Return view with patient details
        }

        // Edit Patient Details (POST)
        [HttpPost]
        public async Task<IActionResult> EditPatient(RegisterModel patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Entry(patient).State = EntityState.Modified;                     // Mark the patient as modified
                    await _dbcontext.SaveChangesAsync();                                         // Save changes asynchronously
                    TempData["editPatient"] = "Patient details updated successfully!";           // Set success message
                    return RedirectToAction("List_Patients");                                     // Redirect to patient list
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists, see your system administrator."); // Add error message to ModelState if an exception occurs
                }
            }
            return View(patient); // Return view with current patient details if invalid
        }

    }
}