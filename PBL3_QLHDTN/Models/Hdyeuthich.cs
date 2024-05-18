﻿using System;
using System.Collections.Generic;

namespace PBL3_QLHDTN.Models;

public partial class Hdyeuthich
{
    public int Id { get; set; }

    public int? Idcanhan { get; set; }

    public int? Idhoatdong { get; set; }

    public virtual Canhan? IdcanhanNavigation { get; set; }

    public virtual Hoatdong? IdhoatdongNavigation { get; set; }
}
