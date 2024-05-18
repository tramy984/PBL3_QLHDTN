using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;
using PBL3_QLHDTN.ViewModel;

namespace PBL3_QLHDTN.Controllers
{
    public class HomeController : Controller
    {
        QlhdtnContext db = new QlhdtnContext();
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0169 // The field 'HomeController._context' is never used
        private readonly QlhdtnContext _context;
#pragma warning restore CS0169 // The field 'HomeController._context' is never used
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IActionResult Trangchu()
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
        [HttpPost] public IActionResult Trangchu(string cbblinhvuc,string QuanHuyen,string tenhd)
        {
            var linhvuc = db.Linhvucs.ToList();
            ViewBag.linhvuc = linhvuc;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            tenhd = Request.Form["tenhd"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (cbblinhvuc == null)
            {
                if(QuanHuyen == null) {
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
                                                                         where hoatdong.Trangthai == 0 && mota.Tenhoatdong==tenhd
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
                                                                          && lv.Idlinhvuc==Convert.ToInt32( cbblinhvuc)
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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
        public IActionResult Gioithieu()
        {
            return View();
        }
        public IActionResult thongtinToChuc()
        {
            return View();
        }
        public IActionResult chitiet(int id)
        {
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == id)
                .Select(n => n.Tenban).ToList();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
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
                              Tenban1 = tenban
                          }).FirstOrDefault();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
              return View(ketqua);
        }
        [HttpPost]
        public IActionResult chitiet(int id,thongtinHoatdong model)
        {
            int? iduser = HttpContext.Session.GetInt32("UserID");
            var tenban = db.YeucauTnvs.Where(n => n.Idhoatdong == id)
                .Select(n => n.Tenban).ToList();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
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
                              Tenban1 = tenban
                          }).FirstOrDefault();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            if (iduser == null)
            {
                ViewBag.thongbao = "Bạn chưa đăng nhập";
                return View(ketqua);
            }
            else return View(ketqua);
        }
    }
}