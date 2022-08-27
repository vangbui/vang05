using ShopGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopGiay.Controllers
{
    public class SearchController : Controller
    {
        dbGiayDataContext data = new dbGiayDataContext();
        // GET: Search       

        //public ActionResult Index(string strSearch)
        //{
            
        //    return View();
        //}
        

        public ActionResult Search(string strSearch)
        {
            ViewBag.Search = strSearch;
            var kq = new List<GIAY>();
            if (!string.IsNullOrEmpty(strSearch))
            {
                kq = (from g in data.GIAYs where g.SoLuongBan >= 5 && g.SoLuongBan <= 10 && g.TenGiay.ToLower().Contains(strSearch.ToLower()) select g).ToList();

                return View(kq);
            }
            return View(kq);
        }
       
        public ActionResult ThongKe()
        {
            var kq = from g in data.GIAYs
                     group g by g.MaNH into g
                     select new ReportInfo
                     {
                         Id = g.Key.ToString(),
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };
            return View(kq);
        }
    }
}