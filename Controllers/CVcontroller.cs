using APIcv.DATA;
using APIcv.Dto;
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
    public class CVcontroller : Controller
    {
        private readonly ICVrep _CVrep;
        private readonly IMapper _mapper;
        private readonly ICVEXPORTERep _cVEXPORTERep;

        public CVcontroller(ICVrep CVrep , IMapper mapper , ICVEXPORTERep cVEXPORTERep) 
        {
            _CVrep = CVrep;
            _mapper = mapper;
            _cVEXPORTERep = cVEXPORTERep;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CV>))]

        public IActionResult GetCVS()
        {
            var CVS = _mapper.Map<List<CVDto>>(_CVrep.GetCVs());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(CVS);
        }


        [HttpGet("{IDCV}")]
        [ProducesResponseType(200, Type = typeof(CV))]
        [ProducesResponseType(400)]
        public IActionResult GetCV(int IDCV)
        {
            if (!_CVrep.CVEXISTS(IDCV))
                return NotFound();

            var CV = _mapper.Map<CVDto>(_CVrep.GetCV(IDCV));


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(CV);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCV([FromQuery] int IDEMPLOYEE, [FromBody] CVDto CVCreate)
        {
            if (CVCreate == null)
                return BadRequest(ModelState);

            var CV = _CVrep.GetCVs()
                .Where(c => c.ETAT.Trim().ToUpper() == CVCreate.ETAT.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (CV != null)
            {
                ModelState.AddModelError("", "CV already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CVMap = _mapper.Map<CV>(CVCreate);


            if (!_CVrep.CreateCV(IDEMPLOYEE, CVMap)) 
            {
                ModelState.AddModelError("", "something went wrong while savin");
            }
            return Ok("successfully created");

        }
        [HttpPut("{IDCV}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCV(int IDCV, [FromQuery] int IDCVMODIF, [FromBody] CVDto updatedCV)
        {
            if (updatedCV == null)
                return BadRequest(ModelState);

            if (IDCV != updatedCV.ID)
                return BadRequest(ModelState);

            if (!_CVrep.CVEXISTS(IDCV))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var CVMap = _mapper.Map<CV>(updatedCV);

            if (!_CVrep.updateCV(IDCVMODIF, CVMap))
            {
                ModelState.AddModelError("", "Something went wrong updating CVMODIF");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{IDCV}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult deleteCV(int IDCV)
        {
            if (!_CVrep.CVEXISTS(IDCV))
            {
                return NotFound();
            }         

            var CVToDelete = _CVrep.GetCV(IDCV);
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_CVrep.deleteCV(CVToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting CV");
            }

            return NoContent();
        }

    }
}
