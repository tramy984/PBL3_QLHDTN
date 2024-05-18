using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.ViewModel
{
    public class thongtinCanhan
    {
        public int Idcanhan { get; set; }

        public string? Ten { get; set; }

        public string? Sdt { get; set; }

        public string? Diachi { get; set; }

        public string? Email { get; set; }

        public bool? Gioitinh { get; set; }

        public int? Namsinh { get; set; }
        public bool? Trangthai { get; set; }

        public List<QuanlyTghd> dsHoatdong { get; set; }
    }
}
