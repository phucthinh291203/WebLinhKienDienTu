﻿using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            Ctdhs = new HashSet<Ctdh>();
            PhieuBaoHanhs = new HashSet<PhieuBaoHanh>();
        }

        public int Id { get; set; }
        public DateTime NgayDat { get; set; }
        public string? TrangThai { get; set; }
        public int? Slsp { get; set; }
        public int? IdKh { get; set; }

        public virtual KhachHang? IdKhNavigation { get; set; }
        public virtual ICollection<Ctdh> Ctdhs { get; set; }
        public virtual ICollection<PhieuBaoHanh> PhieuBaoHanhs { get; set; }
    }
}
