using PBL3_QLHDTN.Models;

namespace PBL3_QLHDTN.ViewModel
{
    public class LinhvucView
    {
        public int Idlinhvuc { get; set; }

        public string? Linhvuc1 { get; set; }

        public byte[]? Anh { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public List<string> TenLV { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
