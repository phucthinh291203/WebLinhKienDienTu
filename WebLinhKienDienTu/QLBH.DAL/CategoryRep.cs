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

        public int Remove(int id)
        {
            var m = base.All.First(i => i.MaLoaiSp == id);
            m = base.Delete(m);
            return m.MaLoaiSp;
        }

        public List<LoaiSp> SearchCategory(string keyword)
        {
            return All.Where(x => x.TenLoaiSp.Contains(keyword)).ToList();
        }

        public SingleRsp CreateCategory(LoaiSp loaiSanPham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTuContext())
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
            using (var context = new WebDienTuContext())
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
