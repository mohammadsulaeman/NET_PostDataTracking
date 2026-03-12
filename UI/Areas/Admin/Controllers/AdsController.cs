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
    public class AdsController : Controller
    {
        AdsBLL bll = new AdsBLL();

        public ActionResult AdsList()
        {
            List<AdsDTO> adsList = new List<AdsDTO>();
            adsList = bll.GetAds();
            return View(adsList);
        }
        // Add: Admin/Ads
        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto);
        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {
            if(model.AdsImage == null)
            {
                ViewBag.ProcessState = BLL.General.Message.ImageMissing;
            }else if(ModelState.IsValid)
            {
                HttpPostedFileBase postedFile = model.AdsImage;
                Bitmap SocilaMedia = new Bitmap(postedFile.InputStream);
                string ext = Path.GetExtension(postedFile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".png")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedFile.FileName;
                    SocilaMedia.Save(Server.MapPath("~/Areas/Admin/Content/AdsImages/" + filename));
                    model.ImagePath = filename;
                    bll.AddAds(model);
                    ViewBag.ProcessState = General.Message.AddSuccess;
                    model = new AdsDTO();
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ProcessState = General.Message.ExtensionError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Message.EmptyArea;
            }
            return View(model);
        }

        public ActionResult UpdateAds(int ID)
        {
            AdsDTO dto = bll.GetAdsWithID(ID);

            return View(dto);
        }

    }

    
}