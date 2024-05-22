using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class LoaiSp
    {
        public LoaiSp()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int MaLoaiSp { get; set; }
        public string TenLoaiSp { get; set; } = null!;

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
