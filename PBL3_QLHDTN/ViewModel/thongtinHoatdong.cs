using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.ViewModel
{
    public class thongtinHoatdong
    {
        public int Idhoatdong1 { get; set; }

        public string? Tenhoatdong1 { get; set; }

        public DateOnly? Thoigianbatdau1 { get; set; }

        public DateOnly? Thoigiaketthuc1 { get; set; }
        public DateOnly? Tgbddachinhsua {  get; set; }
        public DateOnly? Tgktdachinhsua { get; set; }
        public DateOnly? tgktdk { get; set; }

        public string? DiaDiem1 { get; set; }

        public string? MuctieuHd1 { get; set; }
        public string? Linhvuc11 { get; set; }
        public int? Sltnvcan1 { get; set; }

        public int? Trangthai1 { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string TenTrangthai {  get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public List<string> Tenban1 { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
