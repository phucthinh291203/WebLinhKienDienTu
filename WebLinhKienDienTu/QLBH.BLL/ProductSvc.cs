using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBH.common.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;
using QLBH.DAL;
using QLBH.DAL.Models;

namespace QLBH.BLL
{
    public class ProductSvc: GenericSvc<ProductRep, Sanpham>
    {
        private ProductRep productRep;

        #region -- Override --
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            var m = _rep.Read(id);
            res.Data = m;
            return res;
        }
        #endregion
        public ProductSvc()
        {
            productRep = new ProductRep();
        }

        public SingleRsp CreateProduct(ProductReq productReq)
        {
            var res = new SingleRsp();
            Sanpham sanpham = new Sanpham();
            sanpham.MaSp = productReq.MaSp;
            sanpham.TenSp = productReq.TenSp;
            sanpham.AnhSp = productReq.AnhSp;
            sanpham.Giaban = productReq.Giaban;
            sanpham.Soluongton = productReq.Soluongton;
            res = productRep.CreateProduct(sanpham);
            return res;
        }

        public override SingleRsp Update(Sanpham m)
        {
            var res = new SingleRsp();
            var m1 = m.MaSp > 0 ? _rep.Read(m.MaSp) : _rep.Read(m.TenSp);
            if(m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }
            return res;
        }

    }
}
