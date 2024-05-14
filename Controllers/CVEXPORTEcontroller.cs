using APIcv.Dto;
using APIcv.DATA;
using APIcv.interfaces;
using APIcv.Models;
using APIcv.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace APIcv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVEXPORTEcontroller : Controller
    {
        
        
            private readonly ICVEXPORTERep _CVEXPORTERep;
            private readonly IMapper _mapper;

            public CVEXPORTEcontroller(ICVEXPORTERep CVEXPORTERep, IMapper mapper)
            {
                _CVEXPORTERep = CVEXPORTERep;
                _mapper = mapper;
            }

            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<CVEXPORTE>))]

            public IActionResult GetCVEXPORTEs()
            {
                var CVEXPORTEs = _mapper.Map<List<CVEXPORTEDto>>(_CVEXPORTERep.GetCVEXPORTEs());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(CVEXPORTEs);
            }
            [HttpGet("{IDCVEXPORTE}")]
            [ProducesResponseType(200, Type = typeof(CVEXPORTE))]
            [ProducesResponseType(400)]
            public IActionResult GetCVEXPORTE(int IDCVEXPORTE)
            {
                if (!_CVEXPORTERep.CVEXPORTEEXISTS(IDCVEXPORTE))
                    return NotFound();

                var CVEXPORTE = _mapper.Map<CVEXPORTEDto>(_CVEXPORTERep.GetCVEXPORTE(IDCVEXPORTE));


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(CVEXPORTE);

            }

            [HttpPost]
            [ProducesResponseType(204)]
            [ProducesResponseType(400)]
            public IActionResult CreateCVEXPORTE([FromQuery] int IDUser,[FromBody] CVEXPORTEDto CVEXPORTECreate)
            {
                if (CVEXPORTECreate == null)
                    return BadRequest(ModelState);

                var CVEXPORTE = _CVEXPORTERep.GetCVEXPORTEs()
                    .Where(c => c.FORMAT.Trim().ToUpper() == CVEXPORTECreate.FORMAT.TrimEnd().ToUpper())
                    .FirstOrDefault();
                if (CVEXPORTE != null)
                {
                    ModelState.AddModelError("", "CVEXPORTE already exists");
                    return StatusCode(422, ModelState);
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var CVEXPORTEMap = _mapper.Map<CVEXPORTE>(CVEXPORTECreate);


                if (!_CVEXPORTERep.CreateCVEXPORTE(IDUser, CVEXPORTEMap)) 
                {
                    ModelState.AddModelError("", "something went wrong while savin");
                }
                return Ok("successfully created");

            }
        [HttpDelete("{IDCVEXPORTE}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult deleteCVEXPORTE(int IDCV)
        {
            if (!_CVEXPORTERep.CVEXPORTEEXISTS(IDCV))
            {
                return NotFound();
            }

            var CVEXPORTETodelete = _CVEXPORTERep.GetCVEXPORTE(IDCV);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_CVEXPORTERep.deleteCVEXPORTE(CVEXPORTETodelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting CVEXPORTE");
            }

            return NoContent();
        }


    }

}