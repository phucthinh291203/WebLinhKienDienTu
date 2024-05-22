using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class Dondathang
    {
        public Dondathang()
        {
            Chitietdonthangs = new HashSet<Chitietdonthang>();
        }

        public int MaDonHang { get; set; }
        public bool? Dathanhtoan { get; set; }
        public bool? Tinhtranggiaohang { get; set; }
        public DateTime? Ngaydat { get; set; }
        public DateTime? Ngaygiao { get; set; }
        public int? MaKh { get; set; }
        public int? MaAdmin { get; set; }

        public virtual Admin? MaAdminNavigation { get; set; }
        public virtual Khachhang? MaKhNavigation { get; set; }
        public virtual ICollection<Chitietdonthang> Chitietdonthangs { get; set; }
    }
}
