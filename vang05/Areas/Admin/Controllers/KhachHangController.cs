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
    public class KhachHangController : Controller
    {
        dbGiayDataContext db = new dbGiayDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (fFileUpload == null)
            {
                ViewBag.HoTen = f["sHoTen"];
                ViewBag.TaiKhoan = f["tTaiKhoan"];
                ViewBag.MatKhau = (f["mMatKhau"]);
                ViewBag.Email = (f["eEmail"]);
                ViewBag.DiaChi = f["dDiaChi"];
                ViewBag.DienThoai = (f["sDienThoai"]);
                ViewBag.NgaySinh = DateTime.Parse(f["nNgaySinh"]);
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    kh.HoTen = f["sHoTen"];
                    kh.TaiKhoan = f["tTaiKhoan"];
                    kh.MatKhau = (f["mMatKhau"]);
                    kh.Email = (f["eEmail"]);
                    kh.DiaChi = f["dDiaChi"];
                    kh.DienThoai = (f["sDienThoai"]);
                    kh.NgaySinh = DateTime.Parse(f["nNgaySinh"]);
                    db.KHACHHANGs.InsertOnSubmit(kh);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        public ActionResult Delete(int id)
        {

            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCofirm(int id, FormCollection f)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
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
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var kh = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == int.Parse(f["iMaKH"]));
            if (ModelState.IsValid)
            {
                kh.HoTen = f["sHoTen"];
                kh.TaiKhoan = f["tTaiKhoan"];
                kh.MatKhau = (f["mMatKhau"]);
                kh.Email = (f["eEmail"]);
                kh.DiaChi = f["dDiaChi"];
                kh.DienThoai = (f["sDienThoai"]);
                kh.NgaySinh = DateTime.Parse(f["nNgaySinh"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(kh);
        }

    }
    
}

    
