using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBH.common.DAL;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
    public class CategoryRep:GenericRep<WebDienTuContext,LoaiSp>
    {
        public CategoryRep()
        {
        }

        public override LoaiSp Read(int id)
        {
            var res = All.FirstOrDefault(c => c.MaLoaiSp == id);
            return res;
        }
    }
}
