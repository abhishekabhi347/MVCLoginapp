using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Loginapplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
            return View();
        }




        [HttpGet]
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

        [HttpPost]
        public ActionResult AddEditTechnology(Technologies technologies)
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
                                //Technologies techins = new Technologies();
                                //techins.Technology = technologies.Technology;
                                //techins.Description = technologies.Description;
                                technologies.Checkstatus = "Y";
                                Techdb.Technologies.Add(technologies);
                                Techdb.SaveChanges();
                            }

                        }
                        ModelState.Clear();
                        return RedirectToAction("Index",technologies);

                    }



                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Technology", "Index"));
                    }

                }

                return RedirectToAction("AddEditTechnology", technologies.TechId);

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteTech(int TechId)
        {
            Technologies model = new Technologies();
            using (EmpDbContext Techdb = new EmpDbContext())
            {
                Technologies techlist = Techdb.Technologies.SingleOrDefault(x => x.TechId == TechId);
                techlist.Checkstatus = "N";
                Techdb.SaveChanges();
            }
                return RedirectToAction("Index", "Technology");
        }




        public FileStreamResult pdf(int Id)
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            document.Open();

            Font font_heading_1 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 19, Font.BOLD);
            Font font_body = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9);

            // Create the heading paragraph with the headig font
            Paragraph paragraph;
            paragraph = new Paragraph("Hello world!", font_heading_1);

            // Add a horizontal line below the headig text and add it to the paragraph
            iTextSharp.text.pdf.draw.VerticalPositionMark seperator = new iTextSharp.text.pdf.draw.LineSeparator();
            seperator.Offset = -6f;
            paragraph.Add(seperator);

            //document.Add(new Header("Technology","Technology Report"));
            //document.Add(new Paragraph("Hello World"));
            //document.Add(new Paragraph(DateTime.Now.ToString()));

            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }




    }
}
