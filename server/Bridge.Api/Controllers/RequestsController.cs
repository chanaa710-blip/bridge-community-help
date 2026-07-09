using AutoMapper;
using Bridge.Core.Models;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using Bridge.Service;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Add(RequestResource entity)
        {
            return Ok(await _requestService.Add(entity));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestResource>>> GetAll()
        {
            var requests = await _requestService.GetAll();
            return Ok(requests);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(Guid id)
        {
            return Ok(await _requestService.DeleteById(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestResource?>> GetById(Guid id)
        { 
            var request = await _requestService.GetById(id);
            return Ok(request);
        }
        [HttpGet("nearby-requests")]
        public async Task<ActionResult<IEnumerable<RequestResource>>> GetRequestsByLocationAndCategory([FromQuery] double lat, [FromQuery] double lng, [FromQuery] double radiusInMeters, [FromQuery] Guid? categoryId)
        {
            var requests = await _requestService.GetRequestsByLocationAndCategory(lat, lng, radiusInMeters, categoryId);
            return Ok(requests);
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateRequest(RequestResource entity)
        {
            return Ok(await _requestService.Update(entity));
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<bool>> UpdateStatus(Guid id, [FromBody] RequestStatus newStatus)
        {
           return Ok(await _requestService.UpdateStatus(id,newStatus));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRequestsByUserId(Guid userId)
        {
            var requests = await _requestService.GetRequestsByUserId(userId);
            return Ok(requests);
        }
    }
}
