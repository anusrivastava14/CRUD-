using CRUD____.DAL;
using CRUD____.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD____.Controllers
{
    public class HomeController : Controller
    {
        DataAccess DB = new DataAccess();
        private int procid;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult ADD()
        {
            MODEL model = new MODEL();

            if (Request.QueryString["sid"]!=null)
            {
                model.ID = Convert.ToInt32(Request.QueryString["sid"].ToString());
                model = DB.Reach(model, 3).ToList().FirstOrDefault();
                ViewBag.ButtonName = "Update";
            }
            else {
           
                ViewBag.ButtonName = "submit";
            }

            
var list1 = DB.Reach(model,4).ToList();
            if(list1.Count>0)
            {

                ViewBag.list = list1;
            }
            else
            {
                ViewBag.list = null;
            }

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ADD(MODEL model,string Command,HttpPostedFileBase ImgInp)
        {


            if (string.IsNullOrEmpty(model.Fname))
            {
                ModelState.AddModelError("Fname", "Enter aa valid name");
            }
            if (ModelState.IsValid)
            {
                if (ImgInp != null)
                {
                    string path = "~/Content/Upload";
                    string b = "";
                    var upload = Server.MapPath(path);
                    string extension = System.IO.Path.GetExtension(Request.Files["ImgInp"].FileName);
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        if (ImgInp.ContentLength > 0)
                        {
                            ImgInp = Request.Files["ImgInp"];
                            string name = DateTime.Now.Ticks + "_P" + extension.ToLower().ToString();
                            string photopath = path + "/" + name;
                            b = upload + "/" + name;
                            model.ImagePath = photopath;
                            ImgInp.SaveAs(b);


                        }
                        else
                        {
                            TempData["msg"] = "please upload a valid pic";
                        }

                    }
                    else

                    {
                        TempData["msg"] = "please upload a valid pic";
                    }
                }
                if (model.Qf == "1")
                {
                    model.Qf = "High School";
                }
                if (model.Qf == "2")
                {
                    model.Qf = "Intermediate";
                }
                if (model.Qf == "3")
                {
                    model.Qf = "Diploma";
                }
                if (model.Qf == "4")
                {
                    model.Qf = "B.tech";
                }
                if (model.Qf == "5")
                {
                    model.Qf = "M.tech";
                }
                if (Command == "submit")
                {
                    procid = 1;
                }
                if (Command == "Update")
                {

                    procid = 2;
                }
                var list = DB.Reach(model, procid).ToList();
                if (list.Count > 0)
                {
                    ViewBag.list = list;
                }
                else
                {
                    ViewBag.list = null;
                }
            }
            return RedirectToAction("ADD","Home");
        }

        public JsonResult city(int procid)
        {
            var data = DB.Reach1(procid).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult State(int id,int procid)
        {

            var data = DB.Reach2(id, procid).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult edit(int id)
        {

            MODEL model = new MODEL();
            return RedirectToAction("ADD",new{ sid=id});
        }
        public JsonResult deletee(int sid)
        {
            MODEL model = new MODEL();
            model.ID = sid;
            var data=DB.Reach(model, 5).ToList();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult loginid()
        {

            ViewBag.LOGIN = "Login";
            return View();
        }
        public ActionResult loginid(MODEL model, string Command)
        {

            if (Command == "Login")
            {
                procid = 6;
            }
            var list = DB.Reach(model, 6).ToList();
            if(list.Count>0)
            {
                Session["Name"] = list[0].Name;
                Session["MOB"] = list[0].MOB;
                return RedirectToAction("session", "Home");
            }
            else
            {
                return RedirectToAction("ADD","Home");
            }
            return RedirectToAction("ADD","Home");
        }
        public ActionResult session()
        {
            if (Session["Name"]== null)
            {
                return RedirectToAction("ADD","Home");
            }
            var a = Session["Name"].ToString();
            ViewBag.a = a;
          return View();
        }

        [HttpGet]
        public ActionResult rdt1()
        {
            MODEL model = new MODEL();
            ViewBag.buttonname = "date";
            var list = DB.Reach(model, 4).ToList();
            if (list.Count > 0)
            {
                ViewBag.RDT = list;
                //Session["name"] = list[0].Fname;
                 
            }
            return View();
        }

        [HttpPost]
        public ActionResult rdt1(MODEL model, string Command)
        {
           if(Command== "date")
            {
                procid = 7;
            }
            var list = DB.Reach(model, 7).ToList();
            if(list.Count>0)
            {
                ViewBag.RDT = list;
            }
            else
            {

                ViewBag.RDT = null;
            }
            return View();
        }
        public ActionResult report()
        {
            MODEL model = new MODEL();
            //ViewBag.session = Session["name"].ToString();
            var list = DB.Reach(model, 4).ToList();
            if(list.Count>0)
            {
                ViewBag.list = list;
            }
            else
            {
                ViewBag.list = null; 
            }
            return View();
        }
    }
}