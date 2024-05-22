using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.common.Req
{
    public class ProductReq
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = null!;
        public decimal? Giaban { get; set; }
        public string? AnhSp { get; set; }
        public int? Soluongton { get; set; }
        public int? MaLoai { get; set; }
        public int? MaNcc { get; set; }
    }
}
