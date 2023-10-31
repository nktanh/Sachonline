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
using System.Web.Helpers;
using SachOnline.App_Start;

namespace SachOnline.Areas.Admin.Controllers
{
    public class KhachHangController : KiemTraController
    {
        // GET: Admin/KhachHang
        dbSachOnlineDataContext db = new dbSachOnlineDataContext(@"Data Source=DESKTOP-JS1E7FS\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;

            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            
            //Dua dữ liệu vào DropDown
            if (ModelState.IsValid)
                {
                kh.HoTen = f["sHoTen"];
                    kh.TaiKhoan = f["sTaiKhoan"];
                    kh.MatKhau = f["sMatKhau"];
                    kh.Email = f["sEmail"];
                    kh.DiaChi = f["sDiaChi"];
                    kh.DienThoai = f["iDienThoai"];
                    kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                if (db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == kh.TaiKhoan) != null)
                {
                    ViewBag.ThongBao1 = "Tên đăng nhập đã tồn tại";
                }
                else if (db.KHACHHANGs.SingleOrDefault(n => n.Email == kh.Email) != null)
                {
                    ViewBag.ThongBao2 = "Email đã được sử dụng";
                }
                else
                {
                    db.KHACHHANGs.InsertOnSubmit(kh);

                    db.SubmitChanges();
                    //Vê lai trang Quán ly sách
                    return RedirectToAction("Index");
                }    
                
            }
                return Create();

        }
        public ActionResult Details(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(kh);
        }
        public ActionResult Delete(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            //Xóa hêt thông tin cua cuon sách trong table VietSach truoc khi xóa sách nay
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            //Hiēn thi danh sách chú dê và nhà xuat bán dông thoi chon chú dê và nhà xuat bán cua cuon hiên tai
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //Dua dữ liệu vào DropDown
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));


            if (ModelState.IsValid)
            {
                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["sTaiKhoan"];
                kh.MatKhau = f["sMatKhau"];
                kh.Email = f["sEmail"];
                kh.DiaChi = f["sDiaChi"];
                kh.DienThoai = f["iDienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["dNgaySinh"]);
                db.SubmitChanges();
                //Vê lai trang Quán ly sách
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}