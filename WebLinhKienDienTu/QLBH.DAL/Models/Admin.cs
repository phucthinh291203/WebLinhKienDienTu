using System;
using System.Collections.Generic;

namespace QLBH.DAL.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Dondathangs = new HashSet<Dondathang>();
        }

        public int MaAdmin { get; set; }
        public string? UserAdmin { get; set; }
        public string? PassAdmin { get; set; }
        public string? HoTen { get; set; }

        public virtual ICollection<Dondathang> Dondathangs { get; set; }
    }
}
