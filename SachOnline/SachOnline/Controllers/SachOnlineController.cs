using Microsoft.Ajax.Utilities;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext(@"Data Source=DESKTOP-JS1E7FS\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True");
       
        // GET: SachOnline
        public ActionResult Index()
        {
            var listSachMoi = LaySachMoi(6);
            

            return View(listSachMoi);
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd; 
            return PartialView(listChuDe);
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = LaySachMoi(6);
            return PartialView(listSachBanNhieu);
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();
        }
        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult SachNXBPartial()
        {
            var listNXB  = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNXB);
        }



        
        private List<SACH> LaySachMoi(int count )
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult SachTheoChuDe(int iMaCD,int ? page)
        {
            
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = (from s in data.SACHes where s.MaCD==iMaCD select s).OrderBy(s => s.MaSach);
            ViewBag.MaCD = iMaCD;
            return View(sach.ToPagedList(iPageNum,iSize));
        }
        public ActionResult SachTheoNXB(int iMaNXB, int? page)
        {
            ViewBag.MaNXB = iMaNXB;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = (from s in data.SACHes where s.MaNXB == iMaNXB select s).OrderBy(s => s.MaSach); ;
            return View(sach.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }

        public ActionResult LoginLogoutPartial()
        {
            return PartialView();
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }
    }
}