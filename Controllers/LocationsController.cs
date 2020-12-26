using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using superVise.Entities;
using superVise.Helpers.Exceptions;
using superVise.Models.Requests;
using superVise.Services.Interfaces;

namespace superVise.Controllers
{
    //[Helpers.Authorization.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private ILocationService _locationService;
        private IUserService _userService;
        private IMapper _mapper;

        public LocationsController(ILocationService locationService, IUserService userService, IMapper mapper)
        {
            _locationService = locationService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetLocationsForUser([FromQuery]int userId, [FromQuery]string dateFrom, [FromQuery] string dateTo)
        {
            try
            {
                DateTime from = DateTime.Parse(dateFrom);
                DateTime to = DateTime.Parse(dateTo);
                if (from < to)
                    return BadRequest();

                return Ok(_locationService.GetRangedLocationsForUser(userId, from, to));

            } catch (Exception)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _locationService.GetById(id);
            return Ok(location);
        }
        
        [HttpPost("{userId}")]
        public IActionResult PostLocation(int userId, NewLocationRequest locationRequest)
        {
            var location = _mapper.Map<Location>(locationRequest);
            
            if (location.Timestamp.Year < 1900)
                return BadRequest();

            try
            {
                _locationService.Create(location, userId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _locationService.Delete(id);
            return Ok();
        }
    }
}