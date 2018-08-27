using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Loginapplication.Models;
using System.Data.Entity;

namespace Loginapplication.Controllers
{
    // [SessionState(SessionStateBehavior.Default)]


    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            using (EmpDbContext dba = new EmpDbContext())
            {
                var ado = dba.Employees.ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(string UserName,string Password)
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
                            if (query != null)
                            {
                                //RedirectToAction("Details/" + id.ToString(), "FullTimeEmployees");    
                                //return View("../Admin/Registration"); url not change in browser  
                                Session["EmpName"] = Convert.ToString(result.UserName);
                                Session["EmpID"] = Convert.ToInt32(result.Empid);
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
            Session.Abandon();
            ViewBag.Message = "Succesfully Logged Out";
            return RedirectToAction("Index");
        }



        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "EmpName,Password,Email,Checkstatus,UserName")] 
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
                            user.Checkstatus = "Y";
                            user.Vcode = keyNew;
                            db.Users.Add(user);
                            db.SaveChanges();
                            ModelState.Clear();
                            ViewBag.Message = "Registration Successful";
                            return RedirectToAction("Index");
                        }
                        ViewBag.Message = "User Alredy Exixts!!!!!!!!!!";
                        return View();

                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Some exception occured" + e;
                    return View();
                }

            }
            else
            {
                ViewBag.Message = "Registration Failed";
                return RedirectToAction("Register");
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

                    if(result != null)
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

                    empDb.Entry(data).State = EntityState.Modified;

                    empDb.SaveChanges();
                }

                return RedirectToAction("Contact");
            }
            catch
            {
                return RedirectToAction("EditEmp");
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



















        //protected override void Dispose(bool disposing)
        //{
        //    this.empDbContext.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}