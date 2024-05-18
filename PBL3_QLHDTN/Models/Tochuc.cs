using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Tochuc
{
    public int Idtochuc { get; set; }

    public string? Ten { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public string? Diachi { get; set; }

    public virtual ICollection<Baocao> BaocaoIdtkbaocao1s { get; set; } = new List<Baocao>();

    public virtual ICollection<Baocao> BaocaoIdtkbibaocao1s { get; set; } = new List<Baocao>();

    public virtual ICollection<DanhgiaTnv> DanhgiaTnvs { get; set; } = new List<DanhgiaTnv>();

    public virtual ICollection<Hoatdong> Hoatdongs { get; set; } = new List<Hoatdong>();

    public virtual Taikhoan IdtochucNavigation { get; set; } = null!;

    public virtual Motatochuc? Motatochuc { get; set; }
}
