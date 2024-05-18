using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Canhan
{
    public int Idcanhan { get; set; }

    public string? Ten { get; set; }

    public string? Sdt { get; set; }

    public string? Diachi { get; set; }

    public string? Email { get; set; }

    public bool? Gioitinh { get; set; }

    public int? Namsinh { get; set; }

    public virtual ICollection<Baocao> BaocaoIdtkbaocaoNavigations { get; set; } = new List<Baocao>();

    public virtual ICollection<Baocao> BaocaoIdtkbibaocaoNavigations { get; set; } = new List<Baocao>();

    public virtual ICollection<DanhgiaHd> DanhgiaHds { get; set; } = new List<DanhgiaHd>();

    public virtual ICollection<DanhgiaTnv> DanhgiaTnvs { get; set; } = new List<DanhgiaTnv>();

    public virtual ICollection<Hdyeuthich> Hdyeuthiches { get; set; } = new List<Hdyeuthich>();

    public virtual Taikhoan IdcanhanNavigation { get; set; } = null!;

    public virtual ICollection<QuanlyTghd> QuanlyTghds { get; set; } = new List<QuanlyTghd>();
}
