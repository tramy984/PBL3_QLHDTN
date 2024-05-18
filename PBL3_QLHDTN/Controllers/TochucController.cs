using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBL3_QLHDTN.Models;
using PBL3_QLHDTN.ViewModel;
using System;
using System.Globalization;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace PBL3_QLHDTN.Controllers
{
    public class TochucController : Controller
    {
        QlhdtnContext db = new QlhdtnContext();

        public IActionResult Trangchutochuc()
        {

            // return View();
            var listHoatdong = (from hoatdong in db.Hoatdongs
                                join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                join linhvuc in db.Linhvucs on mota.Linhvuc equals linhvuc.Idlinhvuc
                                where hoatdong.Trangthai == 0
                                select new ViewModel.thongtinHoatdong
                                {
                                    Idhoatdong1 = hoatdong.Idhoatdong,
                                    Tenhoatdong1 = mota.Tenhoatdong,
                                    MuctieuHd1 = mota.MuctieuHd,
                                    Linhvuc11 = linhvuc.Linhvuc1
                                }).ToList();

            return View(listHoatdong);
        }
        public IActionResult TaoHD()
        {
            var LV = db.Linhvucs
               .Select(n => n.Linhvuc1).ToList();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            var ketqua = new LinhvucView
            {
                TenLV = LV,
            };
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            return View(ketqua);
        }
        public int LayIDLV(string tenLV)
        {

            int idLinhVuc = -1; // Giả sử mặc định không tìm thấy
            using (var context = new QlhdtnContext())
            {
                var linhVuc = context.Linhvucs.FirstOrDefault(lv => lv.Linhvuc1 == tenLV);
                if (linhVuc != null)
                {
                    idLinhVuc = linhVuc.Idlinhvuc;
                }
            }

            return idLinhVuc;
        }
        [HttpPost]
        public IActionResult TaoHD(string TenHD, string QuanHuyen, string PhuongXa, string linhvuc, DateOnly TGBD,
            DateOnly TGKT, DateOnly TGKTDK)
        {
            var Lv = db.Linhvucs
               .Select(n => n.Linhvuc1).ToList();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            var ketqua = new LinhvucView
            {
                TenLV = Lv,
            };
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TenHD = Request.Form["TenHD"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            string DiaDiem = Request.Form["DiaDiem"] + " Phường " + PhuongXa + " Quận " + QuanHuyen;
            string LV = linhvuc;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Muctieu = Request.Form["ThongDiep"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
            string SL = Int32.Parse(Request.Form["SL"]).ToString();
#pragma warning restore CS8604 // Possible null reference argument.

            int? IDTC = HttpContext.Session.GetInt32("UserID");


            if (string.IsNullOrEmpty(TenHD) || TGBD == default(DateOnly) || TGKT == default(DateOnly) ||
                string.IsNullOrEmpty(DiaDiem) || string.IsNullOrEmpty(LV) ||
                string.IsNullOrEmpty(Muctieu) || string.IsNullOrEmpty(SL))
            {
                ViewBag.thongbao = "Vui lòng nhập đầy đủ thông tin.";
                return View(ketqua);
            }
            else
            {
                var hd = new Models.Hoatdong
                {
                    Idtochuc = IDTC,
                    Sltnvcan = Convert.ToInt32(SL),
                    Trangthai = 0,
                };
                db.Hoatdongs.Add(hd);
                db.SaveChanges();
                HttpContext.Session.SetInt32("IDhoatdong", hd.Idhoatdong);
                var motahd = new MotaHd
                {
                    Idhoatdong = hd.Idhoatdong,
                    Tenhoatdong = TenHD,
                    Linhvuc = LayIDLV(LV),
                    DiaDiem = DiaDiem,
                    MuctieuHd = Muctieu,
                    Thoigianbatdau = TGBD,
                    Thoigiaketthuc = TGKT,
                    Thoigianketthucdk = TGKTDK,
                };
                db.MotaHds.Add(motahd);
                db.SaveChanges();
                return Redirect("Themban");
            }
        }
        public IActionResult Themban()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Themban(string TenHD)
        {
            int? IDhoatdong = HttpContext.Session.GetInt32("IDhoatdong");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string tenban = Request.Form["Tenban"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Sl = Request.Form["SLBan"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Nhiemvu = Request.Form["Nhiemvu"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Yeucau = Request.Form["Yeucau"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (string.IsNullOrEmpty(tenban) || string.IsNullOrEmpty(Sl))
            {
                ViewBag.thongbao("Vui lòng nhập tên ban và số lượng tình nguyện viên của ban");
                var ketqua = (from yeucau in db.YeucauTnvs
                              where yeucau.Idhoatdong == IDhoatdong
                              select new ViewModel.banHD
                              {
                                  Tenban = yeucau.Tenban,
                                  Sltnvcan = yeucau.Sltnvcan,
                                  Nhiemvu = yeucau.Nhiemvu,
                                  Yeucau = yeucau.Yeucau,
                              }).ToList();

                return View(ketqua);
            }
            else
            {
                var yc = new YeucauTnv
                {
                    Idhoatdong = IDhoatdong,
                    Tenban = tenban,
                    Nhiemvu = Nhiemvu,
                    Yeucau = Yeucau,
                    Sltnvcan = Convert.ToInt32(Sl),
                    Sltnvdk = 0,
                };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var u = db.YeucauTnvs
                .Where(ycau => ycau.Idhoatdong.Equals(IDhoatdong) && ycau.Tenban.Equals(tenban))
                .Select(ycau => new { IDhoatdong = IDhoatdong, tenban = tenban })
                .FirstOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (u == null)
                {
                    db.YeucauTnvs.Add(yc);
                    db.SaveChanges();
                    ViewBag.thongbao = "Tạo ban thành công";
                }
                else
                {
                    ViewBag.thongbao = "Ban " + u.tenban + " đã tồn tại";
                }
                var ketqua = (from yeucau in db.YeucauTnvs
                              where yeucau.Idhoatdong == IDhoatdong
                              select new ViewModel.banHD
                              {
                                  Tenban = yeucau.Tenban,
                                  Sltnvcan = yeucau.Sltnvcan,
                                  Nhiemvu = yeucau.Nhiemvu,
                                  Yeucau = yeucau.Yeucau,
                              }).ToList();

                return View(ketqua);
            }
        }

        public IActionResult DSHD()
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;

            int? IDTC = HttpContext.Session.GetInt32("UserID");
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

            return View(ketqua);

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
        public IActionResult DSHD(string cbbtinhtrang, string cbblinhvuc, string thoigian, string tenhoatdong)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            tenhoatdong = Request.Form["Tenhoatdong"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            int? IDTC = HttpContext.Session.GetInt32("UserID");

            if (cbbtinhtrang == null)
            {
                if (cbblinhvuc == null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                    else
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc != null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                    else
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc == null && thoigian != null)
                {
                    DateTime startday = DateTime.Parse(thoigian);
                    DateTime endday = startday.AddMonths(1).AddDays(-1);
                    if (string.IsNullOrEmpty(tenhoatdong) == true)
                    {

#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                    else
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc != null && thoigian == null)
                {
                    if (string.IsNullOrEmpty(tenhoatdong) == false)
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                    else
                    {
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.

                        return View(ketqua);
                    }
                }
                if (cbblinhvuc == null && thoigian != null)
                {
                    DateTime startday = DateTime.Parse(thoigian);
                    DateTime endday = startday.AddMonths(1).AddDays(-1);
                    if (string.IsNullOrEmpty(tenhoatdong) == true)
                    {

#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
#pragma warning disable CS8601 // Possible null reference assignment.
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
#pragma warning restore CS8601 // Possible null reference assignment.
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
        public static List<TNV> listTNV = new List<TNV>();


        public IActionResult DSTNV(int idhd)
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
                              Idcanhan = cn.Idcanhan,
                              Trangthaiduyetdon = ql.Trangthaiduyetdon,
                              Tinhtrangthamgia = ql.Tinhtrangthamgia,


                          }).ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

            return View(ketqua);

        }
        [HttpPost]
        public IActionResult DSTNV(int idhd, string action, string tentnv, string cbbtinhtrang, string cbbgioitinh)
        {
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
        public IActionResult Duyetthamgia(int idhd, int idcanhan)
        {
            var qltnv = (from ql in db.QuanlyTghds
                         where ql.Idhoatdong == idhd && ql.Idcanhan == idcanhan
                         select new QuanlyTghd
                         {
                             Id = ql.Id,
                             Idcanhan = idcanhan,
                             Idhoatdong = idhd,
                             Tinhtranghuy = ql.Tinhtranghuy,
                             Trangthaiduyetdon = ql.Trangthaiduyetdon,
                             Tinhtrangthamgia = ql.Tinhtrangthamgia,
                         }).FirstOrDefault();
            if (qltnv != null)
            {
                if (qltnv.Tinhtranghuy == true) { ViewBag.thongbao = "Tình nguyện viên đã huỷ đăng ký."; }
                else
                {

                    qltnv.Trangthaiduyetdon = true;

                    db.Update(qltnv);
                    db.SaveChanges();
                }

            }

            //return View();
            return RedirectToAction("DSTNV", new { idhd = idhd });
        }
        public IActionResult Hienthithongtincanhan(int userID)
        {

            var u = db.Canhans.Where(model => model.Idcanhan.Equals(userID)).Select(model => new { Ten = model.Ten, Email = model.Email, Sdt = model.Sdt, Diachi = model.Diachi, Gioitinh = model.Gioitinh, Namsinh = model.Namsinh }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.idcanhan = userID;
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
            var soLuongHoaDong = db.QuanlyTghds.Where(model => model.Idcanhan.Equals(userID) && model.Tinhtrangthamgia == true).ToList().Count();
            ViewBag.sohoatdong = soLuongHoaDong;


            return View();
        }
        public IActionResult thongtinToChuc()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            List<ViewModel.thongtinHoatdong> listHoatdong = (from hoatdong in db.Hoatdongs
                                                             join mota in db.MotaHds on hoatdong.Idhoatdong equals mota.Idhoatdong
                                                             join linhvuc in db.Linhvucs on mota.Linhvuc equals linhvuc.Idlinhvuc
                                                             where hoatdong.Idtochuc == userID
                                                             select new ViewModel.thongtinHoatdong
                                                             {
                                                                 Idhoatdong1 = hoatdong.Idhoatdong,
                                                                 Tenhoatdong1 = mota.Tenhoatdong,
                                                                 MuctieuHd1 = mota.MuctieuHd,
                                                                 Linhvuc11 = linhvuc.Linhvuc1
                                                             }).ToList();
            int slhd = db.Hoatdongs.Where(n => n.Idtochuc == userID).Count();
            var ketqua = (from tochuc in db.Tochucs
                          join hoatdong in db.Tochucs on tochuc.Idtochuc equals hoatdong.Idtochuc
                          join mota in db.Motatochucs on tochuc.Idtochuc equals mota.Idtochuc
                          where tochuc.Idtochuc == userID
                          select new ViewModel.thongtinTochuc
                          {
                              Idtochuc = tochuc.Idtochuc,
                              Ten = tochuc.Ten,
                              Email = tochuc.Email,
                              Diachi = tochuc.Diachi,
                              Sdt = tochuc.Sdt,
                              Gioithieu = mota.Gioithieu,
                              Thanhtuu = mota.Thanhtuu,
                              soluonghd = slhd,
                              Hoatdong = listHoatdong,
                          }).FirstOrDefault();
            return View(ketqua);


        }

        public IActionResult HuyDK(int idhd)
        {
            var hoatdong = (from hd in db.Hoatdongs

                            where hd.Idhoatdong == idhd
                            select new Models.Hoatdong
                            {
                                Idhoatdong = idhd,
                                Trangthai = hd.Trangthai,
                                Idtochuc = hd.Idtochuc,
                                Sltnvcan = hd.Sltnvcan,

                            }).FirstOrDefault();
            if (hoatdong != null)
            {
                hoatdong.Trangthai = 3;
                db.Update(hoatdong);
                db.SaveChanges();
            }
            return RedirectToAction("DSHD");
        }
        public IActionResult suathongtinTochuc()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var u = (from tc in db.Tochucs
                     join mt in db.Motatochucs on tc.Idtochuc equals mt.Idtochuc
                     where (tc.Idtochuc == userID)
                     select new
                     {
                         IDtochuc = userID,
                         TenTC = tc.Ten,
                         SDT = tc.Sdt,
                         Email = tc.Email,
                         Diachi = tc.Diachi,
                         Gioithieu = mt.Gioithieu,
                     }).FirstOrDefault();

            if (u != null)
            {
                ViewBag.Ten = u.TenTC.ToString();
                ViewBag.Email = u.Email.ToString();
                ViewBag.SDT = u.SDT.ToString();
                ViewBag.DC = u.Diachi.ToString();
                ViewBag.Gioithieu = u.Gioithieu.ToString();
            }
            return View();

        }
        [HttpPost]
        public IActionResult suathongtinTochuc(string hoTen, string soDienThoai, string email, string diaChi, string gioithieu)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId != 0)
            {
                var user = db.Tochucs.FirstOrDefault(u => u.Idtochuc == userId);
                var mt = db.Motatochucs.FirstOrDefault(u => u.Idtochuc == userId);
                if (user != null)
                {
                    user.Ten = hoTen;
                    user.Diachi = diaChi;
                    user.Sdt = soDienThoai;
                    user.Email = email;
                    db.SaveChanges();
                }
                if (mt != null)
                {
                    mt.Gioithieu = gioithieu;
                    db.SaveChanges();
                }
                return RedirectToAction("suathongtinTochuc");

            }
            else return View();
        }
        public IActionResult ChinhsuaHD(int Idhoatdongchinhsua)
        {       
            var u=(from mota in db.MotaHds
                   where mota.Idhoatdong== Idhoatdongchinhsua
                   select new MotaHd
                   {
                       Tenhoatdong=mota.Tenhoatdong,
                       DiaDiem=mota.DiaDiem,
                       MuctieuHd=mota.MuctieuHd,
                       Thoigianbatdau=mota.Thoigianbatdau,
                       Thoigiaketthuc=mota.Thoigiaketthuc,
                       Tgbdchinhsua=mota.Tgbdchinhsua,
                       Tgktchinhsua=mota.Tgktchinhsua,
                   }).FirstOrDefault();
            /*var u = db.MotaHds.Where(model => model.Idhoatdong.Equals(Idhoatdongchinhsua))
                .Select(model => new MotaHd{ Tenhoatdong = model.Tenhoatdong, DiaDiem = model.DiaDiem,
                    MuctieuHd = model.MuctieuHd, Thoigianbatdau = model.Thoigianbatdau, Thoigiaketthuc = model.Thoigiaketthuc, 
                    Tgbdchinhsua = model.Tgbdchinhsua, Tgktchinhsua = model.Tgktchinhsua }).FirstOrDefault();*/
            if (u != null)
            {
                ViewBag.TenHD = u.Tenhoatdong;
                ViewBag.DiaDiem = u.DiaDiem;
                ViewBag.MucTieu = u.MuctieuHd;
                if (u.Tgbdchinhsua != null)
                {
                    ViewBag.ThoiGianBD = u.Tgbdchinhsua;

                }
                else
                {
                    ViewBag.ThoiGianBD = u.Thoigianbatdau;
                }
                if (u.Tgktchinhsua != null)
                {
                    ViewBag.ThoiGianKT = u.Tgktchinhsua;

                }
                else
                {
                    ViewBag.ThoiGianKT = u.Thoigiaketthuc;
                }

            }
            return View();
        }
        [HttpPost]
        public IActionResult ChinhsuaHD(int Idhoatdongchinhsua, string TenHD, string DiaDiem, string MucTieu, DateOnly ThoiGianBD, DateOnly ThoiGianKT)
        {
            if (Idhoatdongchinhsua != 0)
            {
                var hd = db.MotaHds.FirstOrDefault(u => u.Idhoatdong == Idhoatdongchinhsua);

                if (hd != null)
                {
                    hd.Tenhoatdong = TenHD;
                    hd.MuctieuHd = MucTieu;
                    hd.DiaDiem = DiaDiem;
                    if (hd.Tgbdchinhsua == null)
                    {
                        hd.Tgbdchinhsua = ThoiGianBD;                      
                    }
                    else
                    {
                        hd.Thoigianbatdau = hd.Tgbdchinhsua;
                        hd.Tgbdchinhsua = ThoiGianBD;
                    }
                    if (hd.Tgktchinhsua == null)
                    {
                        hd.Tgktchinhsua = ThoiGianBD;
                    }
                    else
                    {
                        hd.Thoigiaketthuc = hd.Tgktchinhsua;
                        hd.Tgktchinhsua = ThoiGianKT;
                    }
                    db.SaveChanges();

                    return RedirectToAction("Chinhsuahoatdong");
                }
            }

            return RedirectToAction("Chinhsuahoatdong");

        }

    }
}
