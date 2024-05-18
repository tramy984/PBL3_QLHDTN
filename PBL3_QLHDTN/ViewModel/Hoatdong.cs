namespace PBL3_QLHDTN.ViewModel
{
    public class Hoatdong
    {
        public int Idhoatdong { get; set; }

        public int Idtochuc { get; set; }

        public int? Sltnvcan { get; set; }

        public string? Trangthai { get; set; }

        public string? Tenhoatdong { get; set; }

        public DateOnly? Thoigianbatdau { get; set; }

        public DateOnly? Thoigiaketthuc { get; set; }
        public DateOnly? tgktdk { get; set; }

        public DateOnly? Tgbdchinhsua { get; set; }

        public DateOnly? Tgktchinhsua { get; set; }

        public int? Linhvuc { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string TenLV {  get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DateOnly? Thoigianhuy { get; set; }

        public string? Lydohuy { get; set; }

        public string? DiaDiem { get; set; }

        public string? MuctieuHd { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static List<string> lv { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
