using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Taikhoan
{
    public int Id { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Matkhau { get; set; }

    public bool? Trangthai { get; set; }

    public int? Vaitro { get; set; }

    public virtual Canhan? Canhan { get; set; }

    public virtual Tochuc? Tochuc { get; set; }

    public virtual Vaitro? VaitroNavigation { get; set; }
}
