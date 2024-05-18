using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Hoatdong
{
    public int Idhoatdong { get; set; }

    public int? Idtochuc { get; set; }

    public int? Sltnvcan { get; set; }

    public int? Trangthai { get; set; }

    public virtual ICollection<DanhgiaHd> DanhgiaHds { get; set; } = new List<DanhgiaHd>();

    public virtual ICollection<DanhgiaTnv> DanhgiaTnvs { get; set; } = new List<DanhgiaTnv>();

    public virtual ICollection<Hdyeuthich> Hdyeuthiches { get; set; } = new List<Hdyeuthich>();

    public virtual Tochuc? IdtochucNavigation { get; set; }

    public virtual MotaHd? MotaHd { get; set; }

    public virtual ICollection<QuanlyTghd> QuanlyTghds { get; set; } = new List<QuanlyTghd>();

    public virtual TrangthaiHd? TrangthaiNavigation { get; set; }

    public virtual ICollection<YeucauTnv> YeucauTnvs { get; set; } = new List<YeucauTnv>();
}
