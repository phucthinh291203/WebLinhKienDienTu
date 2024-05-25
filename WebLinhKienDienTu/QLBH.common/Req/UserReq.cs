using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.common.Req
{
    public class UserReq
    {
		public string Ten { get; set; } = null!;
		public string? UserName { get; set; }
		public string? Pass { get; set; }
		public int? IdRole { get; set; }
	}
}
