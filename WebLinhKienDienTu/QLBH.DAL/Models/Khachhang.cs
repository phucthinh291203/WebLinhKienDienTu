using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            DanhGia = new HashSet<DanhGium>();
            Dondathangs = new HashSet<Dondathang>();
        }

        public int MaKh { get; set; }
        public string HoTen { get; set; } = null!;
        public string? Taikhoan { get; set; }
        public string Matkhau { get; set; } = null!;
        public string? Email { get; set; }
        public string? DiachiKh { get; set; }
        public string? DienthoaiKh { get; set; }
        public DateTime? Ngaysinh { get; set; }

        public virtual ICollection<DanhGium> DanhGia { get; set; }
        public virtual ICollection<Dondathang> Dondathangs { get; set; }
    }
}
