using PBL3_QLHDTN.ViewModel;
using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.ViewModel
{
    public class thongtinTochuc
    {
        public int Idtochuc { get; set; }

        public string? Ten { get; set; }

        public string? Sdt { get; set; }

        public string? Email { get; set; }

        public string? Diachi { get; set; }
        public string? Gioithieu { get; set; }

        public string? Thanhtuu { get; set; }
        public int? soluonghd { get; set; }
        public int? soluongtnv { get; set; }
        public bool? Trangthai { get; set; }
        public List<thongtinHoatdong> Hoatdong { get; set; }

    }
}
