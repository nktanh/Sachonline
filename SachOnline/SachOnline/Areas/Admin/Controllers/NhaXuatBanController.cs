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
    [AdminAuthorize]
    public class NhaXuatBanController : KiemTraController
    {
        // GET: Admin/NhaXuatBan
        dbSachOnlineDataContext db = new dbSachOnlineDataContext(@"Data Source=DESKTOP-JS1E7FS\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {

            int iPageNum = (page ?? 1);
            int iPageSize = 7;

            return View(db.NHAXUATBANs.ToList().OrderBy(n => n.MaNXB).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHAXUATBAN chude, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                chude.TenNXB = f["sTenChuDe"];
                chude.DiaChi = f["sDiaChi"];
                chude.DienThoai = f["sDienThoai"];
                db.NHAXUATBANs.InsertOnSubmit(chude);
                db.SubmitChanges();
                //Vê lai trang Quán ly sách
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var sach = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
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
            var sach = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (sach == null)
            {
                Response.StatusCode = 422;
                return null;

            }

            //Xóa hêt thông tin cua cuon sách trong table VietSach truoc khi xóa sách nay

            db.NHAXUATBANs.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var sach = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (sach == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sach = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var chude = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == int.Parse(f["iMaNXB"]));
            
                if (ModelState.IsValid)
                {
                    chude.TenNXB = f["sTenChuDe"];
                    chude.DiaChi = f["sDiaChi"];
                    chude.DienThoai = f["sDienThoai"];
                   
                    db.SubmitChanges();
                    //Vê lai trang Quán ly sách
                    return RedirectToAction("Index");
                }
                return View(chude);

           
        }
    }
}