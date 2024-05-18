using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class MotaHd
{
    public int Idhoatdong { get; set; }

    public string? Tenhoatdong { get; set; }

    public DateOnly? Thoigianbatdau { get; set; }

    public DateOnly? Thoigiaketthuc { get; set; }

    public DateOnly? Tgbdchinhsua { get; set; }

    public DateOnly? Tgktchinhsua { get; set; }

    public int? Linhvuc { get; set; }

    public DateOnly? Thoigianhuy { get; set; }

    public string? Lydohuy { get; set; }

    public string? DiaDiem { get; set; }

    public string? MuctieuHd { get; set; }

    public DateOnly? Thoigianketthucdk { get; set; }

    public virtual Hoatdong IdhoatdongNavigation { get; set; } = null!;

    public virtual Linhvuc? LinhvucNavigation { get; set; }
}
