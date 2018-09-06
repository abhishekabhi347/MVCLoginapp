using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Loginapplication.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Loginapplication.Controllers
{

    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //Session.Abandon();


            using (EmpDbContext dba = new EmpDbContext())
            {
                var ado = dba.Employees.ToList();
                return View();
            }
            //return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    using (LoginDbContext db = new LoginDbContext())
                    {
                        // var result = db.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);

                        var result = (from s in db.Users where s.UserName == UserName || s.Email == UserName select s).FirstOrDefault();
                        if (result != null)
                        {
                            var hashCode = result.Vcode;
                            //Password Hasing Process Call Helper Class Method    
                            var encodingPasswordString = Helper.EncodePassword(Password, hashCode);
                            //Check Login Detail User Name Or Password    
                            var query = (from s in db.Users where (s.UserName == UserName || s.Email == UserName) && s.Password.Equals(encodingPasswordString) select s).FirstOrDefault();
                            var empdb = new EmpDbContext();
                            var getrole = (from s in empdb.Roles where (s.Roleid == query.Roleid) select s).FirstOrDefault();
                            if (query != null)
                            {
                                //RedirectToAction("Details/" + id.ToString(), "FullTimeEmployees");    
                                //return View("../Admin/Registration"); url not change in browser  
                                Session["EmpName"] = Convert.ToString(result.UserName);
                                Session["EmpID"] = Convert.ToInt32(result.Empid);
                                Session["RoleID"] = Convert.ToInt32(result.Roleid);
                                Session["Role"] = Convert.ToString(getrole.RoleName);
                                Session["SessionExpire"] = false;

                                return RedirectToAction("About");
                            }
                            ViewBag.Message = "Invalid User Name or Password";
                            return View();
                        }
                        ViewBag.Message = "Invalid User Name or Password";
                        return View();

                    }
                }

                catch (Exception e)
                {
                    ViewBag.Message = " Error!!! contact cms@info.in";
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        public ActionResult About()
        {
            if (Session["EmpName"] != null)
            {
                ViewBag.Message = "Your application description page.";

                return View();

            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult Contact()
        {
            if (Session["EmpName"] != null)
            {
                ViewBag.Message = "Your contact page.";
                var employees = new EmpDbContext();
                return View(employees.Employees.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            ViewBag.Message = "Succesfully Logged Out";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Register()
        {
            var p = new User();
            using (EmpDbContext db = new EmpDbContext())
            {

                p.RoleList = db.Roles.ToList();
            }
            return View(p);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (LoginDbContext db = new LoginDbContext())
                    {

                        var chkUser = (from s in db.Users where s.UserName == user.UserName || s.Email == user.Email select s).FirstOrDefault();
                        if (chkUser == null)
                        {
                            var keyNew = Helper.GeneratePassword(10);
                            var password = Helper.EncodePassword(user.Password, keyNew);
                            user.Password = password;
                            //user.Checkstatus = "Y";
                            user.Vcode = keyNew;
                            db.Users.Add(user);
                            db.SaveChanges();
                            ModelState.Clear();
                            ViewBag.Message = "Registration Successful";

                            //var emailpassword = "********";
                            //var sub = "Sample application Registration";
                            //var body = "You have been succesfully registered in Sample application";


                            //RegEmail(user.Email, sub, body, emailpassword);

                            return RedirectToAction("Index", "Home");
                        }

                        ViewBag.Message = "User Alredy Exixts!!!!!!!!!!";
                        return View();

                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Some exception occured" + e;
                    return View("Error");
                }

            }
            else
            {
                ViewBag.Message = "Registration Failed";
                return RedirectToAction("Register");
            }
        }


        [HttpGet]
        public ActionResult AddEmp()
        {
            if (Session["EmpName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AddEmp(Employee employee)
        {
            if (Session["EmpName"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (EmpDbContext empDb = new EmpDbContext())
                        {
                            using (DbContextTransaction transaction = empDb.Database.BeginTransaction())
                            {
                                try
                                {
                                    empDb.Employees.Add(employee);
                                    empDb.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    Console.WriteLine("Error occurred.");
                                }
                            }

                        }
                        ModelState.Clear();
                        return RedirectToAction("Contact");
                    }
                    return View();

                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditEmp(int id)
        {
            if (Session["EmpName"] != null)
            {
                using (EmpDbContext empDb = new EmpDbContext())
                {
                    var result = (from s in empDb.Employees where s.EmployeeId == id select s).FirstOrDefault();

                    if (result != null)
                    {
                        return View(result);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult EditEmp(Employee employee)
        {
            if (Session["EmpName"] != null)
            {
                try
                {
                    using (EmpDbContext empDb = new EmpDbContext())
                    {
                        var data = (from a in empDb.Employees
                                    where a.EmployeeId == employee.EmployeeId
                                    select a).FirstOrDefault();

                        data.Company = employee.Company;
                        data.Country = employee.Country;
                        data.Description = employee.Description;
                        data.IsEmployeeRetired = employee.IsEmployeeRetired;
                        data.Name = employee.Name;
                        data.IsActive = employee.IsActive;

                        empDb.Entry(data).State = EntityState.Modified;

                        empDb.SaveChanges();

                    }
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "EmployeeInfo", "Create"));
                }
                // return RedirectToAction("EditEmp");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult EmpDetails(int id)
        {

            if (Session["EmpName"] != null)
            {
                using (EmpDbContext empDb = new EmpDbContext())
                {
                    var result = (from s in empDb.Employees where s.EmployeeId == id select s).FirstOrDefault();

                    if (result != null)
                    {
                        return View(result);
                    }
                    else
                    {
                        return View();
                    }
                }



            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult EmpDelete(int id)
        {
            if (Session["EmpName"] != null)
            {
                using (EmpDbContext empDb = new EmpDbContext())
                {
                    var result = (from s in empDb.Employees where s.EmployeeId == id select s).FirstOrDefault();
                    empDb.Employees.Remove(result);
                    empDb.SaveChanges();

                }
                return RedirectToAction("Contact");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Email Concept////////
        [NonAction]
        public void RegEmail(string receiver, string subject, string message, string passwords)
        {
            try
            {
                var senderEmail = new MailAddress("abhishekabhi347@gmail.com", "abhi");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = passwords;
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })

                    smtp.Send(senderEmail.Address, receiverEmail.Address, sub, body);

                ViewBag.Message = "MailSent";

            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }

        }

    }
}