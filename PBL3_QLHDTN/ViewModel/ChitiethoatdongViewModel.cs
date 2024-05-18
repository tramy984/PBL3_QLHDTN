namespace PBL3_QLHDTN.ViewModel
{
    public class ChitiethoatdongViewModel
    {
        public int Idhoatdong { get; set; }
        public string? Tenhoatdong { get; set; }
        public DateOnly? Thoigianbatdau { get; set; }
        public DateOnly? Thoigiaketthuc { get; set; }
        public DateOnly? Tgbdchinhsua { get; set; }
        public DateOnly? Tgktchinhsua { get; set; }
        public string? Linhvuc { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Tinhtrang { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DateOnly? Thoigianhuy { get; set; }
        public string? Lydohuy { get; set; }
        public string? DiaDiem { get; set; }
        public string? MuctieuHd { get; set; }
        public string? Danhgiatutochuc { get; set; }
        public DateTime? Tgdanhgia1 { get; set; }
        public string? Danhgiacuatnv { get; set; }
        public DateTime? Tgdanhgia2 { get; set; }
    }
}
