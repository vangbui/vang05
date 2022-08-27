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
    public class NhanVienController : Controller
    {
        dbGiayDataContext db = new dbGiayDataContext();
        // GET: Admin/NhanVien
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.NHANVIENs.ToList().OrderBy(n => n.MaNV).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NHANVIEN nv, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (fFileUpload == null)
            {
                ViewBag.TenNV = f["sTenNV"];
                ViewBag.DiaChi = f["dDiaChi"];
                ViewBag.DienThoai =(f["sDienThoai"]);
                ViewBag.GioiTinh = (f["gGioiTinh"]);
                ViewBag.MatKhau = (f["mMatKhau"]);
                ViewBag.NgaySinh = (f["nNgaySinh"]);
                ViewBag.Email = (f["eEmail"]);
                ViewBag.Quyen = int.Parse(f["qQuyen"]);

                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    nv.TenNV = f["sTenNV"];
                    nv.DiaChi = f["dDiaChi"];
                    nv.DienThoai = (f["sDienThoai"]);
                    nv.GioiTinh = (f["gGioiTinh"]);
                    nv.MatKhau = (f["mMatKhau"]);
                    nv.NgaySinh = (f["nNgaySinh"]);
                    nv.Email = (f["eEmail"]);
                    nv.Quyen = int.Parse(f["qQuyen"]);
                    db.NHANVIENs.InsertOnSubmit(nv);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }


        }
        public ActionResult Delete(int id)
        {

            var nv = db.NHANVIENs.SingleOrDefault(n => n.MaNV == id);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nv);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCofirm(int id, FormCollection f)
        {
            var nv = db.NHANVIENs.SingleOrDefault(n => n.MaNV == id);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NHANVIENs.DeleteOnSubmit(nv);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var nv = db.NHANVIENs.SingleOrDefault(n => n.MaNV == id);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nv);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var nv = db.NHANVIENs.SingleOrDefault(n => n.MaNV == int.Parse(f["iMaNV"]));
            if (ModelState.IsValid)
            {
                nv.TenNV = f["sTenNV"];
                nv.DiaChi = f["dDiaChi"];
                nv.DienThoai = (f["sDienThoai"]);
                nv.GioiTinh = (f["gGioiTinh"]);
                nv.MatKhau = (f["mMatKhau"]);
                nv.NgaySinh = (f["nNgaySinh"]);
                nv.Email = (f["eEmail"]);
                nv.Quyen = int.Parse(f["qQuyen"]);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(nv);
        }
    }
}