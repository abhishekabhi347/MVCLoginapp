using Loginapplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loginapplication.Controllers
{
    public class UtilityController : Controller
    {
        // GET: Utility
        [NonAction]
        public ActionResult Index()
        {
            if (Session["EmpName"] != null)
            {

                using (EmpDbContext db = new EmpDbContext())
                {
                    ViewBag.tech = db.FileUploads.Where(x => x.Checkstatus == "Y").ToList();
                    //var x = db.FileUploads.ToList();
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public ActionResult Create()
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

        

        public ActionResult Create(int id)
        {

            if (Session["EmpName"] != null)
            {
                using (EmpDbContext db = new EmpDbContext())
                {
                    var x = db.FileUploads.Where(a => a.Checkstatus == "Y");
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public JsonResult Create(FileUpload img, HttpPostedFileBase ImageFile)
        {

            EmpDbContext db = new EmpDbContext();
            int imageId = 0;

            var file = ImageFile;

            //byte[] imagebyte = null;

            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Server.MapPath("/Assets/" + filename));

                //BinaryReader reader = new BinaryReader(file.InputStream);
                Stream stream = file.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);


                Byte[] image = binaryReader.ReadBytes((int)stream.Length);
                //imagebyte = reader.ReadBytes(file.ContentLength);

                FileUpload fimg = new FileUpload();

                fimg.FileName = filename;
                fimg.Imagelength = image;
                fimg.ImagePath = "/Assets/" + filename;
                fimg.Checkstatus = "Y";
                db.FileUploads.Add(fimg);
                db.SaveChanges();

                imageId = fimg.ImageId;

            }

            return Json(imageId, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ImageRetrieve(int imgID)
        {
            using (EmpDbContext db = new EmpDbContext())
            {

                var img = db.FileUploads.SingleOrDefault(x => x.ImageId == imgID);

                return File(img.Imagelength, "image/jpg");
            }

        }











        //[HttpPost]
        //public ActionResult Create(FileUpload img, HttpPostedFileBase file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (file != null)
        //        {
        //            string filename = Path.GetFileName(file.FileName);
        //            string path = HttpContext.Server.MapPath("/Assets/") + filename;

        //             file.SaveAs(path);

        //            img.ImagePath = file.FileName;

        //            Stream stream = file.InputStream;
        //            BinaryReader binaryReader = new BinaryReader(stream);
        //            Byte[] image = binaryReader.ReadBytes((int)stream.Length);

        //            img.Imagelength = image;
        //        }
        //        using (EmpDbContext db = new EmpDbContext())
        //        {
        //            db.FileUploads.Add(img);
        //            db.SaveChanges();
        //        }
        //        return View();
        //    }
        //    return View();
        //}

    }
}