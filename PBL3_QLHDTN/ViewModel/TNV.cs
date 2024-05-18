namespace PBL3_QLHDTN.ViewModel
{
    public class TNV
    {
        public int Idcanhan { get; set; }

        public string? Ten { get; set; }

        public string? Sdt { get; set; }

        public string? Diachi { get; set; }

        public string? Email { get; set; }

        public bool? Gioitinh { get; set; }

        public int? Namsinh { get; set; }
        public int? Idhoatdong { get; set; }

        public DateOnly? Thoigiandangky { get; set; }

        public string? Tenban { get; set; }

        public string? Uudiem { get; set; }

        public string? Nhuocdiem { get; set; }

        public bool? Trangthaiduyetdon { get; set; }

        public bool? Tinhtrangthamgia { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string tinhtranghd {  get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
