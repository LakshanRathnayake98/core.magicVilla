using core.magicVilla.demo.Data;
using core.magicVilla.demo.Models;
using core.magicVilla.demo.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace core.magicVilla.demo.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaApi")]
    //[ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogger<VillaApiController> _logger;

        public VillaApiController (ILogger<VillaApiController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id")]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Get Villa Error with id" + id);
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> AddNewVilla([FromBody] VillaDTO villaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(villaDTO);
            }

            //Ckeck vill name is already exists or not
            if (VillaStore.villaList.FirstOrDefault(u => u.VillaName.ToLower() == villaDTO.VillaName.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //Check email is in correct pattern
            if (!isvalidemail(villaDTO.VillaEmail))
            {
                ModelState.AddModelError("CustomError", "Email is not in correct format");
                return BadRequest(ModelState);
            }
            villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            //villaDTO.VillaName = "NewVilla";

            VillaStore.villaList.Add(villaDTO);

            return Ok(villaDTO);
        }

        private bool isvalidemail(string email)
        {
            try
            {
                var mailaddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpDelete("id:int")]
        public IActionResult DeleteVilla(int id)
        {
           if(id == 0)
            {
                return BadRequest();
            }
            var delVilla = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (delVilla == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(delVilla);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("id:int")]
        public IActionResult UpdateVilla(int id, VillaDTO villaDTO)
        {
            if(id == 0 || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            villa.VillaName = villaDTO.VillaName;
            villa.VillaEmail = villaDTO.VillaEmail;

            return NoContent();
        }

    } 
}
