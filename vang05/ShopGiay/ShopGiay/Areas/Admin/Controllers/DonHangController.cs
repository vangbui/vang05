using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;
using PagedList.Mvc;
using PagedList;
using System.IO; 

namespace ShopGiay.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        dbGiayDataContext db = new dbGiayDataContext();
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {

            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DONDATHANGs.ToList().OrderBy(g => g.MaDH).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
           
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DONDATHANG dh, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");


            if (fFileUpload == null)
            {
                ViewBag.NgayDat = (f["nNgayDat"]);
                ViewBag.NgayGiao = (f["nNgayDao"]);
                ViewBag.SoDT = (f["sSoDT"]);
                ViewBag.Email = (f["eEmail"]);
                ViewBag.DiaChi = (f["dDiaChi"]);
                ViewBag.TinhTrangGiaoHang = (f["tTinhTrangGiaoHang"]);
                ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen",
                    int.Parse(f["MaKH"]));
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {

                    dh.NgayDat = DateTime.Parse(f["nNgayDat"]);
                    dh.NgayGiao = DateTime.Parse(f["nNgayDao"]);
                    dh.SoDT = int.Parse(f["sSoDT"]);
                    dh.Email = (f["eEmail"]);
                    dh.DiaChi = (f["dDiaChi"]);
                    dh.TinhTrangGiaoHang = (f["tTinhTrangGiaoHang"]);
                    dh.MaKH = int.Parse(f["MaKH"]);
                    db.DONDATHANGs.InsertOnSubmit(dh);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            var sach = db.GIAYs.SingleOrDefault(n => n.MaGiay == 10);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public ActionResult Delete(int id)
        {

            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDH == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCofirm(int id, FormCollection f)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDH == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DONDATHANGs.DeleteOnSubmit(dh);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDH == id);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH",
                "HoTen", dh.MaKH);
            return View(dh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var dh = db.DONDATHANGs.SingleOrDefault(n => n.MaDH == int.Parse(f["iMaDH"]));
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HoTen), "MaKH",
                 "HoTen", dh.MaKH);

            if (ModelState.IsValid)
            {
                dh.NgayDat = DateTime.Parse(f["nNgayDat"]);
                dh.NgayGiao = DateTime.Parse(f["nNgayDao"]);
                dh.SoDT = int.Parse(f["sSoDT"]);
                dh.Email = (f["eEmail"]);
                dh.DiaChi = (f["dDiaChi"]);
                dh.TinhTrangGiaoHang = (f["tTinhTrangGiaoHang"]);
                dh.MaKH = int.Parse(f["MaKH"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(dh);
        }
    }
}