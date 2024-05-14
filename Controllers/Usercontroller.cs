using APIcv.Dto;
using APIcv.interfaces;
using APIcv.Models;
using APIcv.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APIcv.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Usercontroller : Controller
    {
        private readonly IUserRep _userRep;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public Usercontroller(IUserRep userRep, IMapper mapper,  UserManager<User> userManager)
        {
            _userRep = userRep;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]

        public IActionResult GetUsers()
        {
            var Users = _mapper.Map<List<UserDto>>(_userRep.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Users);
        }


        [HttpGet("{IDUser}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int IDUser)
        {
            if (!_userRep.UserEXISTS(IDUser))
                return NotFound();

            var User = _mapper.Map<UserDto>(_userRep.GetUser(IDUser));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(User);

        }
        [HttpPost]
        /*[Authorize(Roles = "User")]*/
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromQuery] int IDUser, [FromBody] UserDto UserCreate)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (UserCreate == null)
                return BadRequest(ModelState);

            var User = _userRep.GetUsers()
                .Where(c => c.Login.Trim().ToUpper() == UserCreate.Login.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(User != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(UserCreate);
            if (!_userRep.CreateUser(userMap))
            {
                ModelState.AddModelError("", "something went wrong while savin");
            }
            return Ok("successfully created");

        }
       
    }
}
