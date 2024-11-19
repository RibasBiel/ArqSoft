using Application.Dtos;
using Application.Guest;
using Application.Guest.Requests;
using Application.Ports;
using Application.Room.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;
        

        public RoomController(
            ILogger<RoomController> logger,
            IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Post(RoomDto room)
        {
            var resquest = new CreateRoomRequest
            {
                Data = room,
            };

            var res = await _roomManager.CreateRoom(resquest);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            
            else if (res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }




            _logger.LogError("Response with unkwn ErrorCode Returned", res);
            return BadRequest();



        }
        [HttpGet]
        public async Task<ActionResult<RoomDto>>Get(int guestId)
        {
            var res = await _roomManager.GetRoom(guestId);
            if (res.Success) return Created("",res.Data);
            return NotFound(res);
        }


        [HttpDelete("{roomId}")]
        public async Task<ActionResult<RoomDto>> Delete(int roomId)
        {

            var res = await _roomManager.GetRoom(roomId);
            if (res == null)
            {
                return NotFound(res);
            }
            _roomManager.DeleteRoom(res);
            return Ok(res);

        }

        [HttpPut("{roomId}")]
        public async Task<ActionResult<RoomDto>> PutGuest(int roomId)
        {
           
            var res = await _roomManager.GetRoom(roomId);
            if (res == null)
            {
                return NotFound(res);
            }
            _roomManager.UpdateRoom(res);
            return Ok(res);


        }
    }
}
