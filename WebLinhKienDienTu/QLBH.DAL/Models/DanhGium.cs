using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class DanhGium
    {
        public int MaDanhGia { get; set; }
        public int? MaSp { get; set; }
        public int? MaKh { get; set; }
        public decimal? DiemDanhGia { get; set; }
        public string? NhanXet { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual Khachhang? MaKhNavigation { get; set; }
        public virtual Sanpham? MaSpNavigation { get; set; }
    }
}
