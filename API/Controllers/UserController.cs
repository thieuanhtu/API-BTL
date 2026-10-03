using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;

        public UserController(IUserBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] UserModel model)
        {
            var result = _userBusiness.Create(model);
            return Ok(new { success = result });
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] UserModel model)
        {
            var result = _userBusiness.Update(model);
            return Ok(new { success = result });
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetDatabyID(string id)
        {
            var data = _userBusiness.GetDatabyID(id);
            return Ok(data);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _userBusiness.Delete(id);
            return Ok(new { success = result });
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                int pageIndex = int.Parse(formData["pageIndex"].ToString());
                int pageSize = int.Parse(formData["pageSize"].ToString());
                string hoten = formData.Keys.Contains("hoten") ? Convert.ToString(formData["hoten"]) : null;
                string taikhoan = formData.Keys.Contains("taikhoan") ? Convert.ToString(formData["taikhoan"]) : null;

                long total = 0;
                var data = _userBusiness.Search(pageIndex, pageSize, out total, hoten, taikhoan);
                return Ok(new { TotalItems = total, Data = data, PageIndex = pageIndex, PageSize = pageSize });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userBusiness.Authenticate(model.taikhoan, model.matkhau);
            if (user == null || string.IsNullOrEmpty(user.user_id))
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });

            // Sinh JWT token
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.user_id),
        new Claim(ClaimTypes.Name, user.taikhoan),
        new Claim(ClaimTypes.Role, user.role ?? "")
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            user.token = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(user);
        }

    }
}