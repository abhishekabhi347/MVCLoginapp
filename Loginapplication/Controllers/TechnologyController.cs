using Loginapplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loginapplication.Controllers
{
    public class TechnologyController : Controller
    {
        // GET: Technology
        public ActionResult Index()
        {
            if (Session["EmpName"] != null)
            {

                var tech = new EmpDbContext();
                ViewBag.tech = tech.Technologies.Where(x => x.Checkstatus == "Y").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        


        [HttpPost]
        public ActionResult Index(Technologies technologies)
        {
            if (Session["EmpName"] != null)
            {
                if (ModelState.IsValid)
                {

                    try
                    {

                        using (EmpDbContext Techdb = new EmpDbContext())
                        {
                            if (technologies.TechId > 0)
                            {
                                //update
                                Technologies techlist = Techdb.Technologies.SingleOrDefault(x => x.TechId == technologies.TechId);
                                techlist.Technology = technologies.Technology;
                                techlist.Description = technologies.Description;
                                Techdb.SaveChanges();
                            }
                            else
                            {
                                //Insert
                                Technologies techins = new Technologies();
                                techins.Technology = technologies.Technology;
                                techins.Description = technologies.Description;
                                Techdb.Technologies.Add(techins);
                                Techdb.SaveChanges();
                            }

                        }

                        return View(technologies);

                    }



                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Technology", "Index"));
                    }
                }
                return RedirectToAction("AddEditTechnology",technologies.TechId);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }





        public ActionResult AddEditTechnology(int TechID)
        {
            Technologies model = new Technologies();
            using (EmpDbContext Techdb = new EmpDbContext())
            {
                if (TechID > 0)
                {
                    Technologies techlist = Techdb.Technologies.SingleOrDefault(x => x.TechId == TechID);
                    model.TechId = techlist.TechId;
                    model.Technology = techlist.Technology;
                    model.Description = techlist.Description;
                }
            }
            return PartialView("_Create", model);
        }

        public ActionResult DeleteTech(int TechId)
        {
            Technologies model = new Technologies();
            using (EmpDbContext Techdb = new EmpDbContext())
            {
                Technologies techlist = Techdb.Technologies.SingleOrDefault(x => x.TechId == TechId);
                model.Checkstatus = "N";

            }
                return RedirectToAction("Index", "Technology");
        }


    }
}
