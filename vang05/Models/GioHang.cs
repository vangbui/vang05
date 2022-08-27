using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopGiay.Models
{
    public class GioHang
    {
        dbGiayDataContext data = new dbGiayDataContext();
        public int iMaGiay { get; set; }
        public string sTenGiay { get; set; }
        public string zSize { get; set; }
        public string sAnhGiay { get; set; }
        public double dGiaBan { get; set; }
        public int iSoLuong { get; set; }
        public int  lMaGG{ get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dGiaBan; }
        }
        public GioHang(int ms)
        {
            iMaGiay = ms;
            GIAY g = data.GIAYs.Single(n => n.MaGiay == iMaGiay);
            sTenGiay = g.TenGiay;
            sAnhGiay = g.AnhGiay;
            zSize = g.Size;
            dGiaBan = double.Parse(g.GiaBan.ToString());
            iSoLuong = 1;
        }

    }
}