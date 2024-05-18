using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class QuanlyTghd
{
    public int Id { get; set; }

    public int? Idcanhan { get; set; }

    public int? Idhoatdong { get; set; }

    public DateOnly? Thoigiandangky { get; set; }

    public string? Tenban { get; set; }

    public string? Uudiem { get; set; }

    public string? Nhuocdiem { get; set; }

    public bool? Trangthaiduyetdon { get; set; }

    public bool? Tinhtrangthamgia { get; set; }

    public bool? Tinhtranghuy { get; set; }

    public virtual Canhan? IdcanhanNavigation { get; set; }

    public virtual Hoatdong? IdhoatdongNavigation { get; set; }
}
