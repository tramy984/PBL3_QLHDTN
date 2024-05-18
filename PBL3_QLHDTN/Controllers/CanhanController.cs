using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PBL3_QLHDTN.Models;
using System.ComponentModel.Design;
using PBL3_QLHDTN.ViewModel;
using System.Globalization;
using System;

namespace PBL3_QLHDTN.Controllers
{
    public class CanhanController : Controller
    {
        public static int? IDcanhan;
        public static int idhd;

        QlhdtnContext db = new QlhdtnContext();
      
        public IActionResult Trangchucanhan()
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
            var listHoatdong = (from hoatdong in db.Hoatdongs
                                    join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                    join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                    where hoatdong.Trangthai == 0
                                    select new ViewModel.thongtinHoatdong
                                    {
                                        Idhoatdong1 = hoatdong.Idhoatdong,
                                        Tenhoatdong1 = mota.Tenhoatdong,
                                        MuctieuHd1 = mota.MuctieuHd,
                                        Linhvuc11 = lv.Linhvuc1
                                    }).ToList();

                return View(listHoatdong);
            
  
        }
        [HttpPost]
        public IActionResult Trangchucanhan(string cbblinhvuc, string QuanHuyen, string tenhd)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            tenhd = Request.Form["tenhd"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (cbblinhvuc == null)
            {
                if (QuanHuyen == null)
                {
                    if (string.IsNullOrEmpty(tenhd))
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                    else
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.Tenhoatdong == tenhd
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(tenhd))
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.DiaDiem.Contains(QuanHuyen)
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1,
                                                                         }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                        return View(listHoatdong);
                    }
                    else
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.Tenhoatdong == tenhd
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1,
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                }
            }
            else
            {
                if (QuanHuyen == null)
                {
                    if (string.IsNullOrEmpty(tenhd))
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0
                                                                          && lv.Idlinhvuc == Convert.ToInt32(cbblinhvuc)
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                    else
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.Tenhoatdong == tenhd
                                                                        && lv.Idlinhvuc == Convert.ToInt32(cbblinhvuc)
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(tenhd))
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.DiaDiem.Contains(QuanHuyen)
                                                                         && lv.Idlinhvuc == Convert.ToInt32(cbblinhvuc)
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1,
                                                                         }).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                        return View(listHoatdong);
                    }
                    else
                    {
                        List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                                         join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                                         join lv in db.Linhvucs on mota.Linhvuc equals lv.Idlinhvuc
                                                                         where hoatdong.Trangthai == 0 && mota.Tenhoatdong == tenhd
                                                                         && lv.Idlinhvuc == Convert.ToInt32(cbblinhvuc)
                                                                         select new ViewModel.thongtinHoatdong
                                                                         {
                                                                             Idhoatdong1 = hoatdong.Idhoatdong,
                                                                             Tenhoatdong1 = mota.Tenhoatdong,
                                                                             MuctieuHd1 = mota.MuctieuHd,
                                                                             Linhvuc11 = lv.Linhvuc1,
                                                                         }).ToList();
                        return View(listHoatdong);
                    }
                }
            }
#pragma warning disable CS0162 // Unreachable code detected
            return View();
#pragma warning restore CS0162 // Unreachable code detected
        }
        public static string Gettinhtrang(QuanlyTghd quanlyTghd)
        {
            if (quanlyTghd.Trangthaiduyetdon == true && quanlyTghd.Tinhtranghuy == false && quanlyTghd.Tinhtrangthamgia == false)
            {
                return "Đã được duyệt ";
            }

            if (quanlyTghd.Tinhtranghuy == true && quanlyTghd.Tinhtrangthamgia == false)
            {
                return "Đã hủy ";
            }
            if (quanlyTghd.Trangthaiduyetdon == true && quanlyTghd.Tinhtranghuy == false && quanlyTghd.Tinhtrangthamgia == true)
            {
                return "Đã tham gia";
            }
            return "Đã đăng ký";
        }
        public IActionResult Hoatdongcanhan()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var ketqua = (from tt in db.QuanlyTghds
                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                          where tt.Idcanhan == userID
                          select new HoatdongcanhanViewModel
                          {
                              Idhoatdong = tt.Idhoatdong,
                              Idtochuc = hd.Idtochuc,
                              Tenhoatdong = mt.Tenhoatdong,
                              Thoigiandangky = tt.Thoigiandangky,
                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                              Tinhtrang = Gettinhtrang(tt)
                          }).ToList();
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.Linhvuc = linhvuc;
            return View(ketqua);

        }
        [HttpPost]
        public IActionResult Hoatdongcanhan(string cbbtinhtrang, string cbblinhvuc, string thoigian, string tenhoatdong, string action, int IdhoatdongHuy)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.Linhvuc = linhvuc;
            if (action == "display")
            {
                int? userID = HttpContext.Session.GetInt32("UserID");
                if (cbbtinhtrang == null)
                {
                    if (cbblinhvuc == null && thoigian == null)
                    {
                        if (string.IsNullOrEmpty(tenhoatdong) == false)
                        {
                            var ketqua = (from tt in db.QuanlyTghds
                                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                          where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                          select new HoatdongcanhanViewModel
                                          {
                                              Idhoatdong = tt.Idhoatdong,
                                              Idtochuc = hd.Idtochuc,
                                              Tenhoatdong = mt.Tenhoatdong,
                                              Thoigiandangky = tt.Thoigiandangky,
                                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                              Tinhtrang = Gettinhtrang(tt),
                                          }).ToList();

                            return View(ketqua);
                        }
                        else
                        {

                            var ketqua = (from tt in db.QuanlyTghds
                                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                          where tt.Idcanhan == userID
                                          select new HoatdongcanhanViewModel
                                          {
                                              Idhoatdong = tt.Idhoatdong,
                                              Idtochuc = hd.Idtochuc,
                                              Tenhoatdong = mt.Tenhoatdong,
                                              Thoigiandangky = tt.Thoigiandangky,
                                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                              Tinhtrang = Gettinhtrang(tt)
                                          }).ToList();

                            return View(ketqua);
                        }
                    }
                    if (cbblinhvuc != null && thoigian == null)
                    {
                        if (string.IsNullOrEmpty(tenhoatdong) == false)
                        {
                            var ketqua = (from tt in db.QuanlyTghds
                                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                          where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                    && mt.Tenhoatdong == tenhoatdong
                                          select new HoatdongcanhanViewModel
                                          {
                                              Idhoatdong = tt.Idhoatdong,
                                              Idtochuc = hd.Idtochuc,
                                              Tenhoatdong = mt.Tenhoatdong,
                                              Thoigiandangky = tt.Thoigiandangky,
                                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                              Tinhtrang = Gettinhtrang(tt)
                                          }).ToList();

                            return View(ketqua);
                        }
                        else
                        {
                            var ketqua = (from tt in db.QuanlyTghds
                                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                          where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                          select new HoatdongcanhanViewModel
                                          {
                                              Idhoatdong = tt.Idhoatdong,
                                              Idtochuc = hd.Idtochuc,
                                              Tenhoatdong = mt.Tenhoatdong,
                                              Thoigiandangky = tt.Thoigiandangky,
                                              Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                              Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                              Tinhtrang = Gettinhtrang(tt)
                                          }).ToList();


                            return View(ketqua);
                        }
                    }
                    if (cbblinhvuc == null && thoigian != null)
                    {
                        DateTime startday = DateTime.Parse(thoigian);
                        DateTime endday = startday.AddMonths(1).AddDays(-1);
                        if (string.IsNullOrEmpty(tenhoatdong) == true)
                        {

                            var hoatdong = (from tt in db.QuanlyTghds
                                            join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                            join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                            where tt.Idcanhan == userID
                                            select new HoatdongcanhanViewModel
                                            {
                                                Idhoatdong = tt.Idhoatdong,
                                                Idtochuc = hd.Idtochuc,
                                                Tenhoatdong = mt.Tenhoatdong,
                                                Thoigiandangky = tt.Thoigiandangky,
                                                Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                Tinhtrang = Gettinhtrang(tt)
                                            }).ToList();

                            List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                            foreach (var i in hoatdong)
                            {
                                DateOnly? TGBD;
                                TGBD = i.Thoigianbatdau;

                                DateOnly? TGKT;

                                TGKT = i.Thoigianketthuc;

                                DateTime tgbd;
                                DateTime tgkt;
                                DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                int ss1 = startday.CompareTo(tgbd);
                                int ss2 = endday.CompareTo(tgbd);
                                int ss3 = startday.CompareTo(tgkt);
                                int ss4 = endday.CompareTo(tgkt);
                                if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                {
                                    ketqua.Add(i);
                                }
                                
                            }

                            return View(ketqua);
                        }
                        else
                        {
                            var hoatdong = (from tt in db.QuanlyTghds
                                            join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                            join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                            where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                            select new HoatdongcanhanViewModel
                                            {
                                                Idhoatdong = tt.Idhoatdong,
                                                Idtochuc = hd.Idtochuc,
                                                Tenhoatdong = mt.Tenhoatdong,
                                                Thoigiandangky = tt.Thoigiandangky,
                                                Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                Tinhtrang = Gettinhtrang(tt)
                                            }).ToList();
                            List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                            foreach (var i in hoatdong)
                            {
                                DateOnly? TGBD;
                                TGBD = i.Thoigianbatdau;
                                DateOnly? TGKT;
                                TGKT = i.Thoigianketthuc;
                                DateTime tgbd;
                                DateTime tgkt;
                                DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                int ss1 = startday.CompareTo(tgbd);
                                int ss2 = endday.CompareTo(tgbd);
                                int ss3 = startday.CompareTo(tgkt);
                                int ss4 = endday.CompareTo(tgkt);
                                if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                {
                                    ketqua.Add(i);
                                }

                            }

                            return View(ketqua);
                        }

                    }
                    if (cbblinhvuc != null && thoigian != null)
                    {
                        DateTime startday = DateTime.Parse(thoigian);
                        DateTime endday = startday.AddMonths(1).AddDays(-1);
                        if (string.IsNullOrEmpty(tenhoatdong) == true)
                        {
                            var hoatdong = (from tt in db.QuanlyTghds
                                            join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                            join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                            where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                            select new HoatdongcanhanViewModel
                                            {
                                                Idhoatdong = tt.Idhoatdong,
                                                Idtochuc = hd.Idtochuc,
                                                Tenhoatdong = mt.Tenhoatdong,
                                                Thoigiandangky = tt.Thoigiandangky,
                                                Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                Tinhtrang = Gettinhtrang(tt)
                                            }).ToList();

                            List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                            foreach (var i in hoatdong)
                            {
                                DateOnly? TGBD;
                                TGBD = i.Thoigianbatdau;
                                DateOnly? TGKT;
                                TGKT = i.Thoigianketthuc;
                                DateTime tgbd;
                                DateTime tgkt;
                                DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                int ss1 = startday.CompareTo(tgbd);
                                int ss2 = endday.CompareTo(tgbd);
                                int ss3 = startday.CompareTo(tgkt);
                                int ss4 = endday.CompareTo(tgkt);
                                if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                {
                                    ketqua.Add(i);
                                }

                            }

                            return View(ketqua);
                        }
                        else
                        {
                            var hoatdong = (from tt in db.QuanlyTghds
                                            join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                            join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                            where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
                                            select new HoatdongcanhanViewModel
                                            {
                                                Idhoatdong = tt.Idhoatdong,
                                                Idtochuc = hd.Idtochuc,
                                                Tenhoatdong = mt.Tenhoatdong,
                                                Thoigiandangky = tt.Thoigiandangky,
                                                Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                Tinhtrang = Gettinhtrang(tt)
                                            }).ToList();


                            List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                            foreach (var i in hoatdong)
                            {
                                DateOnly? TGBD;
                                TGBD = i.Thoigianbatdau;
                                DateOnly? TGKT;
                                TGKT = i.Thoigianketthuc;
                                DateTime tgbd;
                                DateTime tgkt;
                                DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                int ss1 = startday.CompareTo(tgbd);
                                int ss2 = endday.CompareTo(tgbd);
                                int ss3 = startday.CompareTo(tgkt);
                                int ss4 = endday.CompareTo(tgkt);
                                if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                {
                                    ketqua.Add(i);
                                }

                            }

                            return View(ketqua);
                        }
                    }

                }
                else
                {
                    if(cbbtinhtrang=="Đã đăng ký")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                              && tt.Tinhtrangthamgia==false && tt.Tinhtranghuy==false
                                              && tt.Trangthaiduyetdon==false
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt),
                                              }).ToList();
                                return View(ketqua);
                            }
                            else
                            {

                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID
                                               && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc != null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                        && mt.Tenhoatdong == tenhoatdong
                                                         && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                            else
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();


                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc == null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {

                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID
                                                 && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;

                                    DateOnly? TGKT;

                                    TGKT = i.Thoigianketthuc;

                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();
                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }

                        }
                        if (cbblinhvuc != null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                 && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
                                                 && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == false
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();


                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                        }
                    }
                    if(cbbtinhtrang=="Đã được duyệt")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                              && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt),
                                              }).ToList();
                                return View(ketqua);
                            }
                            else
                            {

                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID
                                             && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc != null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                        && mt.Tenhoatdong == tenhoatdong
                                                        && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                            else
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                               && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();


                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc == null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {

                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;

                                    DateOnly? TGKT;

                                    TGKT = i.Thoigianketthuc;

                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();
                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }

                        }
                        if (cbblinhvuc != null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();


                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                        }
                    }
                    if(cbbtinhtrang=="Đã tham gia")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                              && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt),
                                              }).ToList();
                                return View(ketqua);
                            }
                            else
                            {

                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID
                                              && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc != null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                        && mt.Tenhoatdong == tenhoatdong
                                                         && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                            else
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                               && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();


                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc == null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {

                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID
                                                 && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;

                                    DateOnly? TGKT;

                                    TGKT = i.Thoigianketthuc;

                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                                  && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();
                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }

                        }
                        if (cbblinhvuc != null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                 && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
                                                 && tt.Tinhtrangthamgia == true && tt.Tinhtranghuy == false
                                              && tt.Trangthaiduyetdon == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();


                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                        }
                    }
                    if(cbbtinhtrang=="Đã huỷ")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                               && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                             
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt),
                                              }).ToList();
                                return View(ketqua);
                            }
                            else
                            {

                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID
                                              && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc != null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                        && mt.Tenhoatdong == tenhoatdong && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();

                                return View(ketqua);
                            }
                            else
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                              && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                              select new HoatdongcanhanViewModel
                                              {
                                                  Idhoatdong = tt.Idhoatdong,
                                                  Idtochuc = hd.Idtochuc,
                                                  Tenhoatdong = mt.Tenhoatdong,
                                                  Thoigiandangky = tt.Thoigiandangky,
                                                  Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                  Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                  Tinhtrang = Gettinhtrang(tt)
                                              }).ToList();


                                return View(ketqua);
                            }
                        }
                        if (cbblinhvuc == null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {

                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID
                                                 && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;

                                    DateOnly? TGKT;

                                    TGKT = i.Thoigianketthuc;

                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Tenhoatdong == tenhoatdong
                                                && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();
                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }

                        }
                        if (cbblinhvuc != null && thoigian != null)
                        {
                            DateTime startday = DateTime.Parse(thoigian);
                            DateTime endday = startday.AddMonths(1).AddDays(-1);
                            if (string.IsNullOrEmpty(tenhoatdong) == true)
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                                 && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();

                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                            else
                            {
                                var hoatdong = (from tt in db.QuanlyTghds
                                                join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                                join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                                where tt.Idcanhan == userID && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
                                                  && tt.Tinhtrangthamgia == false && tt.Tinhtranghuy == true
                                                select new HoatdongcanhanViewModel
                                                {
                                                    Idhoatdong = tt.Idhoatdong,
                                                    Idtochuc = hd.Idtochuc,
                                                    Tenhoatdong = mt.Tenhoatdong,
                                                    Thoigiandangky = tt.Thoigiandangky,
                                                    Thoigianbatdau = mt.Tgbdchinhsua == null ? mt.Thoigianbatdau : mt.Tgbdchinhsua,
                                                    Thoigianketthuc = mt.Tgktchinhsua == null ? mt.Thoigiaketthuc : mt.Tgktchinhsua,
                                                    Tinhtrang = Gettinhtrang(tt)
                                                }).ToList();


                                List<ViewModel.HoatdongcanhanViewModel> ketqua = new List<ViewModel.HoatdongcanhanViewModel>();
                                foreach (var i in hoatdong)
                                {
                                    DateOnly? TGBD;
                                    TGBD = i.Thoigianbatdau;
                                    DateOnly? TGKT;
                                    TGKT = i.Thoigianketthuc;
                                    DateTime tgbd;
                                    DateTime tgkt;
                                    DateTime.TryParseExact(TGBD.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgbd);
                                    DateTime.TryParseExact(TGKT.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgkt);
                                    int ss1 = startday.CompareTo(tgbd);
                                    int ss2 = endday.CompareTo(tgbd);
                                    int ss3 = startday.CompareTo(tgkt);
                                    int ss4 = endday.CompareTo(tgkt);
                                    if ((ss1 < 0 && ss2 > 0) || (ss3 < 0 && ss4 > 0))
                                    {
                                        ketqua.Add(i);
                                    }

                                }

                                return View(ketqua);
                            }
                        }
                    }
                }
            }

                return Hoatdongcanhan();
        }
        public IActionResult Huydangky(int Idhoatdonghuy)
        {

            int? userID = HttpContext.Session.GetInt32("UserID");
            var hoatdonghuy = db.QuanlyTghds.Where(model => model.Idcanhan.Equals(userID) && model.Idhoatdong.Equals(Idhoatdonghuy) && model.Tinhtrangthamgia.Equals(false)).FirstOrDefault();
            if (hoatdonghuy != null)
            {
                hoatdonghuy.Tinhtranghuy = true;
                db.SaveChanges();

            }
            ViewBag.thongbao = Idhoatdonghuy;
            // return View();
            return RedirectToAction("Hoatdongcanhan");
        }
        [HttpGet]
        public IActionResult chitietHoatdong(int id)
        {
            idhd = id;
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == id)
                .Select(n => n.Tenban).ToList();

            var ketqua = (from hoatdong in db.Hoatdongs
                          join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                          join linhvuc in db.Linhvucs on mota.Linhvuc equals linhvuc.Idlinhvuc
                          where hoatdong.Idhoatdong == id
                          select new ViewModel.thongtinHoatdong
                          {
                              Idhoatdong1 = hoatdong.Idhoatdong,
                              Tenhoatdong1 = mota.Tenhoatdong,
                              Thoigianbatdau1 = mota.Thoigianbatdau,
                              Thoigiaketthuc1 = mota.Thoigiaketthuc,
                              DiaDiem1 = mota.DiaDiem,
                              MuctieuHd1 = mota.MuctieuHd,
                              Linhvuc11 = linhvuc.Linhvuc1,
                              Sltnvcan1 = hoatdong.Sltnvcan,
                              tgktdk = mota.Thoigianketthucdk,
                              Tenban1 = tenban
                          }).FirstOrDefault();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            
            return View(ketqua);

        }
        [HttpPost]
        public IActionResult chitiethoatdong(int id)
        {
            idhd = id;
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == id)
               .Select(n => n.Tenban).ToList();

            var ketqua = (from hoatdong in db.Hoatdongs
                          join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                          join linhvuc in db.Linhvucs on mota.Linhvuc equals linhvuc.Idlinhvuc
                          where hoatdong.Idhoatdong == id
                          select new ViewModel.thongtinHoatdong
                          {
                              Idhoatdong1 = hoatdong.Idhoatdong,
                              Tenhoatdong1 = mota.Tenhoatdong,
                              Thoigianbatdau1 = mota.Thoigianbatdau,
                              Thoigiaketthuc1 = mota.Thoigiaketthuc,
                              DiaDiem1 = mota.DiaDiem,
                              MuctieuHd1 = mota.MuctieuHd,
                              Linhvuc11 = linhvuc.Linhvuc1,
                              Sltnvcan1 = hoatdong.Sltnvcan,
                              tgktdk = mota.Thoigianketthucdk,
                              Tenban1 = tenban
                          }).FirstOrDefault();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            DateTime tgktdk;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            DateTime.TryParseExact(ketqua.tgktdk.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tgktdk);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (tgktdk < DateTime.Now)
            {
               
                return RedirectToAction("dangkythamgiaHD");
            }
            else {
                ViewBag.thongbao = "Thời gian đăng ký đã hết";
                return View(ketqua); }
        }
        public IActionResult dangkythamgiaHD()
        {
           // int? idhd = HttpContext.Session.GetInt32("IDhoatdong");
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == idhd)
                .Select(n => n.Tenban).ToList();
            ViewBag.tenban = tenban;
            return View();
        }
        [HttpPost]
        public IActionResult dangkythamgiaHD(thongtinDangKy model, string CBB)
        {
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == idhd)
            .Select(n => n.Tenban).ToList();
            ViewBag.tenban = tenban;
            int? idcn = HttpContext.Session.GetInt32("UserID");
            string uu = model.Uudiem1;
            string nhuoc = model.Nhuocdiem1;
            DateTime dt = DateTime.Now;
            DateOnly dateonly = new DateOnly(dt.Year, dt.Month, dt.Day);
            if (string.IsNullOrEmpty(uu) == true || string.IsNullOrEmpty(nhuoc) == true)
            {
                ViewBag.thongbao = "Vui lòng nhập đầy đủ ưu điểm và nhược điểm";
                return View();
            }
            else
            {
                var u = db.QuanlyTghds
                 .Where(model => model.Idhoatdong.Equals(idhd) && model.Idcanhan.Equals(idcn))
                 .FirstOrDefault();
                if (u == null)
                {
                    if (tenban.Count == 0)
                    {
                        var ql = new QuanlyTghd
                        {
                            Idhoatdong = idhd,
                            Uudiem = uu,
                            Nhuocdiem = nhuoc,
                            Idcanhan = idcn,
                            Thoigiandangky = dateonly,
                            Tinhtranghuy = false,
                            Tinhtrangthamgia = false,
                            Trangthaiduyetdon = false,
                        };
                        db.QuanlyTghds.Add(ql);
                        db.SaveChanges();
                    }
                    else
                    {
                        var ql = new QuanlyTghd
                        {
                            Idhoatdong = idhd,
                            Uudiem = uu,
                            Nhuocdiem = nhuoc,
                            Idcanhan = idcn,
                            Tenban = CBB,
                            Thoigiandangky = dateonly,
                            Tinhtranghuy = false,
                            Tinhtrangthamgia = false,
                            Trangthaiduyetdon = false,
                        };
                        db.QuanlyTghds.Add(ql);
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.thongbao = "Bạn đã đăng ký hoạt động này trước đó";
                }
               
            }
            return View(model);
        }

        public IActionResult Hienthithongtincanhan()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var u = db.Canhans.Where(model => model.Idcanhan.Equals(userID)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi, Gioitinh = model.Gioitinh, Namsinh = model.Namsinh }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.idtochuc = userID;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Ten = u.Ten.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Email = u.Email.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.SDT = u.Sdt.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.DC = u.Diachi.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (u.Gioitinh == false)
                {
                    ViewBag.GT = "Nữ";
                }
                else
                {
                    {
                        ViewBag.GT = "Nam";
                    }
                }
                ViewBag.NS = u.Namsinh.ToString();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Hienthithongtintochuc(int idtc)
        {

            var u = db.Tochucs.Where(model => model.Idtochuc.Equals(idtc)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi }).FirstOrDefault();

            if (u != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Ten = u.Ten.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Email = u.Email.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Sdt = u.Sdt.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Diachi = u.Diachi.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
            var u1 = db.Motatochucs
                .Where(model => model.Idtochuc.Equals(idtc))
                .Select(model => new { Gioithieu = model.Gioithieu, Thanhtuu = model.Thanhtuu }).FirstOrDefault();
            if (u1 != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Mota = u1.Gioithieu.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Thanhtuu = u1.Thanhtuu.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            }

            return View();
        }
        public IActionResult Chinhsuathongtincanhan()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var u = db.Canhans.Where(model => model.Idcanhan.Equals(userID)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi, Gioitinh = model.Gioitinh, Namsinh = model.Namsinh }).FirstOrDefault();

            if (u != null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Ten = u.Ten.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.Email = u.Email.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.SDT = u.Sdt.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                ViewBag.DC = u.Diachi.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (u.Gioitinh == true)
                {
                    ViewBag.GT = "Nữ";
                }
                else
                {
                    ViewBag.GT = "Nam";

                }
                ViewBag.NS = u.Namsinh.ToString();
            }
            return View();

        }
        [HttpPost]
        public IActionResult Chinhsuathongtincanhan(string hoTen, string soDienThoai, string email, string diaChi, string gioiTinh)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId != 0)
            {
                var user = db.Canhans.FirstOrDefault(u => u.Idcanhan == userId);

                if (user != null)
                {
                    user.Ten = hoTen;
                    user.Diachi = diaChi;
                    user.Sdt = soDienThoai;
                    user.Email = email;
                    if (gioiTinh == "Nữ")
                    {
                        user.Gioitinh = true;
                    }
                    else
                    {
                        user.Gioitinh = false;
                    }
                    db.SaveChanges();

                    return RedirectToAction("Chinhsuathongtincanhan");
                }
            }

            return RedirectToAction("Chinhsuathongtincanhan");

        }
        [HttpGet]
        public IActionResult Chitiethoatdongcanhan(int idhd)
        {
            int? user = HttpContext.Session.GetInt32("UserID");
            var ketqua = (from tt in db.QuanlyTghds
                          join mota in db.MotaHds on tt.Idhoatdong equals mota.Idhoatdong
                          join dgtutochuc in db.DanhgiaTnvs on new { tt.Idhoatdong, tt.Idcanhan } equals new { dgtutochuc.Idhoatdong, dgtutochuc.Idcanhan } into dg1
                          from dgtc in dg1.DefaultIfEmpty()
                          join dgtucanhan in db.DanhgiaHds on new { tt.Idhoatdong, tt.Idcanhan } equals new { dgtucanhan.Idhoatdong, dgtucanhan.Idcanhan } into dg2
                          from dgcn in dg2.DefaultIfEmpty()
                          where tt.Idcanhan == user && tt.Idhoatdong == idhd
                          select new ChitiethoatdongViewModel
                          {
                              Tenhoatdong = mota.Tenhoatdong,
                              Thoigianbatdau = mota.Thoigianbatdau,
                              Tgbdchinhsua = mota.Tgbdchinhsua,
                              Thoigiaketthuc = mota.Thoigiaketthuc,
                              Tgktchinhsua = mota.Tgktchinhsua,
                              DiaDiem = mota.DiaDiem,
                              MuctieuHd = mota.MuctieuHd,
                              Tinhtrang = Gettinhtrang(tt),
                              Linhvuc = db.Linhvucs.Where(model => model.Idlinhvuc.Equals(mota.Linhvuc)).Select(model => model.Linhvuc1).FirstOrDefault(),
                              Thoigianhuy = mota.Thoigianhuy,
                              Lydohuy = mota.Lydohuy,
                              Danhgiatutochuc = dgtc.Danhgia,
                              Tgdanhgia1 = dgtc.Tgdanhgia,
                              Danhgiacuatnv = dgcn.Danhgia,
                              Tgdanhgia2 = dgcn.Tgdanhgia

                          }).FirstOrDefault();

            return View(ketqua);
        }
        [HttpPost]
        public IActionResult Chitiethoatdongcanhan(int idhd, string Danhgia)
        {
            if (!string.IsNullOrEmpty(Danhgia))
            {

                int? user = HttpContext.Session.GetInt32("UserID");
                var danhgiamoi = new DanhgiaHd
                {
                    Idcanhan = user,
                    Idhoatdong = idhd,
                    Danhgia = Danhgia,
                    Tgdanhgia = DateTime.Now,
                };
                db.DanhgiaHds.Add(danhgiamoi);
                db.SaveChanges();
            }
            return Chitiethoatdongcanhan(idhd);
        }

    }
}
