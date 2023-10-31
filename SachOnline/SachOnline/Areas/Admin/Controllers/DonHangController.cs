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

    public class DonHangController : KiemTraController
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext(@"Data Source=DESKTOP-JS1E7FS\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 10;
            return View(db.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONDATHANG ddh, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            if (ModelState.IsValid)
            {


                ddh.DaThanhToan =Boolean.Parse(f["sDaThanhToan"]);
                ddh.TinhTrangGiaoHang = int.Parse(f["sTinhTrangGiaoHang"]);
                ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);
                db.DONDATHANGs.InsertOnSubmit(ddh);
                db.SubmitChanges();
                //Vê lai trang Quán ly sách
                return RedirectToAction("Index");
            }
            return View();



        }
        public ActionResult Details(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(ddh);
        }
        public ActionResult Delete(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            return View(ddh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            db.DONDATHANGs.DeleteOnSubmit(ddh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (ddh == null)
            {
                Response.StatusCode = 422;
                return null;

            }
            //Hiēn thi danh sách chú dê và nhà xuat bán dông thoi chon chú dê và nhà xuat bán cua cuon hiên tai
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen", ddh.MaKH);
            return View(ddh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //Dua dữ liệu vào DropDown
            var ddh = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == int.Parse(f["iMaDonHang"]));
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            if (ModelState.IsValid)
            {

                ddh.DaThanhToan = Boolean.Parse(f["sDaThanhToan"]);
                ddh.TinhTrangGiaoHang = int.Parse(f["sTinhTrangGiaoHang"]);
                ddh.NgayDat = Convert.ToDateTime(f["dNgayDat"]);
                ddh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                ddh.MaKH = int.Parse(f["MaKH"]);
               
                db.SubmitChanges();
                //Vê lai trang Quán ly sách
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}