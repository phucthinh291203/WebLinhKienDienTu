using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.common.Req
{
    public class LoginReq
    {
        [Required]
        [MaxLength(50)]
        public string TaiKhoan { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string MatKhau { get; set; } = null!;
    }
}
