using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using StorageSystem.WebAPI.ViewModel.AuthViewModel;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StorageSystem.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var expiresAt = DateTime.UtcNow.AddMinutes(10);


                return Ok(new
                {
                    access_token = CreateToken(authClaims, expiresAt),
                    expires_at = expiresAt
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        //[HttpPost]
        //public IActionResult Authenticate([FromBody] Credential credential)
        //{
        //    Console.WriteLine("vao");
        //    if(credential.UserName == "admin" && credential.Password == "admin123") {
        //        var claims = new List<Claim> {
        //            new Claim(ClaimTypes.Name, "admin"),
        //            new Claim(ClaimTypes.Email, "admin@gmail.com"),
        //            new Claim("Department", "HR"),
        //            new Claim("Admin", "true"),
        //            new Claim("Manager", "true"),
        //            new Claim("EmploymentDate", "2021-02-01")
        //        };

        //        var expiresAt = DateTime.UtcNow.AddMinutes(10);

        //        return Ok(new
        //        {
        //            access_token = CreateToken(claims, expiresAt),
        //            expires_at = expiresAt
        //        });
        //    }

        //    ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint.");
        //    return Unauthorized(ModelState);
        //}

        private string CreateToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
            var secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey"));

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireAt,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


    }

    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }    
    }
}
