using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Net;
using System.IO;
using System.Text;

namespace JobsPortal.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserController));
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: User
        //GET version of the NewUser action. It's responsible for rendering the registration form.
        //It simply returns a view called "NewUser" and initializes a new UserMV model.
        public ActionResult NewUser()
        {
            return View(new UserMV());
        }

        //Post: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserMV userMV,HttpPostedFileBase resume1)
        {
            bool hasValidationErrors = false;
            // Check if the ModelState is valid, meaning that all form validation rules have passed.
            // ModelState.IsValid checks if there are any validation errors on the submitted form.
            if (ModelState.IsValid)
            {
                
                    var checkUser = db.UserTables.Where(u => u.EmailAddress == userMV.EmailAddress).FirstOrDefault();
                    // Check if the email address provided in the form is already registered in the database.
                    // If it's found, an error message is added to the ModelState, and hasValidationErrors is set to true.
                    if (checkUser != null)
                    {
                        ModelState.AddModelError("EmailAddress", "Email is already registered");
                        hasValidationErrors = true;
                        return View(userMV);
                    }
                    // Similar to the previous block, this checks if the username is already registered in the database.
                    // If it's found, an error message is added to the ModelState, and hasValidationErrors is set to true.
                    checkUser = db.UserTables.Where(u => u.UserName == userMV.UserName).FirstOrDefault();
                    if (checkUser != null)
                    {
                        ModelState.AddModelError("UserName", "User Name is already registered");
                        hasValidationErrors = true;
                        return View(userMV);
                    }

                    // Start a database transaction to ensure data consistency.
                    using (var transact = db.Database.BeginTransaction())
                    {
                        try
                        {
                            // Create a new UserTable entity and set its properties based on the form data.
                            // The UserTypeID is determined based on whether the user is a provider or not.
                            string filepath = "";
                            if (resume1 != null && resume1.ContentLength > 0)

                            {
                            var supportedTypes = new[] { ".docx", ".doc", ".pdf" };
                            string filename = Path.GetFileNameWithoutExtension(resume1.FileName);
                                string extension = Path.GetExtension(resume1.FileName);
                                string uniqueFilename = $"{filename}_{userMV.UserName}{extension}";
                                filepath = Path.Combine(Server.MapPath("~/UploadResumes/"), uniqueFilename);
                            if (supportedTypes.Contains(extension))
                            {
                                resume1.SaveAs(filepath);


                                
                            }
                            else
                            {
                                ModelState.AddModelError("Resume", "please upload a valid format resume docx or doc or pdf ");

                                return View(userMV);
                            }
                            


                            }

                            var user = new UserTable();
                            user.UserName = userMV.UserName;
                            user.Password = userMV.Password;
                            user.ContactNo = userMV.ContactNo;
                            user.EmailAddress = userMV.EmailAddress;
                            user.Preferred_location = userMV.Preferred_location;
                            user.Skills = userMV.Skills;
                            user.Resume = filepath;
                            user.UserTypeID = userMV.AreYouProvider == true ? 2 : 3;

                            // Add the user to the database and save changes.
                            db.UserTables.Add(user);

                            db.SaveChanges();

                            if (userMV.AreYouProvider == true)
                            {
                                // Create a new CompanyTable entity associated with the user.
                                // Set its properties based on the form data.
                                // Validation checks for Company properties done
                                var company = new CompanyTable();
                                company.UserID = user.UserID;
                                if (string.IsNullOrEmpty(userMV.Company.EmailAddress))
                                {
                                    ModelState.AddModelError("Company.EmailAddress", "*Required");
                                    hasValidationErrors = true;
                                    return View(userMV);
                                }
                                if (string.IsNullOrEmpty(userMV.Company.CompanyName))
                                {
                                    ModelState.AddModelError("Company.CompanyName", "*Required");
                                    hasValidationErrors = true;
                                    return View(userMV);
                                }
                                if (string.IsNullOrEmpty(userMV.Company.PhoneNo))
                                {
                                    ModelState.AddModelError("Company.PhoneNo", "*Required");
                                    hasValidationErrors = true;
                                    return View(userMV);
                                }
                                else if (userMV.Company.PhoneNo.Length != 10 || !userMV.Company.PhoneNo.All(char.IsDigit))
                                {
                                    ModelState.AddModelError("Company.PhoneNo", "Please enter a 10-digit numeric phone number");
                                    hasValidationErrors = true;
                                    return View(userMV);
                                }

                                if (string.IsNullOrEmpty(userMV.Company.Description))
                                {
                                    ModelState.AddModelError("Company.Description", "*Required");
                                    hasValidationErrors = true;
                                    return View(userMV);
                                }
                                company.EmailAddress = userMV.Company.EmailAddress;
                                company.CompanyName = userMV.Company.CompanyName;
                                company.ContactNo = userMV.ContactNo;
                                company.PhoneNo = userMV.Company.PhoneNo;
                                company.Logo = "~/Content/assests/img/logo/logo.png";
                                company.Description = userMV.Company.Description;
                                // Add the company to the database and save changes.
                                db.CompanyTables.Add(company);
                                db.SaveChanges();
                            }
                            else if (userMV.AreYouProvider != true)
                            {
                                // Create a new EmployeesTable entity associated with the user.
                                // Set its properties based on the form data.
                                // Validation checks for User properties done
                                var employee = new EmployeesTable();
                                employee.UserId = user.UserID;
                                employee.EmailAddress = userMV.Employee.EmailAddress;
                                employee.EmployeeName = userMV.Employee.EmployeeName;
                                employee.Gender = userMV.Employee.Gender;
                                employee.Photo = "~/Content/assests/img/adapt_icon/3.png";

                            }
                            // If neither provider nor seeker, roll back the transaction.
                            else
                            {
                                transact.Rollback();
                            }
                            // Commit the transaction to save all changes to the database.
                            transact.Commit();
                            log.Info($"New user with username {user.UserName} created successfully.");
                            // Redirect to the login page after successful registration.
                            return RedirectToAction("Login");
                        }
                        // If any exception occurs during the transaction, add a generic error message to the ModelState.
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Please provide correct details!");
                            log.Error("Error while creating new user.", ex);
                            transact.Rollback();
                        }
                    }
                
            }
            // If ModelState.IsValid is false or any exception occurred, return the registration form view with error messages.
            return View(userMV);
        }

        // The GET version of the Login action, returns a view for user login and initializes a new UserLoginMV model.
        public ActionResult Login()
        {
            return View(new UserLoginMV());
        }

        //Login: User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginMV userLoginMV)
        {
            // Check if the ModelState is valid, meaning that all form validation rules have passed.
            if (ModelState.IsValid)
            {
                // Try to find a user with the provided username and password in the database.
                var user = db.UserTables.Where(u => u.UserName == userLoginMV.UserName && u.Password == userLoginMV.Password).FirstOrDefault();
                if (user == null)
                {
                    // Log a warning message indicating that the login attempt failed.
                    ModelState.AddModelError(string.Empty, "User Name or Password are incorrect");
                    log.Warn($"Login failed for username {userLoginMV.UserName}. Incorrect username or password.");
                    // Return to the login form with error messages.
                    return View(userLoginMV);
                }
                // Store user information in session variables.
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserTypeID"] = user.UserTypeID;
                // If the user is of type 2 (provider), store the associated company ID in a session variable.
                if (user.UserTypeID == 2)
                {
                    Session["CompanyID"] = user.CompanyTables.FirstOrDefault().CompanyID;
                }
                // If the user is of type 3 (seeker), store the associated employee ID in a session variable.
                if (user.UserTypeID == 3)
                {
                    var employeeTableEntry = user.EmployeesTables.FirstOrDefault();
                    if (employeeTableEntry != null)
                    {
                        Session["EmployeeID"] = employeeTableEntry.EmployeeID;
                    }
                }
                // Log a successful login event.
                log.Info($"User with username {user.UserName} logged in successfully.");
                // Redirect to the home page after successful login.
                return RedirectToAction("Index", "Home");
            }
            // If ModelState.IsValid is false (e.g., due to validation errors),
            // return to the login form with error messages.
            return View(userLoginMV);
        }

        public ActionResult Logout()
        {
            // Clear session variables and log the user logout event.
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["EmployeeID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            log.Info($"User logged out.");
            // Redirect to the home page after the user logs out.
            return RedirectToAction("Index", "Home");
        }

        //Get all Users for Admin
        // Action to retrieve a list of all users for administrators
        public ActionResult AllUsers()
        {
            // Check if the UserTypeID stored in the user's session is empty or null.
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            // If the user is authenticated and authorized (i.e., they have a UserTypeID),
            // retrieve a list of all users from the UserTables in the database.
            var users = db.UserTables.ToList();
            // Return a view called "AllUsers" and pass the list of users as the model.
            return View(users);
        }

        //Forgot Password
        //This is the HTTP GET method of the Forgot action. It is responsible for displaying the "Forgot Password" view.
        //It returns a view called "Forgot" and passes a new instance of the ForgotPasswordMV model to the view. 
        public ActionResult Forgot()
        {
            return View(new ForgotPasswordMV());
        }
        public ActionResult filteruser(string searching)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            var obj = new FilteruserMV();
         if(searching == null || searching == "")
            {
                searching = null;
            }
            var result = db.UserTables.Where(r => r.UserTypeID == 3 && ((r.Skills.Contains(searching) || r.Preferred_location.Contains(searching)) || searching == null)).ToList();
            obj.Result = result;
            //ViewBag.Skills = new SelectList(
            //                        db.JobCategoryTables.ToList(),
            //                        "JobCategoryID",
            //                        "JobCategory",
            //                        "0");
            //ViewBag.JobNatureID = new SelectList(
            //                        db.JobNatureTables.ToList(),
            //                        "JobNatureID",
            //                        "JobNature",
            //                        "0");
            return View(obj);


        }
        //public FileResult Download(string path)
        //{
        //    byte[] file = System.IO.File.ReadAllBytes(path);
        //    string filename = Path.GetFileName(path);
        //    return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        //    //return RedirectToAction("Index");
        //}
        public FileResult Download(string path)
        {
            try
            {
                if (path == null)
                {
                    // Handle the case where path is null, for example, by returning an error message or redirecting to an error page.
                    // You can customize this part based on your application's requirements.
                    return File(Encoding.UTF8.GetBytes("Invalid file path"), "text/plain", "error.txt");
                }

                byte[] file = System.IO.File.ReadAllBytes(path);
                string filename = Path.GetFileName(path);
                return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis (optional).
                // logger.LogError($"Error downloading file: {ex.Message}");

                // Redirect to an error page or return an error message.
                // You can customize this part based on your application's requirements.
                return File(Encoding.UTF8.GetBytes($"Error downloading file: {ex.Message}"), "text/plain", "error.txt");
            }
        }





        //This is the HTTP POST method of the Forgot action, which handles the form submission for password recovery
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotPasswordMV forgotPasswordMV)
        {
            if (forgotPasswordMV == null)
                return View();
            // Find a user in the database with the provided email address
            var user = db.UserTables.Where(u => u.EmailAddress == forgotPasswordMV.Email).FirstOrDefault();
            if (user != null)
            {
                // Create a string containing the username and password
                string userandpassword = "User Name: " + user.UserName + "\n" + "Password: " + user.Password;
                string body = userandpassword;

                // Attempt to send an email with the user's account details
                bool IsSendEmail = JobsPortal.Helper.Email.Emailsend(user.EmailAddress, "Account Details", body, true);
                if (IsSendEmail)
                {
                    // If the email was sent successfully, add a success message to the model state
                    ModelState.AddModelError(string.Empty, "Username and Password is sent!");
                    log.Info($"Password recovery email sent to {forgotPasswordMV.Email}.");
                }
                else
                {
                    // If there was an issue sending the email, add an error message to the model state
                    ModelState.AddModelError("Email", "Your Email is Registered! Current email sending is not working properly, please try again after some time ");
                    log.Warn($"Failed to send password recovery email to {forgotPasswordMV.Email}.");
                }
            }
            else
            {
                // If no user was found with the provided email address, add an error message to the model state
                ModelState.AddModelError("Email", "Email is not registered");
                log.Warn($"Email is not registered. Email typed: {forgotPasswordMV.Email}.");
            }
            return View(forgotPasswordMV);
        }

        // This is the HTTP GET method of the DeleteUser action.
        // It takes an optional id parameter, which represents the ID of the user we want to delete.
        public ActionResult DeleteUser(int? id)
        {
            // Check if the user is logged in and has admin rights.
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])) ||
                (Convert.ToInt32(Session["UserTypeID"]) == 2) ||
                (Convert.ToInt32(Session["UserTypeID"]) == 3))
            {
                return RedirectToAction("Login", "User");
            }
            // This checks if the id parameter is null.
            // If it's null, it returns a response with an HTTP status code of 400 (Bad Request). 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Fetch the user
            //This code attempts to find a user in the database based on the provided id.
            //If no user is found, it returns a response with an HTTP status code of 404 (Not Found)
            var user = db.UserTables.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            // It wraps the deletion process in a database transaction (transact).
            // This is done to ensure that if any part of the deletion process fails (e.g., an exception is thrown),
            // it can be rolled back to maintain data consistency.
            using (var transact = db.Database.BeginTransaction())
            {
                // Here we delete the user from the UserTables table,
                // save the changes to the database, commit the transaction (if all operations were successful),
                // and log the successful deletion of the user with the specified ID.
                try
                {
                    // If user is a provider, delete the associated company
                    if (user.UserTypeID == 2 && user.CompanyTables.Any())
                    {
                        var company = user.CompanyTables.FirstOrDefault();
                        db.CompanyTables.Remove(company);
                    }
                    // If user is an employee, delete the associated employee record
                    else if (user.UserTypeID == 3 && user.EmployeesTables.Any())
                    {
                        var employee = user.EmployeesTables.FirstOrDefault();
                        db.EmployeesTables.Remove(employee);
                    }

                    // Delete the user
                    db.UserTables.Remove(user);
                    db.SaveChanges();

                    transact.Commit();

                    log.Info($"DeleteUser - Successfully deleted user with ID: {id}");
                }
                catch (Exception ex)
                {
                    log.Error($"Error deleting user with ID: {id}.", ex);
                    transact.Rollback();
                }
            }

            return RedirectToAction("AllUsers");
        }

        // This is part of the ASP.NET MVC controller and deals with resource cleanup,
        // specifically when disposing of the controller. 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}