using QLBH.common.DAL;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
    public class ProductRep : GenericRep<WebDienTuContext, Sanpham>
    {
        public ProductRep()
        {

        }
        #region -- override --
        public override Sanpham Read(int id)
        {
            var res = All.FirstOrDefault(p => p.MaSp == id);
            return res;
        }

        public int Remove(int id)
        {
            var m = base.All.First(i => i.MaSp == id);
            m = base.Delete(m);
            return m.MaSp;
        }

        #endregion


        #region -- method --
        public SingleRsp CreateProduct(Sanpham _sanpham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Sanphams.Add(_sanpham);
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


        public List<Sanpham> SearchProduct(string keyword)
        {
            return All.Where(x => x.TenSp.Contains(keyword)).ToList();

        }

        public SingleRsp UpdateProduct(Sanpham sanPham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTuContext())
            {

                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.Sanphams.Update(sanPham);
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
        #endregion
    }
}
