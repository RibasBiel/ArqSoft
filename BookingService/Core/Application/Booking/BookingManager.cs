using Application.Booking.Requests;
using Application.Dtos;
using Application.Guest.Requests;
using Application.Ports;
using Application.Responses;
using Domain.Guest.Exceptions;
using Domain.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDto.MapToEntity(request.Data);

                //request.Data.Id = await _guestRepository.Create(guest);
                await booking.Save(_bookingRepository);
                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            
            catch (MissingRequiredInformation)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing passed required information"
                };
            }
            
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public Task<BookingResponse> CreateBooking()
        {
            throw new NotImplementedException();
        }

        public async Task<BookingResponse> GetBooking(int bookingId)
        {
            //throw new NotImplementedException();
            var booking = await _bookingRepository.Get(bookingId);
            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            return new BookingResponse
            {
                Data = BookingDto.MapToDto(booking),
                Success = true,
            };
        }

        public async Task<BookingResponse> DeleteBooking(int bookingId)
        {
            //throw new NotImplementedException();
            var booking = await _bookingRepository.Get(bookingId);
            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            booking.Delete(_bookingRepository);
            //_guestRepository.Delete(guest);
            return new BookingResponse
            {

                Success = true,
            };
        }

        public async Task<BookingResponse> UpdateBooking(int bookingId)
        {
            //throw new NotImplementedException();
            var booking = await _bookingRepository.Get(bookingId);
            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            booking.Update(_bookingRepository);
            //_guestRepository.Delete(guest);
            return new BookingResponse
            {

                Success = true,
            };
        }

        public Task<BookingResponse> DeleteBooking(BookingResponse guest)
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponse> UpdateBooking(BookingResponse guest)
        {
            throw new NotImplementedException();
        }
    }
}
