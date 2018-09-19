using LoginappBLL;
using LoginappBLL.Interface;
using LoginappDomain;
using Loginapplication.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Loginapplication.Controllers
{
    public class UtilityController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //IUsersBusiness _usersBusiness;

        //public UtilityController(IUsersBusiness usersBusiness)
        //{
        //    _usersBusiness = usersBusiness;
        //}


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

                FileUpload fimg = new FileUpload
                {
                    FileName = filename,
                    Imagelength = image,
                    ImagePath = "/Assets/" + filename,
                    Checkstatus = "Y"
                };
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


        public ActionResult Users()
        {
            if (Session["EmpName"] != null)
            {
                ViewBag.UsersName = new UsersBusiness().GetUserName(234);

                List<UsersDomainModel> listDomain = new UsersBusiness().GetAllUsers();


                ViewBag.UserList = listDomain;

                return View();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Roles()
        {
            if (Session["EmpName"] != null)
            {
                var db = new EmpDbContext();
                ViewBag.Roles = db.Roles.Where(x => x.Checkstatus == "Y").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public ActionResult AddEditRole(int RoleID)
        {
            Role model = new Role();
            using (EmpDbContext db = new EmpDbContext())
            {
                if (RoleID > 0)
                {
                    var rolelist = db.Roles.SingleOrDefault(x => x.Roleid == RoleID);
                    model.Roleid = rolelist.Roleid;
                    model.RoleName = rolelist.RoleName;
                }
            }
            return PartialView("_AddEditRole", model);

        }

        [HttpPost]
        public ActionResult AddEditRole(Role role)
        {
            if (Session["EmpName"] != null)
            {
                if (ModelState.IsValid)
                {

                    try
                    {

                        using (EmpDbContext db = new EmpDbContext())
                        {
                            if (role.Roleid > 0)
                            {
                                //update
                                Role rolelist = db.Roles.SingleOrDefault(x => x.Roleid == role.Roleid);
                                rolelist.RoleName = role.RoleName;
                                db.SaveChanges();
                            }
                            else
                            {
                                //Insert

                                role.Checkstatus = "Y";
                                db.Roles.Add(role);
                                db.SaveChanges();
                            }
                        }
                        ModelState.Clear();
                        return RedirectToAction("Roles", "Utility");

                    }
                    catch (Exception ex)
                    {
                        //return View("Error", new HandleErrorInfo(ex, "Utility", "Roles"));
                    }

                }
                return RedirectToAction("AddEditRole", role.Roleid);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteRole(int RoleID)
        {
            Role model = new Role();
            using (EmpDbContext db = new EmpDbContext())
            {
                Role rolelist = db.Roles.SingleOrDefault(x => x.Roleid == RoleID);
                rolelist.Checkstatus = "N";
                db.SaveChanges();
            }
            return RedirectToAction("Roles", "Utility");
        }

        public ActionResult MenuManagement(int RoleID)
        {
            if (Session["EmpName"] != null)
            {
                using (EmpDbContext db = new EmpDbContext())
                {
                    var menurole = db.Roles.Find(RoleID);
                    var menus = menurole.Menu_display;
                    List<MenuManagement> data = new List<MenuManagement>();
                    if (menus != null)
                    {
                        List<int> menuIds = menus.Split(',').Select(int.Parse).ToList();


                        ViewBag.rolemenu = menuIds;

                    }
                    ViewBag.roleid = RoleID;
                    ViewBag.roleName = menurole.RoleName;
                    ViewBag.menulist = db.MenuManagement.Where(x => x.Checkstatus == "Y").Select(p => p.Menu_NAME).ToList();
                    data = db.MenuManagement.ToList();

                    //foreach (var item in menuIds)
                    //{
                    //    data.Add(db.MenuManagement.Find(item));


                    //}
                    return View(data);

                }
                //return RedirectToAction("Roles");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult MenuManagement(FormCollection collection)
        {
            if (Session["EmpName"] != null)
            {

                using (EmpDbContext db = new EmpDbContext())
                {

                    var menulist = db.MenuManagement.Where(x => x.Checkstatus == "Y").Select(p => p.Menu_NAME).ToList();
                    List<int> applist = new List<int>();
                    StringBuilder sb = new StringBuilder();
                    // sb.Append("0");
                    foreach (var item in menulist)
                    {

                        if (!string.IsNullOrEmpty(collection[item]))
                        {
                            var checkResp = collection[item].Split(',')[0];

                            if (checkResp != "false")
                            {
                                //sb.Append(", " + int.Parse(checkResp));
                                sb.Append(int.Parse(checkResp) + " ,");
                            }
                        }
                    }
                    var rid = int.Parse(collection["hdnroleid"]);
                    var menures = sb.ToString();
                    var result = (from s in db.Roles where s.Roleid == rid select s).FirstOrDefault();
                    result.Menu_display = menures.Remove(menures.Length - 1);
                    db.SaveChanges();

                }

                return RedirectToAction("Roles");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult SiteSetting()
        {
            if (Session["EmpName"] != null)
            {
                using (EmpDbContext db = new EmpDbContext())
                {
                    ViewBag.SettingsList = db.siteSettings.Where(x => x.Checkstatus == "Y").ToList();
                    ViewBag.ListCount = (from row in db.siteSettings
                                         where row.Checkstatus == "Y"
                                         select row).Count();

                    return View();
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult AddEditSettings(int SiteID)
        {
            if (Session["EmpName"] != null)
            {
                using (EmpDbContext db = new EmpDbContext())
                {
                    var data = db.siteSettings.Find(SiteID);
                    return View(data);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddEditSettings(SiteSettings model)
        {

            try
            {
                using (EmpDbContext db = new EmpDbContext())
                {
                    //update
                    if (model.SettingsID > 0)
                    {
                        if (ModelState.IsValid)
                        {
                            SiteSettings settingslist = db.siteSettings.SingleOrDefault(x => x.SettingsID == model.SettingsID);
                            settingslist.SettingName = model.SettingName;
                            settingslist.ApplicationTitle = model.ApplicationTitle;
                            settingslist.ApplicationTitleColour = model.ApplicationTitleColour;
                            settingslist.ApplicationTitleFont = model.ApplicationTitleFont;
                            settingslist.ApplicationTitleSize = model.ApplicationTitleSize;
                            settingslist.MenuColour = model.MenuColour;
                            settingslist.MenuTextColour = model.MenuTextColour;
                            settingslist.NavColour = model.NavColour;
                            settingslist.NavTextColour = model.NavTextColour;

                            db.SaveChanges();
                            ModelState.Clear();
                        }
                        else
                        {
                            return View();
                        }
                    }
                    //Insert
                    else
                    {
                        model.Checkstatus = "Y";
                        db.siteSettings.Add(model);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("SiteSetting");
            }
            catch (Exception e)
            {
                logger.ErrorException("Error occured in Home controller Index Action", e);
                return RedirectToAction("SiteSetting");
            }
        }

        public ActionResult DeleteSiteSettings(int SiteID)
        {
           // SiteSettings model = new SiteSettings();
            using (EmpDbContext db = new EmpDbContext())
            {
                SiteSettings sitelist = db.siteSettings.SingleOrDefault(x => x.SettingsID == SiteID);
                sitelist.Checkstatus = "N";
                db.SaveChanges();
            }
            return RedirectToAction("SiteSetting", "Utility");
        }

        public void ActivateSiteSetting(int SiteID)
        {
            using (EmpDbContext db = new EmpDbContext())
            {
                SiteSettings Inactivate = db.siteSettings.Where(x => x.IsActive == true).FirstOrDefault();
                if(Inactivate != null) Inactivate.IsActive = false;

                SiteSettings sitelist = db.siteSettings.SingleOrDefault(x => x.SettingsID == SiteID);
                sitelist.IsActive = true;
                db.SaveChanges();
            }
        }

    }
}
