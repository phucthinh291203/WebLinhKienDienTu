using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLBH.BLL;
using QLBH.DAL.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Jose;
using Microsoft.EntityFrameworkCore;
using QLBH.common.Req;
using QLBH.common.Rsp;
using System.Security.Claims;
using System.Text;

namespace WebLinhKienDienTu15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WebDienTuContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly UserSvc _userSvc;

        public UserController(WebDienTuContext context, IOptionsMonitor<JwtSettings> optionsMonitor, UserSvc userSvc)
        {
            _context = context;
            _jwtSettings = optionsMonitor.CurrentValue;
            _userSvc = userSvc;
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginReq model)
        {
            var res = _userSvc.AuthenticateUser(model);
            if (!res.Success)
            {
                res.SetMessage("Invalid username/password");
                return Ok(res);
            }

            var user = (Khachhang)res.Data;
            var token = GenerateToken(user);
            var response = new SingleRsp();
            response.Data = token;
            response.SetMessage("Authenticate success");
            return Ok(response);
        }

        [HttpPost("Register")]
        public IActionResult Register(UserReq model)
        {
            var res = _userSvc.CreateUser(model);
            if (!res.Success)
            {
                return Ok(res);
            }
            res.SetMessage("User registered successfully");
            res.Data = model;
            return Ok(res);
        }
        [HttpGet("GetAllUser")]
        public IActionResult GetAllUsersExceptAdmin()
        {
            var res = _userSvc.GetAllUsersExceptAdmin();
            return Ok(res);
        }
        [HttpGet("SeachByusername")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = _userSvc.GetUserByUserName(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPut("UpdateById")]
        public IActionResult UpdateUser(int id, [FromBody] UserReq userReq)
        {
            if (userReq == null)
            {
                return BadRequest("Invalid user data.");
            }

            var res = _userSvc.UpdateUser(id, userReq);
            res.Data = userReq;
            return Ok(res);

        }

        [HttpPut("UpdateByUsername")]
        public IActionResult UpdateUserByUsername(string username, [FromBody] UserReq userReq)
        {
            if (userReq == null)
            {
                return BadRequest("Invalid user data.");
            }

            var res = _userSvc.UpdateUserByUserName(username, userReq);
            res.Data = userReq;
            return Ok(res);
        }
        [HttpPut("UpdateUserRoleByUsername")]
        public IActionResult UpdateUserRoleByUsername(string username, [FromBody] string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return BadRequest("Invalid role.");
            }

            var res = _userSvc.UpdateUserRoleByUserName(username, role);
            return Ok(res);
        }


        [HttpDelete("DeleteUserById/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                _userSvc.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteByUsername")]
        public IActionResult DeleteUserByUsername(string username)
        {
            try
            {
                _userSvc.DeleteUserByUserName(username);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private string GenerateToken(Khachhang user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.securitykey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim("UserName", user.Taikhoan),
                    new Claim("Id", user.MaKh.ToString()),
                    new Claim("Role", user.Role),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }

    
}
