using Application.Booking.Requests;
using Application.Guest.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest request);
        Task<BookingResponse> GetBooking(int bookingId);
        Task<BookingResponse> DeleteBooking(BookingResponse guest);
        Task<BookingResponse> UpdateBooking(BookingResponse guest);
    }
}
