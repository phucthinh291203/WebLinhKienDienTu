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

        public SingleRsp CreateProduct(ProductReq _productReq)
        {
            var res = new SingleRsp();
            Sanpham sanpham = new Sanpham();
            sanpham.MaSp = _productReq.MaSp;
            sanpham.TenSp = _productReq.TenSp;
            sanpham.AnhSp = _productReq.AnhSp;
            sanpham.Giaban = _productReq.Giaban;
            sanpham.Soluongton = _productReq.Soluongton;
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


        public SingleRsp UpdateProduct(int Id, ProductReq sanPhamReq)
        {
            var res = new SingleRsp();
            var existingCustomer = productRep.Read(Id);
            if (existingCustomer == null)
            {
                res.SetError("Customer not found.");
                return res;
            }
            Sanpham sanPham = new Sanpham();
            sanPham.TenSp = sanPhamReq.TenSp;
            sanPham.Giaban = sanPhamReq.Giaban;
            sanPham.Soluongton = sanPhamReq.Soluongton;
            sanPham.AnhSp = sanPhamReq.AnhSp;
            return res = productRep.UpdateProduct(sanPham);
        }

        public SingleRsp SearchProduct(SearchProductReq search)
        {
            var res = new SingleRsp();
            //lay dssp theo keyword
            var sanPhams = productRep.SearchProduct(search.Keyword);

            //xu ly phan trang
            int pCount,totalPages, offset;
            offset = search.Size * (search.Page - 1);
            pCount = sanPhams.Count;
            totalPages = (pCount % search.Size) == 0? pCount / search.Size : 1 + (pCount / search.Size);
            var p = new
            {
                Data = sanPhams.Skip(offset).Take(search.Size).ToList(),
                Page = search.Page,
                Size = search.Size
            };
            res.Data = p;
            return res;
        }


    }
}
