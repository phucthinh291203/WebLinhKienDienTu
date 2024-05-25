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
    public class CategorySvc : GenericSvc<CategoryRep, LoaiSp>
    {
        private CategoryRep categoryRep;
        public CategorySvc()
        {
            categoryRep = new CategoryRep();
        }

        #region -- Override --
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }

        public override SingleRsp Update(LoaiSp m)
        {
            var res = new SingleRsp();

            var m1 = m.Id > 0 ? _rep.Read(m.Id) : _rep.Read(m.Ten);
            if (m1 == null)
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
        #endregion


        public SingleRsp SearchCategory(SearchCateByNameReq search)
        {
            var res = new SingleRsp();
            //lay dssp theo keyword
            var cates = categoryRep.SearchCategory(search.Keyword);
            res.Data = cates;
            return res;
        }

        public SingleRsp Remove(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Remove(id);
            return res;

        }

        public SingleRsp CreateCategory(CategoryReq loaiSpReq)
        {
            var res = new SingleRsp();

            LoaiSp l = new LoaiSp();
            l.Ten = loaiSpReq.TenLoaiSp;
            res = categoryRep.CreateCategory(l);
            return res;
        }

        public SingleRsp UpdateCategory(int Id, CategoryReq loaiSpReq)
        {
            var res = new SingleRsp();
            var existingCustomer = categoryRep.Read(Id);
            if (existingCustomer == null)
            {
                res.SetError("Customer not found.");
                return res;
            }
            LoaiSp l = new LoaiSp();
            l.Ten = loaiSpReq.TenLoaiSp;
            res = categoryRep.UpdateCategory(l);
            return res;
        }
    }
}
