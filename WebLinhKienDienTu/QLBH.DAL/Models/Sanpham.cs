using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Chitietdonthangs = new HashSet<Chitietdonthang>();
            DanhGia = new HashSet<DanhGium>();
        }

        public int MaSp { get; set; }
        public string TenSp { get; set; } = null!;
        public decimal? Giaban { get; set; }
        public string? AnhSp { get; set; }
        public int? Soluongton { get; set; }
        public int? MaLoai { get; set; }
        public int? MaNcc { get; set; }

        public virtual LoaiSp? MaLoaiNavigation { get; set; }
        public virtual Nhacungcap? MaNccNavigation { get; set; }
        public virtual ICollection<Chitietdonthang> Chitietdonthangs { get; set; }
        public virtual ICollection<DanhGium> DanhGia { get; set; }
    }
}
