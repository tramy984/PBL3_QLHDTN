using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class YeucauTnv
{
    public int Id { get; set; }

    public int? Idhoatdong { get; set; }

    public string? Tenban { get; set; }

    public string? Nhiemvu { get; set; }

    public string? Yeucau { get; set; }

    public int? Sltnvcan { get; set; }

    public int? Sltnvdk { get; set; }

    public virtual Hoatdong? IdhoatdongNavigation { get; set; }
}
