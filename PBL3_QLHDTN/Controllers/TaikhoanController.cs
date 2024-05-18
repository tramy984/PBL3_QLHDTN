using Microsoft.AspNetCore.Mvc;
using PBL3_QLHDTN.Models;
using PBL3_QLHDTN.ViewModel;

namespace PBL3_QLHDTN.Controllers
{
    public class TaikhoanController : Controller
    {
        public int ID;
        QlhdtnContext db = new QlhdtnContext();
        public IActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Dangnhap(Taikhoan user)
        {
            if (HttpContext.Session.GetString("Tendangnhap") == null)
            {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var u = db.Taikhoans
                 .Where(model => model.Tendangnhap.Equals(user.Tendangnhap) && model.Matkhau.Equals(user.Matkhau))
                 .Select(model => new { Id = model.Id, Vaitro = model.Vaitro, Trangthai = model.Trangthai })
                 .FirstOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (u == null)
                {
                    ViewBag.thongbao = "Thông tin không chính xác !";
                    return View(user);
                }
                else
                {
                    if (u.Trangthai == false)
                    {
                        ViewBag.thongbao = "Tài khoản đã bị khoá.";
                        return View(user);
                    }
                    else
                    {
                        if (u.Vaitro == 1)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                            return Redirect("/Canhan/Trangchucanhan");
                        } else
                        if (u.Vaitro == 2)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                           return Redirect("/Tochuc/Trangchutochuc");
                        } else
                        if (u.Vaitro == 0)
                        {
                            HttpContext.Session.SetInt32("UserID", u.Id);
                            return Redirect("/Quantrivien/Trangchuquantrivien");
                        } else return View();

                    }                   
                }              
            }
            else return View();
        }
        [HttpGet]
        public ActionResult Dangkytaikhoan()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Dangkytaikhoan(DKTK model, string myComboBox)
        {
            // Kiểm tra xem tên đăng nhập đã tồn tại chưa
            var existingUser = db.Taikhoans.FirstOrDefault(u => u.Tendangnhap == model.Tendangnhap);
            if (existingUser != null)
            {
                ModelState.AddModelError("Tendangnhap", "Tên đăng nhập đã tồn tại.");
                return View(model);
            }

            // Kiểm tra xác nhận mật khẩu
            if (model.Matkhau != model.NLMK)
            {
                ModelState.AddModelError("XacnhanMatkhau", "Xác nhận mật khẩu không khớp.");
                return View(model);
            }

            // Thêm tài khoản mới vào cơ sở dữ liệu

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string Vaitro = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (myComboBox == "CN")
            {
                Vaitro = "Cá nhân";//Vaitro=1
            }
            else if (myComboBox == "TC")
            {
                Vaitro = "Tổ chức";//Vaitro=2
            }

            // Lưu thông tin số điện thoại và email nếu có
            if (!string.IsNullOrEmpty(model.Sdt) && !string.IsNullOrEmpty(model.Email))
            {
                if (Vaitro == "Cá nhân")
                {
                    var newUser = new Taikhoan
                    {
                        Tendangnhap = model.Tendangnhap,
                        Matkhau = model.Matkhau,
                        Trangthai = true,
                        Vaitro = 1
                    };
                    db.Taikhoans.Add(newUser);
                    db.SaveChanges();
                    var canhan = new Canhan
                    {
                        Idcanhan = newUser.Id,
                        Ten = model.Ten,
                        Sdt = model.Sdt,
                        Email = model.Email,

                    };
                    db.Canhans.Add(canhan);
                }
                if (Vaitro == "Tổ chức")
                {
                    var newUser = new Taikhoan
                    {
                        Tendangnhap = model.Tendangnhap,
                        Matkhau = model.Matkhau,
                        Trangthai = true,
                        Vaitro = 2
                    };
                    db.Taikhoans.Add(newUser);
                    db.SaveChanges();
                    var tochuc = new Tochuc
                    {
                        Idtochuc = newUser.Id,
                        Ten = model.Ten,
                        Sdt = model.Sdt,
                        Email = model.Email
                    };
                    db.Tochucs.Add(tochuc);
                }

            }

            db.SaveChanges();

            ViewBag.thongbao = "Đăng ký tài khoản thành công!";
            return Redirect("/Dangnhap/Dangnhap");
        }
        public IActionResult Doimatkhau()
        {
            
            return View();
        }
        [HttpPost] 
        public IActionResult Doimatkhau(string Mkcu,string matkhaumoi,string nhaplaimk)
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
           
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var u = db.Taikhoans
                 .Where(model => model.Matkhau.Equals(Mkcu) && model.Id.Equals(userID))
                 .FirstOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (Mkcu == null || matkhaumoi == null || nhaplaimk == null)
            {
                ViewBag.thongbao = "Vui lòng đầy đủ thông tin";
                return View();
            }
            else
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                Mkcu = Request.Form["Mkcu"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                matkhaumoi = Request.Form["matkhaumoi"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                nhaplaimk = Request.Form["nhaplaimk"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                if (u == null)
                {
                    ViewBag.thongbao = "Mật khẩu cũ bạn nhập đã sai";
                    return View();
                }
                else
                {
                    if (matkhaumoi != nhaplaimk)
                    {
                        ViewBag.thongbao = "Nhập lại mật khẩu không chính xác";
                     
                        return View();
                       
                    }
                    else
                    {
                        u.Matkhau = matkhaumoi;
                        db.Update(u);
                        db.SaveChanges();
                        return Redirect("Dangnhap");
                    }

                }
            }
        }
        public IActionResult Quenmatkhau()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Quenmatkhau(string TDN, string Email)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TDN = Request.Form["TDN"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Email = Request.Form["email"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            /*var u = (from tk in db.Taikhoans
                     join cn in db.Canhans on tk.Id equals cn.Idcanhan
                     join tc in db.Tochucs on tk.Id equals tc.Idtochuc
                     where tk.Tendangnhap == TDN &&((tc.Email == Email)||cn.Email==Email)
                     select new Taikhoan
                     {
                         Id = tk.Id,
                     }).FirstOrDefault();*/
            var taikhoan = db.Taikhoans.FirstOrDefault(tk => tk.Tendangnhap == TDN);
            if (string.IsNullOrEmpty(TDN) == true || string.IsNullOrEmpty(Email) == true)
            {
                ViewBag.thongbao = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            else {
                if (taikhoan == null)
                {
                    ViewBag.thongbao = "Bạn đã nhập sai thông tin";
                    return View();
                }
                else
                {
                    var canhanExists = db.Canhans.Any(cn => cn.Email == Email && cn.Idcanhan == taikhoan.Id);
                    var tochucExists = db.Tochucs.Any(tc => tc.Email == Email && tc.Idtochuc == taikhoan.Id);
                    if (!canhanExists || !tochucExists)
                    {
                        return RedirectToAction("Nhapmkmoi", new { idtk = taikhoan.Id });
                    }
                    else
                    {
                        ViewBag.thongbao = "Bạn đã nhập sai thông tin";
                        return View();
                    }

                }
            }
            
        }
        public IActionResult Nhapmkmoi(int idtk)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Nhapmkmoi(int idtk, string mkmoi, string nlmkmoi)
        {
            
            var u = db.Taikhoans.FirstOrDefault(u => u.Id == idtk);
            if(string.IsNullOrEmpty(mkmoi) == true || string.IsNullOrEmpty(nlmkmoi) == true)
            {
                ViewBag.thongbao = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            else
            {
                mkmoi = Request.Form["mkmoi"];
                nlmkmoi = Request.Form["nlmkmoi"];
                if (nlmkmoi != mkmoi)
                {
                    ViewBag.thongbao = "Mật khẩu nhập lại không trùng";
                    return View();
                }
                else
                {
                    if (u == null) { return View(); }
                    else
                    {
                        u.Matkhau = mkmoi;
                        db.SaveChanges();
                        return Redirect("Dangnhap");
                    }
                }
            }
#pragma warning disable CS0162 // Unreachable code detected
            return View();
#pragma warning restore CS0162 // Unreachable code detected
        }
        public IActionResult Baocao(int idcn)
        {
            return View();
        }
        [HttpPost]

        public IActionResult Baocao(int idcn, string noidung)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            noidung = Request.Form["noidung"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            int? idtc = HttpContext.Session.GetInt32("UserID");

            var u = new Baocao
            {
                Idtkbaocao = idtc,
                Idtkbibaocao = idcn,
                Ndbaocao = noidung,
                Tgbaocao = DateTime.Now,
            };
            if (string.IsNullOrEmpty(noidung) == true)
            {
                ViewBag.thongbao = "Vui lòng nhập nội dung báo cáo";
                ViewBag.thongbao = idcn;
                return View();
            }
            else
            {
                db.Baocaos.Add(u);
                db.SaveChanges();
            }
            return View();
        }
    }
}
