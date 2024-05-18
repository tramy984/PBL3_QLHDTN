using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class TrangthaiHd
{
    public int Idtrangthai { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<Hoatdong> Hoatdongs { get; set; } = new List<Hoatdong>();
}
