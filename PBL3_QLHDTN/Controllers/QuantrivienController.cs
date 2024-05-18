using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;
using PBL3_QLHDTN.ViewModel;
using System.Globalization;

namespace PBL3_QLHDTN.Controllers
{
    public class QuantrivienController : Controller
    {
        QlhdtnContext db = new QlhdtnContext();
        public IActionResult Trangchuquantrivien()
        {
            return View();
        }
        public IActionResult QuanlyCanhan()
        {
            var ketqua = db.Canhans.ToList();

            return View(ketqua);
        }
        [HttpPost]
        public IActionResult QuanlyCanhan(string tencn, string cbbtinhtrang)
        {
            if (string.IsNullOrEmpty(cbbtinhtrang) == true)
            {
                if (string.IsNullOrEmpty(tencn) == true)
                {
                    var ketqua = db.Canhans.ToList();

                    return View(ketqua);
                }
                else
                {
                    var ketqua = db.Canhans.Where(n => n.Ten == tencn).ToList();

                    return View(ketqua);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tencn) == true)
                {
                    var ketqua = (from taikhoan in db.Taikhoans
                                  join canhan in db.Canhans on taikhoan.Id equals canhan.Idcanhan
                                  where taikhoan.Trangthai == Convert.ToBoolean(cbbtinhtrang)
                                  select new Canhan
                                  {
                                      Idcanhan = canhan.Idcanhan,
                                      Ten = canhan.Ten,
                                      Email = canhan.Email,
                                  }).ToList();

                    return View(ketqua);
                }
                else
                {
                    var ketqua = (from taikhoan in db.Taikhoans
                                  join canhan in db.Canhans on taikhoan.Id equals canhan.Idcanhan
                                  where taikhoan.Trangthai == Convert.ToBoolean(cbbtinhtrang)
                                  && canhan.Ten == tencn
                                  select new Canhan
                                  {
                                      Idcanhan = canhan.Idcanhan,
                                      Ten = canhan.Ten,
                                      Email = canhan.Email,
                                  }).ToList();

                    return View(ketqua);
                }
            }
            return View();
        }

        public IActionResult thongtinCN(int id)
        {
            var ds = db.QuanlyTghds.Where(n => n.Idcanhan == id).ToList();
            var ketqua = (from taikhoan in db.Taikhoans
                          join canhan in db.Canhans on taikhoan.Id equals canhan.Idcanhan
                          where taikhoan.Id == id
                          select new thongtinCanhan
                          {
                              Idcanhan = canhan.Idcanhan,
                              Ten = canhan.Ten,
                              Email = canhan.Email,
                              Sdt = canhan.Sdt,
                              Diachi = canhan.Diachi,
                              Gioitinh = canhan.Gioitinh,
                              Namsinh = canhan.Namsinh,
                              Trangthai = taikhoan.Trangthai,
                              dsHoatdong = ds,
                          }).FirstOrDefault();
            List<thongtinDangKy> listHD = (from canhan in db.Canhans
                                           join ql in db.QuanlyTghds on canhan.Idcanhan equals ql.Idcanhan
                                           join hd in db.MotaHds on ql.Idhoatdong equals hd.Idhoatdong
                                           where canhan.Idcanhan == id
                                           select new thongtinDangKy
                                           {
                                               Idcanhan1 = canhan.Idcanhan,
                                               Idhoatdong1 = hd.Idhoatdong,
                                                Tenhoatdong1= hd.Tenhoatdong,
                                               Thoigiandangky1 = ql.Thoigiandangky,
                                               Bandangky1 = ql.Tenban,
                                           }).ToList();
            ViewBag.listHD = listHD;
            return View(ketqua);
        }

        public IActionResult DSHDCanhan(int idcn)
        {
            var ketqua = (from tt in db.QuanlyTghds
                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                          where tt.Idcanhan == idcn
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
            HttpContext.Session.SetInt32("IDCN", idcn);
            return View(ketqua);
        }
        [HttpPost]
    
        public IActionResult DSHDCanhan(string cbbtinhtrang, string cbblinhvuc, string thoigian, string tenhoatdong, string action)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.Linhvuc = linhvuc;
            if (action == "display")
            {
                int? idcanhan = HttpContext.Session.GetInt32("IDCN");
                int idcn = Convert.ToInt32(idcanhan);
                if (cbbtinhtrang == null)
                {
                    if (cbblinhvuc == null && thoigian == null)
                    {
                        if (string.IsNullOrEmpty(tenhoatdong) == false)
                        {
                            var ketqua = (from tt in db.QuanlyTghds
                                          join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                          join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                          where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                          where tt.Idcanhan == idcn
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
                                          where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                          where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                            where tt.Idcanhan == idcn
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
                                            where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                            where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                            where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
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
                    if (cbbtinhtrang == "Đã đăng ký")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                                  Tinhtrang = Gettinhtrang(tt),
                                              }).ToList();
                                return View(ketqua);
                            }
                            else
                            {

                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == idcn
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn
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
                                                where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
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
                    if (cbbtinhtrang == "Đã được duyệt")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                              where tt.Idcanhan == idcn
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn
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
                                                where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
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
                    if (cbbtinhtrang == "Đã tham gia")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                              where tt.Idcanhan == idcn
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn
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
                                                where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
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
                    if (cbbtinhtrang == "Đã huỷ")
                    {
                        if (cbblinhvuc == null && thoigian == null)
                        {
                            if (string.IsNullOrEmpty(tenhoatdong) == false)
                            {
                                var ketqua = (from tt in db.QuanlyTghds
                                              join mt in db.MotaHds on tt.Idhoatdong equals mt.Idhoatdong
                                              join hd in db.Hoatdongs on tt.Idhoatdong equals hd.Idhoatdong
                                              where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                              where tt.Idcanhan == idcn
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                              where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn
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
                                                where tt.Idcanhan == idcn && mt.Tenhoatdong == tenhoatdong
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
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
                                                where tt.Idcanhan == idcn && mt.Linhvuc == Convert.ToInt32(cbblinhvuc) && mt.Tenhoatdong == tenhoatdong
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

            return View();
        }
        public IActionResult Khoataikhoan(int id)
        {
            var tkkhoa = db.Taikhoans.Where(model => model.Id.Equals(id)).FirstOrDefault();
            if (tkkhoa != null)
            {
                tkkhoa.Trangthai = false;
                db.SaveChanges();
            }
            ViewBag.thongbao = id;
            // return View();
            return RedirectToAction("thongtinCN", new { id = id });
        }
        public IActionResult Motaikhoan(int id)
        {
            var tkkhoa = db.Taikhoans.Where(model => model.Id.Equals(id)).FirstOrDefault();
            if (tkkhoa != null)
            {
                tkkhoa.Trangthai = true;
                db.SaveChanges();
            }
            ViewBag.thongbao = id;
            return RedirectToAction("thongtinCN", new { id = id });
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
        public int LayIDTT(string tenTT)
        {

            int idTT = -1; // Giả sử mặc định không tìm thấy
            using (var context = new QlhdtnContext())
            {
                var tt = context.TrangthaiHds.FirstOrDefault(lv => lv.Trangthai == tenTT);
                if (tt != null)
                {
                    idTT = tt.Idtrangthai;
                }
            }

            return idTT;
        }
        [HttpPost]
        public IActionResult DSHDTochuc(string cbbtinhtrang, string cbblinhvuc, string thoigian, string tenhoatdong, int IDTC)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
            tenhoatdong = Request.Form["Tenhoatdong"];
            // int? IDTC = HttpContext.Session.GetInt32("UserID");

            if (cbbtinhtrang == null)
            {
                if (cbblinhvuc == null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Tenhoatdong == tenhoatdong
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                    else
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc != null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                      && mt.Tenhoatdong == tenhoatdong
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                    else
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc == null && thoigian != null)
                {
                    DateTime startday = DateTime.Parse(thoigian);
                    DateTime endday = startday.AddMonths(1).AddDays(-1);
                    ViewBag.thongbao = "THD" + tenhoatdong;
                    if (string.IsNullOrEmpty(tenhoatdong) == true)
                    {

                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Tenhoatdong == tenhoatdong
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                        && mt.Tenhoatdong == tenhoatdong
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                if (cbblinhvuc == null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Tenhoatdong == tenhoatdong
                                      && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                    else
                    {
                        ViewBag.thongbao = LayIDTT(cbbtinhtrang);
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC
                                      && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc != null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                      && mt.Tenhoatdong == tenhoatdong && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                    else
                    {
                        var ketqua = (from hd in db.Hoatdongs
                                      join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                      join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                      join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                      where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                      && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                      select new ViewModel.Hoatdong
                                      {
                                          Idhoatdong = hd.Idhoatdong,
                                          Tenhoatdong = mt.Tenhoatdong,
                                          Thoigianbatdau = mt.Thoigianbatdau,
                                          Thoigiaketthuc = mt.Thoigiaketthuc,
                                          Tgbdchinhsua = mt.Tgbdchinhsua,
                                          Tgktchinhsua = mt.Tgktchinhsua,
                                          DiaDiem = mt.DiaDiem,
                                          TenLV = lv.Linhvuc1,
                                          MuctieuHd = mt.MuctieuHd,
                                          Sltnvcan = hd.Sltnvcan,
                                          Trangthai = tt.Trangthai,
                                      }).ToList();

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc == null && thoigian != null)
                {
                    DateTime startday = DateTime.Parse(thoigian);
                    DateTime endday = startday.AddMonths(1).AddDays(-1);
                    ViewBag.thongbao = "THD" + tenhoatdong;
                    if (string.IsNullOrEmpty(tenhoatdong) == true)
                    {

                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Tenhoatdong == tenhoatdong
                                        && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                        && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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
                        var hoatdong = (from hd in db.Hoatdongs
                                        join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                                        join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                        join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                                        where hd.Idtochuc == IDTC && mt.Linhvuc == Convert.ToInt32(cbblinhvuc)
                                        && mt.Tenhoatdong == tenhoatdong && hd.Trangthai == LayIDTT(cbbtinhtrang)
                                        select new ViewModel.Hoatdong
                                        {
                                            Idhoatdong = hd.Idhoatdong,
                                            Tenhoatdong = mt.Tenhoatdong,
                                            Thoigianbatdau = mt.Thoigianbatdau,
                                            Thoigiaketthuc = mt.Thoigiaketthuc,
                                            Tgbdchinhsua = mt.Tgbdchinhsua,
                                            Tgktchinhsua = mt.Tgktchinhsua,
                                            DiaDiem = mt.DiaDiem,
                                            TenLV = lv.Linhvuc1,
                                            MuctieuHd = mt.MuctieuHd,
                                            Sltnvcan = hd.Sltnvcan,
                                            Trangthai = tt.Trangthai,
                                        }).ToList();
                        List<ViewModel.Hoatdong> ketqua = new List<ViewModel.Hoatdong>();
                        foreach (var i in hoatdong)
                        {
                            DateOnly? TGBD;
                            if (i.Tgbdchinhsua != null)
                            {
                                TGBD = i.Tgbdchinhsua;
                            }
                            else TGBD = i.Thoigianbatdau;
                            DateOnly? TGKT;
                            if (i.Tgktchinhsua != null)
                            {
                                TGKT = i.Tgktchinhsua;
                            }
                            else TGKT = i.Thoigiaketthuc;
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

            return View();
        }
        public IActionResult QuanlyTochuc()
        {
            var ketqua = db.Tochucs.ToList();
            return View(ketqua);
        }
        [HttpPost]
        public IActionResult QuanlyTochuc(string tentc, string cbbtinhtrang)
        {
            if (string.IsNullOrEmpty(cbbtinhtrang) == true)
            {
                if (string.IsNullOrEmpty(tentc) == true)
                {
                    var ketqua = db.Tochucs.ToList();

                    return View(ketqua);
                }
                else
                {
                    var ketqua = db.Tochucs.Where(n => n.Ten == tentc).ToList();

                    return View(ketqua);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tentc) == true)
                {
                    var ketqua = (from taikhoan in db.Taikhoans
                                  join tochuc in db.Tochucs on taikhoan.Id equals tochuc.Idtochuc
                                  where taikhoan.Trangthai == Convert.ToBoolean(cbbtinhtrang)
                                  select new Tochuc
                                  {
                                      Idtochuc = tochuc.Idtochuc,
                                      Ten = tochuc.Ten,
                                      Email = tochuc.Email,
                                      Diachi = tochuc.Diachi,
                                  }).ToList();

                    return View(ketqua);
                }
                else
                {
                    var ketqua = (from taikhoan in db.Taikhoans
                                  join tochuc in db.Tochucs on taikhoan.Id equals tochuc.Idtochuc
                                  where taikhoan.Trangthai == Convert.ToBoolean(cbbtinhtrang)
                                  && tochuc.Ten == tentc
                                  select new Tochuc
                                  {
                                      Idtochuc = tochuc.Idtochuc,
                                      Ten = tochuc.Ten,
                                      Email = tochuc.Email,
                                      Diachi = tochuc.Diachi,
                                  }).ToList();

                    return View(ketqua);
                }
            }
            return View();
        }
        public IActionResult chitietTochuc(int id)
        {
            int slhd = db.Hoatdongs.Where(n => n.Idtochuc == id).Count();
            var ketqua = (from taikhoan in db.Taikhoans
                          join tochuc in db.Tochucs on taikhoan.Id equals tochuc.Idtochuc
                          join hd in db.Hoatdongs on tochuc.Idtochuc equals hd.Idtochuc
                          join mota in db.Motatochucs on hd.Idhoatdong equals mota.Idtochuc
                          where taikhoan.Id == id
                          select new ViewModel.thongtinTochuc
                          {
                              Idtochuc = taikhoan.Id,
                              Ten = tochuc.Ten,
                              Email = tochuc.Email,
                              Diachi = tochuc.Diachi,
                              Sdt = tochuc.Sdt,
                              Gioithieu = mota.Gioithieu,
                              Thanhtuu = mota.Thanhtuu,
                              soluonghd = slhd,
                              Trangthai = taikhoan.Trangthai,
                          }).FirstOrDefault();
            return View(ketqua);
        }
        public IActionResult DSHDTochuc(int idtc)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
            var ketqua = (from hd in db.Hoatdongs
                          join mt in db.MotaHds on hd.Idhoatdong equals mt.Idhoatdong
                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                          join lv in db.Linhvucs on mt.Linhvuc equals lv.Idlinhvuc
                          where hd.Idtochuc == idtc
                          select new ViewModel.Hoatdong
                          {
                              Idhoatdong = hd.Idhoatdong,
                              Tenhoatdong = mt.Tenhoatdong,
                              Thoigianbatdau = mt.Thoigianbatdau,
                              Thoigiaketthuc = mt.Thoigiaketthuc,
                              Tgbdchinhsua = mt.Tgbdchinhsua,
                              Tgktchinhsua = mt.Tgktchinhsua,
                              DiaDiem = mt.DiaDiem,
                              TenLV = lv.Linhvuc1,
                              MuctieuHd = mt.MuctieuHd,
                              Sltnvcan = hd.Sltnvcan,
                              Trangthai = tt.Trangthai,
                          }).ToList();

            return View(ketqua);
        }
        public IActionResult KhoataikhoanTC(int id)
        {
            var tkkhoa = db.Taikhoans.Where(model => model.Id.Equals(id)).FirstOrDefault();
            if (tkkhoa != null)
            {
                tkkhoa.Trangthai = false;
                db.SaveChanges();
            }
            ViewBag.thongbao = id;
            // return View();
            return RedirectToAction("chitietTochuc", new { id = id });
        }
        public IActionResult MotaikhoanTC(int id)
        {
            var tkkhoa = db.Taikhoans.Where(model => model.Id.Equals(id)).FirstOrDefault();
            if (tkkhoa != null)
            {
                tkkhoa.Trangthai = true;
                db.SaveChanges();
            }
            ViewBag.thongbao = id;
            // return View();
            return RedirectToAction("chitietTochuc", new { id = id });
        }
        public IActionResult DSTNV(int idhd)
        {
            var ketqua = (from ql in db.QuanlyTghds
                          join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                          join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                          where ql.Idhoatdong == idhd
                          select new ViewModel.TNV
                          {
                              Idhoatdong = idhd,
                              Thoigiandangky = ql.Thoigiandangky,
                              Tenban = ql.Tenban,
                              Uudiem = ql.Uudiem,
                              Nhuocdiem = ql.Nhuocdiem,
                              Ten = cn.Ten,
                              Email = cn.Email,
                              tinhtranghd = tt.Trangthai,
                              Idcanhan = cn.Idcanhan,
                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                              Tinhtrangthamgia = ql.Tinhtrangthamgia,


                          }).ToList();
            HttpContext.Session.SetInt32("IDHD", idhd);
            return View(ketqua);
        }
        [HttpPost]
        public IActionResult DSTNV( string action, string tentnv, string cbbtinhtrang, string cbbgioitinh)
        {
            int? idhd = HttpContext.Session.GetInt32("IDHD");
            if (action == "hienthi")
            {
                if (cbbtinhtrang == null)
                {
                    if (cbbgioitinh == null)
                    {
                        if (string.IsNullOrEmpty(tentnv) == true)
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            var ketqua = (from ql in db.QuanlyTghds
                                          join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                          join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                          where ql.Idhoatdong == idhd
                                          select new ViewModel.TNV
                                          {
                                              Idhoatdong = idhd,
                                              Thoigiandangky = ql.Thoigiandangky,
                                              Tenban = ql.Tenban,
                                              Uudiem = ql.Uudiem,
                                              Nhuocdiem = ql.Nhuocdiem,
                                              Ten = cn.Ten,
                                              Email = cn.Email,
                                              tinhtranghd = tt.Trangthai,
                                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                              Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                          }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                            return View(ketqua);
                        }
                        else
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            var ketqua = (from ql in db.QuanlyTghds
                                          join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                          join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                          where ql.Idhoatdong == idhd && cn.Ten == tentnv
                                          select new ViewModel.TNV
                                          {
                                              Idhoatdong = idhd,
                                              Thoigiandangky = ql.Thoigiandangky,
                                              Tenban = ql.Tenban,
                                              Uudiem = ql.Uudiem,
                                              Nhuocdiem = ql.Nhuocdiem,
                                              Ten = cn.Ten,
                                              Email = cn.Email,
                                              tinhtranghd = tt.Trangthai,
                                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                              Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                          }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                            return View(ketqua);

                        }
                    }
                    else
                    {
                        bool GT;
                        if (cbbgioitinh == "Nữ") GT = true;
                        else GT = false;
                        if (string.IsNullOrEmpty(tentnv) == true)
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            var ketqua = (from ql in db.QuanlyTghds
                                          join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                          join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                          where ql.Idhoatdong == idhd && cn.Gioitinh == GT
                                          select new ViewModel.TNV
                                          {
                                              Idhoatdong = idhd,
                                              Thoigiandangky = ql.Thoigiandangky,
                                              Tenban = ql.Tenban,
                                              Uudiem = ql.Uudiem,
                                              Nhuocdiem = ql.Nhuocdiem,
                                              Ten = cn.Ten,
                                              Email = cn.Email,
                                              tinhtranghd = tt.Trangthai,
                                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                              Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                          }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                            return View(ketqua);
                        }
                        else
                        {
#pragma warning disable CS8601 // Possible null reference assignment.
                            var ketqua = (from ql in db.QuanlyTghds
                                          join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                          join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                          join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                          where ql.Idhoatdong == idhd && cn.Gioitinh == GT && cn.Ten == tentnv
                                          select new ViewModel.TNV
                                          {
                                              Idhoatdong = idhd,
                                              Thoigiandangky = ql.Thoigiandangky,
                                              Tenban = ql.Tenban,
                                              Uudiem = ql.Uudiem,
                                              Nhuocdiem = ql.Nhuocdiem,
                                              Ten = cn.Ten,
                                              Email = cn.Email,
                                              tinhtranghd = tt.Trangthai,
                                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                              Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                          }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                            return View(ketqua);

                        }
                    }
                }
                else
                {
                    if (cbbtinhtrang == "Đã đăng ký")
                    {
                        if (cbbgioitinh == null)
                        {
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                        else
                        {
                            bool GT;
                            if (cbbgioitinh == "Nữ") GT = true;
                            else GT = false;
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                    }
                    if (cbbtinhtrang == "Đã được duyệt")
                    {
                        if (cbbgioitinh == null)
                        {
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd
                                              && ql.Trangthaiduyetdon == true && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == true && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                        else
                        {
                            bool GT;
                            if (cbbgioitinh == "Nữ") GT = true;
                            else GT = false;
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT
                                              && ql.Trangthaiduyetdon == true && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == true && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                    }
                    if (cbbtinhtrang == "Đã tham gia")
                    {
                        if (cbbgioitinh == null)
                        {
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == true
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == true
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                        else
                        {
                            bool GT;
                            if (cbbgioitinh == "Nữ") GT = true;
                            else GT = false;
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == true
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == false &&
                                              ql.Tinhtrangthamgia == true
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                    }
                    else
                    {
                        if (cbbgioitinh == null)
                        {
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == true &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == true &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                        else
                        {
                            bool GT;
                            if (cbbgioitinh == "Nữ") GT = true;
                            else GT = false;
                            if (string.IsNullOrEmpty(tentnv) == true)
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == true &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);
                            }
                            else
                            {
#pragma warning disable CS8601 // Possible null reference assignment.
                                var ketqua = (from ql in db.QuanlyTghds
                                              join cn in db.Canhans on ql.Idcanhan equals cn.Idcanhan
                                              join hd in db.Hoatdongs on ql.Idhoatdong equals hd.Idhoatdong
                                              join tt in db.TrangthaiHds on hd.Trangthai equals tt.Idtrangthai
                                              where ql.Idhoatdong == idhd && cn.Gioitinh == GT && cn.Ten == tentnv
                                              && ql.Trangthaiduyetdon == false && ql.Tinhtranghuy == true &&
                                              ql.Tinhtrangthamgia == false
                                              select new ViewModel.TNV
                                              {
                                                  Idhoatdong = idhd,
                                                  Thoigiandangky = ql.Thoigiandangky,
                                                  Tenban = ql.Tenban,
                                                  Uudiem = ql.Uudiem,
                                                  Nhuocdiem = ql.Nhuocdiem,
                                                  Ten = cn.Ten,
                                                  Email = cn.Email,
                                                  tinhtranghd = tt.Trangthai,
                                                  Trangthaiduyetdon = ql.Trangthaiduyetdon,
                                                  Tinhtrangthamgia = ql.Tinhtrangthamgia,
                                              }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

                                return View(ketqua);

                            }
                        }
                    }

                }
            }
            else { }
            return View();

        }
        public IActionResult Xembaocao()
        {
            var ketqua = (from bc in db.Baocaos
                          join tk1 in db.Taikhoans on bc.Idtkbaocao equals tk1.Id
                          join tk2 in db.Taikhoans on bc.Idtkbibaocao equals tk2.Id
                          select new ViewModel.BaocaoViewModel
                          {
                              Idtkbaocao = bc.Idtkbaocao,
                              Idtkbibaocao = bc.Idtkbibaocao,
                              Tgbaocao = bc.Tgbaocao,
                              Ndbaocao = bc.Ndbaocao,
                              VTtaikhoanbc = tk1.Vaitro, // Lấy vai trò của tài khoản báo cáo
                              VTtaikhoanbibc = tk2.Vaitro,
                          }).ToList();
            return View(ketqua);
        }
        [HttpPost]
        public IActionResult Xembaocao(string idbibaocao)
        {
            int id = Convert.ToInt32(idbibaocao);
            if (string.IsNullOrEmpty(idbibaocao) == false)
            {
                var ketqua = (from bc in db.Baocaos
                              join tk1 in db.Taikhoans on bc.Idtkbaocao equals tk1.Id
                              join tk2 in db.Taikhoans on bc.Idtkbibaocao equals tk2.Id
                              where bc.Idtkbibaocao == id
                              select new ViewModel.BaocaoViewModel
                              {
                                  Idtkbaocao = bc.Idtkbaocao,
                                  Idtkbibaocao = bc.Idtkbibaocao,
                                  Tgbaocao = bc.Tgbaocao,
                                  Ndbaocao = bc.Ndbaocao,
                                  VTtaikhoanbc = tk1.Vaitro, // Lấy vai trò của tài khoản báo cáo
                                  VTtaikhoanbibc = tk2.Vaitro,
                              }).ToList();
                if (ketqua.Count() > 0)
                {
                    ViewBag.thongbao = "Tài khoản đã bị báo cáo " + ketqua.Count() + " lần";
                }
                else
                {
                    ViewBag.thongbao = "Tài khoản chưa bị báo cáo";
                }

                return View(ketqua);
            }
            else
            {
                var ketqua = (from bc in db.Baocaos
                              join tk1 in db.Taikhoans on bc.Idtkbaocao equals tk1.Id
                              join tk2 in db.Taikhoans on bc.Idtkbibaocao equals tk2.Id
                              select new ViewModel.BaocaoViewModel
                              {
                                  Idtkbaocao = bc.Idtkbaocao,
                                  Idtkbibaocao = bc.Idtkbibaocao,
                                  Tgbaocao = bc.Tgbaocao,
                                  Ndbaocao = bc.Ndbaocao,
                                  VTtaikhoanbc = tk1.Vaitro, // Lấy vai trò của tài khoản báo cáo
                                  VTtaikhoanbibc = tk2.Vaitro,
                              }).ToList();
                ViewBag.thongbao = "Nhập ID tài khoản bị báo cáo muốn kiểm tra để có dữ liệu cụ thể";
                return View(ketqua);
            }
            return Redirect("Xembaocao");
        }
    }
}
