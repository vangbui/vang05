using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;
using PagedList.Mvc;
using PagedList;

namespace ShopGiay.Controllers
{
    public class GiayController : Controller
    {
        dbGiayDataContext data = new dbGiayDataContext();
        // GET: Giay
        private List<GIAY> LayGiayNoiBat(int count)
        {
            return data.GIAYs.OrderByDescending(a => a.GiaBan).Take(count).ToList();
        }

        public ActionResult Index()
        {
            var listGiay = LayGiayNoiBat(9);
            return View(listGiay);
        }
        public ActionResult ChiTietGiay(int id)
        {
            var giay= from g in data.GIAYs where g.MaGiay== id select g;
            return View(giay.Single());
        }
        private List<GIAY> LayGiay(int count)
        {
            return data.GIAYs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();
        }
        public ActionResult NavbarPartial()
        {
            var listNhanHieu = from nh in data.NHANHIEUs select nh;
            return PartialView(listNhanHieu);
        }
        public ActionResult NhanHieuPatial()
        {
            var listNhanHieu = from nh in data.NHANHIEUs select nh;
            return PartialView(listNhanHieu);
        }
        public ActionResult NhaSanXuatPatial()
        {
            var listNSX = from nsx in data.NHASANXUATs select nsx;
            return PartialView(listNSX);
        }
        public ActionResult TheoNhanHieu(int iMaNH, int? page)
        {
           
            ViewBag.MaNH = iMaNH;
            int iSize = 3;
            int iPageNum = (page ?? 1);
           
            var giay = from g in data.GIAYs where g.MaNH == iMaNH select g;
            return View(giay.ToPagedList(iPageNum, iSize));
        }
        public ActionResult TheoNSX(int iMaNSX, int? page)
        {
            ViewBag.MaNXB = iMaNSX;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var giay = from g in data.GIAYs where g.MaNSX == iMaNSX select g;
            return View(giay.ToPagedList(iPageNum, iSize));
        }
    }
}