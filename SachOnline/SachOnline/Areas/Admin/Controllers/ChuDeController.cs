using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using SachOnline.App_Start;

namespace SachOnline.Areas.Admin.Controllers
{

    public class ChuDeController : KiemTraController

    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext(@"Data Source=DESKTOP-JS1E7FS\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {

            int iPageNum = (page ?? 1);
            int iPageSize = 7;

            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE chude, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            
                if (ModelState.IsValid)
                {
                    chude.TenChuDe = f["sTenChuDe"];
                    db.CHUDEs.InsertOnSubmit(chude);
                    db.SubmitChanges();
                    //Vê lai trang Quán ly sách
                    return RedirectToAction("Index");
                }
                return View();

            
        }
        public ActionResult Delete(int id)
        {
            var sach = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (sach == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(sach);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sach = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (sach == null)
            {
                Response.StatusCode = 422;
                return null;
            }
            
            //Xóa hêt thông tin cua cuon sách trong table VietSach truoc khi xóa sách nay
            
            db.CHUDEs.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 422;
                return null;
            }
            return View(chude);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit (FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == int.Parse(f["iMaCD"]));
            
                if (ModelState.IsValid)
                {
                    chude.TenChuDe = f["sTenChuDe"];
                    
                    db.SubmitChanges();
                    //Vê lai trang Quán ly sách
                    return RedirectToAction("Index");
                }
                return View();

            
        }
    }
}