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
    public class AGiayController : Controller
    {
        dbGiayDataContext db = new dbGiayDataContext();
        // GET: Admin/AGiay
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.GIAYs.ToList().OrderBy(g => g.MaGiay).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaNH = new SelectList(db.NHANHIEUs.ToList().OrderBy(n => n.TenNH), "MaNH", "TenNH");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNXB), "MaNSX", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GIAY giay, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaNH = new SelectList(db.NHANHIEUs.ToList().OrderBy(n => n.TenNH), "MaNH", "TenNH");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNXB), "MaNSX", "TenNXB");

            if (fFileUpload == null)
            {
                ViewBag.ThongBao = "Hãy chọn ảnh";
                ViewBag.TenGiay = f["gTenGiay"];
                ViewBag.MoTa = f["gMoTa"];
                ViewBag.SoLuong = int.Parse(f["sSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaNH = new SelectList(db.NHANHIEUs.ToList().OrderBy(n => n.TenNH), "MaNH", "TenNH",
                    int.Parse(f["MaNH"]));
                ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNXB), "MaNSX",
                    "TenNSX", int.Parse(f["MaNSX"]));
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
                    giay.TenGiay = f["gTenGiay"];
                    giay.MoTa = f["sMoTa"].Replace("<p>", " ").Replace("<p>", "\n");
                    giay.AnhGiay = sFileName;
                    giay.SoLuongBan = int.Parse(f["iSoLuong"]);
                    giay.GiaBan = decimal.Parse(f["mGiaBan"]);
                    giay.MaNH = int.Parse(f["MaNH"]);
                    giay.MaNSX = int.Parse(f["MaNSX"]);
                    db.GIAYs.InsertOnSubmit(giay);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

        }
        public ActionResult Details(int id)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        public ActionResult Delete(int id)
        {

            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteCofirm(int id, FormCollection f)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = db.CHITIETDATHANGs.Where(ct => ct.MaGiay == id);
            if (ctdh.Count() > 0)
            {
                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết đặt hàng <br>" +
                       " Nếu muốn xóa thì phải xóa hết sản phẩm này trong bảng chi tiết đặt hàng";
                return View(giay);
            }
            db.GIAYs.DeleteOnSubmit(giay);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.NHANHIEUs.ToList().OrderBy(n => n.TenNH), "MaNH",
                "TenNH", giay.MaNH);
            ViewBag.MaNXB = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNXB),
                "MaNSX", "TenNXB", giay.MaNH);
            return View(giay);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == int.Parse(f["iMaGiay"]));
            ViewBag.MaNH = new SelectList(db.NHANHIEUs.ToList().OrderBy(n => n.TenNH), "MaNH",
               "TenNH", giay.MaNH);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNXB),
                "MaNSX", "TenNXB", giay.MaNH);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    giay.AnhGiay = sFileName;
                }
                giay.TenGiay = f["sTenGiay"];
                giay.MoTa = f["sMoTa"].Replace("<p>", " ").Replace("<p>", "\n");
                giay.SoLuongBan = int.Parse(f["iSoLuong"]);
                giay.GiaBan = decimal.Parse(f["mGiaBan"]);
                giay.MaNH = int.Parse(f["MaNH"]);
                giay.MaNSX = int.Parse(f["MaNSX"]);
                db.SubmitChanges();
                return RedirectToAction("Index");


            }
            return View(giay);
        }
    }
}