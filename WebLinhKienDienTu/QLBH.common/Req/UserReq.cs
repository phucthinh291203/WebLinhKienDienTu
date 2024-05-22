using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.common.Req
{
    public class UserReq
    {
        public int MaKh { get; set; }
        public string HoTen { get; set; } = null!;
        public string? Taikhoan { get; set; }
        public string Matkhau { get; set; } = null!;
        public string? Email { get; set; }
        public string? DiachiKh { get; set; }
        public string? DienthoaiKh { get; set; }
        public DateTime? Ngaysinh { get; set; }
    }
}
