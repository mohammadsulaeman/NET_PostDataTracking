using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using BLL;
using System.Drawing;
using System.IO;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserBLL bll = new UserBLL();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            UserDTO dto = new UserDTO();

            return View(dto);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.UserImage == null)
            {
                ViewBag.ProcessState = General.Message.ImageMissing;
            } else if (ModelState.IsValid)
            {
                string filename = "";
                HttpPostedFileBase postedFile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedFile.InputStream);
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedFile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedFile.FileName;
                    resizeImage.Save(Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename));
                    model.ImagePath = filename;
                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    ModelState.Clear();
                    model = new UserDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }
            }else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }

            return View(model);
        }
        
    }
}