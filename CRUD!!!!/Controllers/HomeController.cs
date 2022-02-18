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
        public ActionResult ADD(MODEL model,string Command,HttpPostedFileBase ImgInp)
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
                if(model.Qf == "2")
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
    }
}