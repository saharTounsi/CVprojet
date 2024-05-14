using APIcv.Dto;
using APIcv.interfaces;
using APIcv.Models;
using APIcv.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APIcv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMPLOYEEcontroller : Controller
    {
        private readonly IEMPLOYEERep _EMPLOYEERep;
        private readonly ICVEXPORTERep _cVEXPORTERep;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public EMPLOYEEcontroller(IEMPLOYEERep EMPLOYEERep, ICVEXPORTERep cVEXPORTERep, IMapper mapper, UserManager<User> userManager)
        {
            _EMPLOYEERep = EMPLOYEERep;
            _cVEXPORTERep = cVEXPORTERep;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EMPLOYEE>))]

        public IActionResult GetEMPLOYEEs()
        {
            var EMPLOYEEs = _mapper.Map<List<EMPLOYEEDto>>(_EMPLOYEERep.GetEMPLOYEEs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(EMPLOYEEs);
        }
        [HttpGet("{IDEMPLOYEE}")]
        [ProducesResponseType(200, Type = typeof(EMPLOYEE))]
        [ProducesResponseType(400)]
        public IActionResult GetEMPLOYEE(int IDEMPLOYEE)
        {
            if (!_EMPLOYEERep.EMPLOYEEEXISTS(IDEMPLOYEE))
                return NotFound();

            var EMPLOYEE = _mapper.Map<EMPLOYEEDto>(_EMPLOYEERep.GetEMPLOYEE(IDEMPLOYEE));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(EMPLOYEE);

        }
        [HttpPost]
        /*[Authorize(Roles = "Employee")]*/
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateEMPLOYEE([FromQuery] int IDEMPLOYEE, [FromBody] EMPLOYEEDto eMPLOYEECreate)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (eMPLOYEECreate == null)
                return BadRequest(ModelState);

            var eMPLOYEE = _EMPLOYEERep.GetEMPLOYEEs()
                .Where(c => c.NAME.Trim().ToUpper() == eMPLOYEECreate.NAME.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (eMPLOYEE != null)
            {
                ModelState.AddModelError("", "EMPLOYEE already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var EMPLOYEEMap = _mapper.Map<EMPLOYEE>(eMPLOYEECreate);

            EMPLOYEEMap = _EMPLOYEERep.GetEMPLOYEE(IDEMPLOYEE);
            if (!_EMPLOYEERep.CreateEMPLOYEE(EMPLOYEEMap))
            {
                ModelState.AddModelError("", "something went wrong while savin");
            }
            return Ok("successfully created");

        }

    }
}   