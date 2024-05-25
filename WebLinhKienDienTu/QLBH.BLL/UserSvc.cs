using QLBH.common.BLL;
using QLBH.common.Req;
using QLBH.common.Rsp;
using QLBH.DAL;
using QLBH.DAL.Models;

namespace QLBH.BLL
{
    public class UserSvc : GenericSvc<UserRep, TaiKhoan>
    {
        private UserRep userRep;

        public UserSvc()
        {
            userRep = new UserRep();
        }

        public SingleRsp Remove(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Remove(id);
            return res;
        }
        public void DeleteUserByUserName(string username)
        {
            userRep.DeleteByUserName(username);
        }

        public TaiKhoan GetUserById(int id)
        {
            return userRep.GetById(id);
        }
        public TaiKhoan GetUserByUserName(string username)
        {
            return userRep.GetByUserName(username);
        }

        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            if (string.IsNullOrWhiteSpace(userReq.UserName))
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            var existingUser = userRep.GetByUserName(userReq.UserName);

            if (existingUser != null)
            {
                res.SetError("Username already exists.");
                return res;
            }

            var user = new TaiKhoan
            {
                UserName = userReq.UserName,
                Pass = userReq.Pass,
                Ten = userReq.Ten,
                IdRole = 2

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
            if (!string.IsNullOrEmpty(userReq.UserName) && string.IsNullOrWhiteSpace(userReq.UserName))
            {
                res.SetError("Tai khoan khong duoc de khoang trong");
                return res;
            }
            if (userRep.ExistsUserName(userReq.UserName, id))
            {
                res.SetError("Tai khoan da ton tai");
                return res;
            }

            existingUser.UserName = userReq.UserName ?? existingUser.UserName;
            if (!string.IsNullOrEmpty(userReq.Pass))
            {
                existingUser.Pass = userReq.Pass;
            }
            existingUser.Ten = userReq.Ten ?? existingUser.Ten;


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
            if (!string.IsNullOrEmpty(userReq.UserName) && string.IsNullOrWhiteSpace(userReq.UserName)) // Kiem tra khoang trang
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            if (userRep.ExistsUserName(userReq.UserName, existingUser.Id))
            {
                res.SetError("Username already exists.");
                return res;
            }

            existingUser.UserName = userReq.UserName ?? existingUser.UserName;
            if (!string.IsNullOrEmpty(userReq.Pass))
            {
                existingUser.Pass = userReq.Pass;
            }
            existingUser.Ten = userReq.Ten ?? existingUser.Ten;


            return userRep.UpdateUser(existingUser);
        }


        public SingleRsp UpdateUserRoleByUserName(string username, int role)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetByUserName(username);
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }

            existingUser.IdRole = role;
            return userRep.UpdateUser(existingUser);
        }

        public SingleRsp AuthenticateUser(LoginReq loginReq)
        {
            var res = new SingleRsp();
            var user = userRep.Read(loginReq.TaiKhoan);

            if (user == null || user.Pass != loginReq.MatKhau)
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
