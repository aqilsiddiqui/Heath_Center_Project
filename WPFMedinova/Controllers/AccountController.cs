using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WPFMedinova.Models;

namespace WPFMedinova.Controllers
{
    public class AccountController : Controller
    {
        private AccountDbContext _dbcontext;
        public AccountController(AccountDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(RegisterModel logObj)
        {
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            HttpContext.Session.SetString("role", logObj.Role);
            HttpContext.Session.SetString("username", logObj.Username);
            HttpContext.Session.SetString("name", (logObj.FirstName + " " + logObj.LastName));

            string role = HttpContext.Session.GetString("role");

            if (role == "Admin")
            {
                HttpContext.Session.SetString("name", "Admin");
            }
            else if (role == "Doctor")
            {
                var data_ = _dbcontext.Doctor_Table.Where(m => m.Username == logObj.Username).ToList();
                HttpContext.Session.SetString("name", (data_[0].FirstName + " " + data_[0].LastName));
            }
            else
            {
                // Patient
                var data_ = _dbcontext.Register_Table.Where(m => m.Username == logObj.Username).ToList();
                HttpContext.Session.SetString("name", (data_[0].FirstName + " " + data_[0].LastName));
            }

            if (string.IsNullOrEmpty(logObj.Username) && string.IsNullOrEmpty(logObj.Password))
            {
                return RedirectToAction("Login");
            }

            if (logObj.Username == "Admin" && logObj.Password == "Admin@123" && logObj.Role == "Admin")
            {
                //Create the identity for Admin
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, logObj.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var Principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal);
                return RedirectToAction("Index", "AuthSecurity");
            }
            else
            {
                if (logObj.Role == "Doctor")
                {
                    var data = _dbcontext.Doctor_Table.Where(m => m.Username == logObj.Username && m.Password == logObj.Password);
                    if (data.Count() > 0)
                    {
                        identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, logObj.Username),
                            new Claim(ClaimTypes.Role, "Doctor")
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthenticated = true;
                        return RedirectToAction("List_Patients", "AuthSecurity");
                    }
                }
                else if (logObj.Role == "Patient")
                {
                    var data2 = _dbcontext.Register_Table.Where(m => m.Username == logObj.Username && m.Password == logObj.Password).ToList();
                    if (data2.Count() > 0)
                    {
                        TempData["username"] = data2[0].Username;
                        TempData["password"] = data2[0].Password;
                        TempData["mobileno"] = data2[0].MobileNo;

                        HttpContext.Session.SetString("username", data2[0].Username);

                        //Create the identity for Patient
                        identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, logObj.Username),
                            new Claim(ClaimTypes.Role, "Patient")
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthenticated = true;
                        return RedirectToAction("Patient_Details", "AuthSecurity");
                    }
                }
                else
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Invalid Credentials')</script>";
                    return RedirectToAction("Login");
                }
            }
            if (isAuthenticated)
            {
                var Principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal);
                return RedirectToAction("Index", "AuthSecurity");
            }
            else
            {
                // Removed alert script from TempData
                TempData["insert"] = "<script>alert('Invalid Credentials')</script>";
                return RedirectToAction("Login");
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel regObj)
        {
            TempData["firstname"] = regObj.FirstName;
            TempData["lastname"] = regObj.LastName;
            TempData["mobileno"] = regObj.MobileNo;
            if (ModelState.IsValid)
            {
                if (regObj.Password == regObj.Confirm_Password)
                {
                    _dbcontext.Register_Table.Add(regObj);
                    int n = await _dbcontext.SaveChangesAsync();
                    if (n != 0)
                    {
                        // Removed alert script from TempData
                        TempData["insert"] = "<script>alert('Patient Added Successfully!!')</script>";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        // Removed alert script from TempData
                        TempData["insert"] = "<script>alert('Patient Failed!!')</script>";
                    }
                }
                else
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Wrong Password!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Create_Doctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create_Doctor(Create_Doctor_Model docObj) //doctor registration by admin
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Doctor_Table.Add(docObj);
                int n = await _dbcontext.SaveChangesAsync();
                if (n != 0)
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Doctor Added Successfully!!')</script>";
                    return RedirectToAction("Index", "AuthSecurity");
                }
                else
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Doctor Failed!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }

        public IActionResult Appointment() // After patient registration, appointment needs to be booked
        {
            string username = HttpContext.Session.GetString("username");
            var data = _dbcontext.Register_Table.Where(m => m.Username == username).ToList();
            ViewBag.name = data[0].FirstName + " " + data[0].LastName;
            ViewBag.mobile_no = data[0].MobileNo;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Appointment(AppointmentModel appObj)
        {
            if (ModelState.IsValid)
            {
                appObj.Status = "Pending";
                _dbcontext.Appointment_Table.Add(appObj);
                int n = await _dbcontext.SaveChangesAsync();
                if (n != 0)
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Appointment Booked Successfully!!')</script>";
                    return RedirectToAction("Appointment_List", "AuthSecurity");
                }
                else
                {
                    // Removed alert script from TempData
                    TempData["insert"] = "<script>alert('Appointment Booking Failed!!')</script>";
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error in Model");
            }
            return View();
        }
    }
}
