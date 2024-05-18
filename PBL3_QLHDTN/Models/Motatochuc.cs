using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Motatochuc
{
    public int Idtochuc { get; set; }

    public string? Gioithieu { get; set; }

    public string? Thanhtuu { get; set; }

    public virtual Tochuc IdtochucNavigation { get; set; } = null!;
}
