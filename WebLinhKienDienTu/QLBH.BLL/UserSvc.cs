using QLBH.common.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;
using QLBH.DAL;
using QLBH.DAL.Models;

namespace QLBH.BLL
{
    public class UserSvc : GenericSvc<UserRep, Khachhang>
    {
        private readonly UserRep userRep;

        public UserSvc()
        {
            userRep = new UserRep();
        }

        public void DeleteUser(int id)
        {
            userRep.Delete(id);
        }
        public void DeleteUserByUserName(string username)
        {
            userRep.DeleteByUserName(username);
        }

        public Khachhang GetUserById(int id)
        {
            return userRep.GetById(id);
        }
        public Khachhang GetUserByUserName(string username)
        {
            return userRep.GetByUserName(username);
        }

        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            if (string.IsNullOrWhiteSpace(userReq.Taikhoan))
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            var existingUser = userRep.GetByUserName(userReq.Taikhoan);

            if (existingUser != null)
            {
                res.SetError("Username already exists.");
                return res;
            }

            var user = new Khachhang
            {
                Taikhoan = userReq.Taikhoan,
                Matkhau = userReq.Matkhau,
                HoTen = userReq.HoTen,
                Email = userReq.Email,
                DienthoaiKh = userReq.DienthoaiKh,
                DiachiKh = userReq.DiachiKh,

            };

            return res = userRep.CreateUser(user);
        }

        public SingleRsp UpdateUser(int id, UserReq userReq)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetById(id);
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }
            if (!string.IsNullOrEmpty(userReq.Taikhoan) && string.IsNullOrWhiteSpace(userReq.Taikhoan))
            {
                res.SetError("Tai khoan khong duoc de khoang trong");
                return res;
            }
            if (userRep.ExistsUserName(userReq.Taikhoan, id))
            {
                res.SetError("Tai khoan da ton tai");
                return res;
            }

            existingUser.Taikhoan = userReq.Taikhoan ?? existingUser.Taikhoan;
            if (!string.IsNullOrEmpty(userReq.Matkhau))
            {
                existingUser.Matkhau = userReq.Matkhau;
            }
            existingUser.HoTen = userReq.HoTen ?? existingUser.HoTen;
            existingUser.Email = userReq.Email ?? existingUser.Email;
            existingUser.DienthoaiKh = userReq.DienthoaiKh ?? existingUser.DienthoaiKh;
            existingUser.DiachiKh = userReq.DiachiKh ?? existingUser.DiachiKh;

            return userRep.UpdateUser(existingUser);
        }


        public SingleRsp UpdateUserByUserName(string username, UserReq userReq)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetByUserName(username); // Get data cua 1 user
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }
            if (!string.IsNullOrEmpty(userReq.Taikhoan) && string.IsNullOrWhiteSpace(userReq.Taikhoan)) // Kiem tra khoang trang
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            if (userRep.ExistsUserName(userReq.Taikhoan, existingUser.MaKh))
            {
                res.SetError("Username already exists.");
                return res;
            }

            existingUser.Taikhoan = userReq.Taikhoan ?? existingUser.Taikhoan;
            if (!string.IsNullOrEmpty(userReq.Matkhau))
            {
                existingUser.Matkhau = userReq.Matkhau;
            }
            existingUser.HoTen = userReq.HoTen ?? existingUser.HoTen;
            existingUser.Email = userReq.Email ?? existingUser.Email;
            existingUser.DienthoaiKh = userReq.DienthoaiKh ?? existingUser.DienthoaiKh;
            existingUser.DiachiKh = userReq.DiachiKh ?? existingUser.DiachiKh;

            return userRep.UpdateUser(existingUser);
        }

        public SingleRsp AuthenticateUser(LoginReq loginReq)
        {
            var res = new SingleRsp();
            var user = userRep.Read(loginReq.TaiKhoan);

            if (user == null || user.Matkhau != loginReq.MatKhau)
            {
                res.SetError("Sai tai khoan hoac mat khau");
            }
            else
            {
                res.Data = user;
            }

            return res;
        }
    }
}
