using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
{
    // Controller for managing specializations
    public class SpecializationController : Controller
    {
        private AccountDbContext _dbContext; // Database context for accessing the database

        // Constructor that initializes the AccountDbContext
        public SpecializationController(AccountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Action method to display the index view
        public IActionResult Index()
        {
            return View();
        }

        // Action method to display the view for adding a new specialization
        public IActionResult Add_Specialization()
        {
            return View();
        }

        // POST action method to add a new specialization to the database
        [HttpPost]
        public IActionResult Add_Specialization(Specialization spObj)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                _dbContext.specializations.Add(spObj); // Add the specialization to the context
                int n = _dbContext.SaveChanges(); // Save changes to the database
                if (n != 0) // Check if the save operation was successful
                {
                    TempData["insert"] = "<script>alert('Specialization Added Successfully!!')</script>"; // Show success message
                    return View(); // Return the view
                }
                else
                {
                    TempData["insert"] = "<script>alert('Specialization Failed!!')</script>"; // Show failure message
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model"); // Add model error if validation fails
            }
            return View(); // Return the view regardless of the outcome
        }

        // Uncomment this method to enable fetching specializations as JSON
        //public JsonResult GetSpecialization()
        //{
        //    var Specialization = _dbContext.specializations.ToList(); // Retrieve all specializations from the database
        //    return new JsonResult(Specialization); // Return the specializations as a JSON result
        //}
    }
}
