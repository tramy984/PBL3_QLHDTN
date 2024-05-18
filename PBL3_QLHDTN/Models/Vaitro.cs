using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Vaitro
{
    public int Idvaitro { get; set; }

    public string? Vaitro1 { get; set; }

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
