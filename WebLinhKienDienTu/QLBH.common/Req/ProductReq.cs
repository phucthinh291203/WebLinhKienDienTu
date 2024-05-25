using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBH.common.Req
{
    public class ProductReq
    {
		public int Id { get; set; }
		public string Ten { get; set; } = null!;
		public string? MoTa { get; set; }
		public double? GiaBan { get; set; }
		public double? GiaGiam { get; set; }
		public int? SoLuongTon { get; set; }
		public string? HinhAnh { get; set; }
		public int? IdLsp { get; set; }
		public int? IdNcc { get; set; }
	}
}
