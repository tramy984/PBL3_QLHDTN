using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Baocao
{
    public int Id { get; set; }

    public int? Idtkbaocao { get; set; }

    public int? Idtkbibaocao { get; set; }

    public DateTime? Tgbaocao { get; set; }

    public string? Ndbaocao { get; set; }

    public virtual Tochuc? Idtkbaocao1 { get; set; }

    public virtual Canhan? IdtkbaocaoNavigation { get; set; }

    public virtual Tochuc? Idtkbibaocao1 { get; set; }

    public virtual Canhan? IdtkbibaocaoNavigation { get; set; }
}
