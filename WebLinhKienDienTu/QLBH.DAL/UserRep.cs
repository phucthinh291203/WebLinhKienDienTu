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
    public class UserRep : GenericRep<WebDienTuContext, Khachhang>
    {
        private WebDienTuContext _context = new WebDienTuContext();

        public override Khachhang Read(int id)
        {
            var res = All.FirstOrDefault(u => u.MaKh == id);
            return res;
        }

        public Khachhang Read(string username)
        {
            var res = All.FirstOrDefault(u => u.Taikhoan == username);
            return res;
        }
        public Khachhang GetById(int id)
        {
            return _context.Khachhangs.Find(id);
        }
        public Khachhang GetByUserName(string username)
        {
            return _context.Khachhangs.FirstOrDefault(u => u.Taikhoan == username);
        }

        public SingleRsp UpdateUser(Khachhang user)
        {
            var res = new SingleRsp();
            try
            {
                _context.Khachhangs.Update(user);
                _context.SaveChanges();
                res.Data = user;
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }
            return res;
        }

        public void Delete(int id)
        {
            var user = _context.Khachhangs.Find(id);
            if (user != null)
            {
                _context.Khachhangs.Remove(user);
                _context.SaveChanges();
            }
        }
        public void DeleteByUserName(string username)
        {
            var user = _context.Khachhangs.FirstOrDefault(u => u.Taikhoan == username);
            if (user != null)
            {
                _context.Khachhangs.Remove(user);
                _context.SaveChanges();
            }
        }

        public SingleRsp CreateUser(Khachhang user)
        {
            var res = new SingleRsp();
            using (var context = new WebDienTuContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Khachhangs.Add(user);
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
            return _context.Khachhangs.Any(u => u.Taikhoan == username && u.MaKh != id);
        }
    }
}
