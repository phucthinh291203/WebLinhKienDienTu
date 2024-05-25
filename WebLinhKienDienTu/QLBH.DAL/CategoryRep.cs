using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBH.common.DAL;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
    public class CategoryRep : GenericRep<WebDienTu15Context, LoaiSp>
    {
        public CategoryRep()
        {
        }

        public override LoaiSp Read(int id)
        {
            var res = All.FirstOrDefault(c => c.Id == id);
            return res;
        }

        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m);
            return m.Id;
        }

        public List<LoaiSp> SearchCategory(string keyword)
        {
            return All.Where(x => x.Ten.Contains(keyword)).ToList();
        }

        public SingleRsp CreateCategory(LoaiSp loaiSanPham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTu15Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.LoaiSps.Add(loaiSanPham);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }


        public SingleRsp UpdateCategory(LoaiSp loaiSanPham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTu15Context())
            {

                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.LoaiSps.Update(loaiSanPham);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
    }
}
