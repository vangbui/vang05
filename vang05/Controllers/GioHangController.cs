using ShopGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopGiay.Controllers
{
    public class GioHangController : Controller
    {
        dbGiayDataContext db = new dbGiayDataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //khoi tao gio hang rong
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int mg, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.iMaGiay == mg);
            if (sp == null)
            {
                sp = new GioHang(mg);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Giay");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        
        public ActionResult XoaSPKhoiGioHang(int iMaGiay)
        {
            List<GioHang> lstGioHang = LayGioHang();
            //Kiem tra Sach da co trong Session["GioHang"]
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaGiay);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaGiay == iMaGiay);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Giay");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int iMaGiay, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaGiay == iMaGiay);
            //neu ton tai thi cho sua so luong
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
       
    
    //Xoa gio hang
    public ActionResult XoaGioHang()
    {
        List<GioHang> lstGioHang = LayGioHang();
        lstGioHang.Clear();
        return RedirectToAction("Index", "Giay");

    }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Giay");
            }
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection f)
        {


            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];

            List<GioHang> listGioHang = LayGioHang();
            var lastDH = db.DONDATHANGs.OrderByDescending(x=>x.MaDH).FirstOrDefault();
            if(lastDH is null)
            {
                ddh.MaDH = 1;
            }
            else
            {
                ddh.MaDH = lastDH.MaDH + 1;
            }
            ddh.MaKH = kh.MaKH;
            ddh.Email = kh.Email;
            ddh.DiaChi = kh.DiaChi;
            ddh.NgayDat = DateTime.Now;
            if (string.IsNullOrEmpty(f["NgayGiao"]))
            {
                return RedirectToAction("DatHang", "GioHang");
            }
            var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            ddh.NgayGiao = DateTime.Parse(NgayGiao);
            ddh.TinhTrangGiaoHang = "3";
             
            //ddh.DaThanhToan = false
            db.DONDATHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //them chi tiet don hang
            foreach (var item in listGioHang)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaGiay = item.iMaGiay;
                ctdh.Ten = item.sTenGiay;
                ctdh.Size = item.zSize;
                ctdh.HinhAnh = db.GIAYs.Where(x => x.MaGiay == item.iMaGiay).FirstOrDefault().AnhGiay;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dGiaBan;
                db.CHITIETDATHANGs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}