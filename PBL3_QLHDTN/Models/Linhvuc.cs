using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Linhvuc
{
    public int Idlinhvuc { get; set; }

    public string? Linhvuc1 { get; set; }

    public byte[]? Anh { get; set; }

    public virtual ICollection<MotaHd> MotaHds { get; set; } = new List<MotaHd>();
}
