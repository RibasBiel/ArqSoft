
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Status {  get; set; }

        public static Domain.Entities.Booking MapToEntity(BookingDto bookingDto) => new Domain.Entities.Booking
        {
            Id = bookingDto.Id,
            PlacedAt = bookingDto.PlacedAt,
            Start = bookingDto.Start,
            End = bookingDto.End,
            //Status = new Status
            //{
            //    Status = (Status)bookingDto.Status
            //}

        };


        public static BookingDto MapToDto(Domain.Entities.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                
            };

        }
    }
}
