using Application.Booking.Requests;
using Application.Dtos;
using Application.Guest;
using Application.Guest.Requests;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        

        public BookingController(
            ILogger<BookingController> logger,
            IBookingManager bookingManager)
        {
            _logger = logger;
            _bookingManager = bookingManager;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Post(BookingDto booking)
        {
            var resquest = new CreateBookingRequest
            {
                Data = booking,
            };

            var res = await _bookingManager.CreateBooking(resquest);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
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
        public async Task<ActionResult<BookingDto>>Get(int bookingId)
        {
            var res = await _bookingManager.GetBooking(bookingId);
            if (res.Success) return Created("",res.Data);
            return NotFound(res);
        }

        [HttpDelete("{bookingId}")]
        public async Task<ActionResult<BookingDto>> Delete(int bookingId)
        {

            var res = await _bookingManager.GetBooking(bookingId);
            if (res == null)
            {
                return NotFound(res);
            }
            _bookingManager.DeleteBooking(res);
            return Ok(res);

        }

        [HttpPut("{bookingId}")]
        public async Task<ActionResult<BookingDto>> PutBooking(int bookingId)
        {
            //var resquest = new CreateGuestRequest
            //{
            //    Data = guest,
            //};
            //if (resquest == null)
            //{
            //    return NotFound(resquest);
            //}
            var res = await _bookingManager.GetBooking(bookingId);
            if (res == null)
            {
                return NotFound(res);
            }
            _bookingManager.UpdateBooking(res);
            return Ok(res);


        }
    }
}
