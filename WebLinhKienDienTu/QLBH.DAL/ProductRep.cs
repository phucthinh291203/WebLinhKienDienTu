using QLBH.common.DAL;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
    public class ProductRep : GenericRep<WebDienTu15Context, SanPham>
    {
        public ProductRep()
        {

        }
        #region -- override --
        public override SanPham Read(int id)
        {
            var res = All.FirstOrDefault(p => p.Id == id);
            return res;
        }

        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m);
            return m.Id;
        }

        #endregion


        #region -- method --
        public SingleRsp CreateProduct(SanPham _sanpham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTu15Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.SanPhams.Add(_sanpham);
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


        public List<SanPham> SearchProduct(string keyword)
        {
            return All.Where(x => x.Ten.Contains(keyword)).ToList();

        }

        public SingleRsp UpdateProduct(SanPham sanPham)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTu15Context())
            {

                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.SanPhams.Update(sanPham);
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
