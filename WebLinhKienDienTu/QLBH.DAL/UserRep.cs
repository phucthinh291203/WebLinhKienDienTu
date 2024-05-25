using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLBH.common.DAL;
using QLBH.common.Rsp;
using QLBH.DAL.Models;

namespace QLBH.DAL
{
    public class UserRep : GenericRep<WebDienTu15Context, TaiKhoan>
    {
        private WebDienTu15Context _context = new WebDienTu15Context();

        public override TaiKhoan Read(int id)
        {
            var res = All.FirstOrDefault(u => u.Id == id);
            return res;
        }
        public int Remove(int id)
        {
            var m = base.All.First(i => i.Id == id);
            m = base.Delete(m);
            return m.Id;
        }
        public TaiKhoan Read(string username)
        {
            var res = All.FirstOrDefault(u => u.UserName == username);
            return res;
        }
        public TaiKhoan GetById(int id)
        {
            return _context.TaiKhoans.Find(id);
        }
        public TaiKhoan GetByUserName(string username)
        {
            return _context.TaiKhoans.FirstOrDefault(u => u.UserName == username);
        }

        public SingleRsp UpdateUser(TaiKhoan user)
        {
            var res = new SingleRsp();
            try
            {
                _context.TaiKhoans.Update(user);
                _context.SaveChanges();
                res.Data = user;
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }
            return res;
        }
        public void DeleteByUserName(string username)
        {
            var user = _context.TaiKhoans.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                _context.TaiKhoans.Remove(user);
                _context.SaveChanges();
            }
        }

        public SingleRsp CreateUser(TaiKhoan user)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTu15Context())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.TaiKhoans.Add(user);
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
        public bool ExistsUserName(string username, int id)
        {
            return _context.TaiKhoans.Any(u => u.UserName == username && u.Id != id);
        }
    }
}
