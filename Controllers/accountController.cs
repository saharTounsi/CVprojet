using APIcv.Dto.Account;
using APIcv.interfaces;
using APIcv.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static APIcv.Dto.Account.RegisterDto;

namespace APIcv.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class accountController : ControllerBase
    {
            private readonly UserManager<User> _userManager;
            private readonly ItokenService _tokenService;
            private readonly SignInManager<User> _signInManager;
            public accountController(UserManager<User> userManager, ItokenService tokenService, SignInManager<User> signInManager)
            {
                _userManager = userManager;
                _tokenService = tokenService;
                _signInManager = signInManager;
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login(LoginDto loginDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

                if (user == null) return Unauthorized("Invalid username!!");

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!result.Succeeded) return Unauthorized("username not found and/or password incorrect");

                return Ok(
                    new newUserDto
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)

                    }
                    );

            }



            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] ResisterDto registerDto)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    var user = new User
                    {
                        UserName = registerDto.Username,
                        firstName = registerDto.firstName,
                        lastName = registerDto.lastName,
                        Email = registerDto.Email,
                    };

                    var createdUser = await _userManager.CreateAsync(user, registerDto.Password);

                    if (createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Employee");
                        if (roleResult.Succeeded)
                        {
                            return Ok(
                                new newUserDto
                                {
                                    UserName = user.UserName,
                                    Email = user.Email,
                                    Token = _tokenService.CreateToken(user)
                                }
                                );
                        }
                        else
                        {
                            return StatusCode(500, roleResult.Errors);

                        }
                    }
                    else
                    {
                        return StatusCode(500, createdUser.Errors);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500, e);
                }
            }
    }
}
