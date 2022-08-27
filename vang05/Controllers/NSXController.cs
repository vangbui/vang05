using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGiay.Models;

namespace ShopGiay.Controllers
{
    public class NSXController : Controller
    {
        dbGiayDataContext data = new dbGiayDataContext();
        // GET: NSX
        public ActionResult Index()
        {
            return View(from nsx in data.NHASANXUATs select nsx);
        }
        public ActionResult Details()
        {
            int mansx = int.Parse(Request.QueryString["id"]);
            var result = data.NHASANXUATs.Where(nsx => nsx.MaNSX == mansx).SingleOrDefault();
            return View(result);
        }
        public NHASANXUAT GetNSX(int id)
        {
            return (data.NHASANXUATs.OrderBy(n => n.TenNXB).Where(n => n.MaNSX == id).SingleOrDefault());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //NHAXUATBAN nxb = data.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
            //return View(nxb);
            return View(GetNSX(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                var nsx = GetNSX(int.Parse(Request.Form["MaNSX"]));
                nsx.TenNXB = Request.Form["TenNXB"];
                nsx.DiaChi = Request.Form["DiaChi"];
                nsx.Email = Request.Form["Email"];
                nsx.DienThoai = Request.Form["DienThoai"];
                //UpdateModel(nxb);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(GetNSX(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection f)
        {
            var sach = GetNSX(id);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //var ctdh = data.CHITIETDATHANGs.Where(ct => ct.MaSach == id).ToList();
            //if (ctdh != null)
            //{
            //    ViewBag.ThongBao = "Sách này đang có trong bảng Chi tiết đặt hàng!";
            //    return RedirectToAction("Delete");
            //}
            data.NHASANXUATs.DeleteOnSubmit(sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                NHASANXUAT nsx = new NHASANXUAT();
                nsx.MaNSX = int.Parse(Request.Form["MaNSX"]);
                nsx.TenNXB = Request.Form["TenNXB"];
                nsx.DiaChi = Request.Form["DiaChi"];
                nsx.Email = Request.Form["Email"];
                nsx.DienThoai = Request.Form["DienThoai"];
                data.SubmitChanges();
                //s.DonGia = Convert.ToDecimal(f["DonGia"]);
                //s.SLTon = Convert.ToDecimal(f["SLTon"]);
                data.NHASANXUATs.InsertOnSubmit(nsx);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}