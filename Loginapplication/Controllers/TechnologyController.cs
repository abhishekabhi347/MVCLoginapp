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
                ViewBag.tech = tech.Technologies.ToList();
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
                
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (EmpDbContext Techdb = new EmpDbContext())
                        {
                            Techdb.Technologies.Add(technologies);
                            Techdb.SaveChanges();
                            ViewBag.Tech = Techdb.Technologies.ToList();
                        }
                        ModelState.Clear();
                        //var tech = new EmpDbContext();
                        //ViewBag.Tech = tech.Technologies.ToList();
                        //return RedirectToAction("Index","Technology");
                        return PartialView("_StudentData");
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Technology", "Index"));
                }
               
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        // GET: Technology/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Technology/Create
        public ActionResult Create()
        {
            if (Session["EmpName"] != null)
            {

                return PartialView("_Create");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Technology/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Technology/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["EmpName"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Technology/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Technology/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Technology/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
