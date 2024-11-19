using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public BookingRepository(HotelDbContext hotelDbContext) 
        {
            _hotelDbContext = hotelDbContext;
        
        }
        public async Task<int> Create(Domain.Entities.Booking booking)
        {
            _hotelDbContext.Bokings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking.Id;
        }

        public async Task<Domain.Entities.Booking> Delete(Domain.Entities.Booking booking)
        {
            var existingBooking = await _hotelDbContext.Bokings.FindAsync(booking.Id);

            if (existingBooking == null)
            {
                return null; 
            }

            
            _hotelDbContext.Bokings.Remove(existingBooking);

            
            await _hotelDbContext.SaveChangesAsync();

            return existingBooking;
        }

        public Task<Domain.Entities.Booking> Get(int Id)
        {
            
            return _hotelDbContext.Bokings.Where(g =>g.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.Booking> Update(Domain.Entities.Booking booking)
        {
            var existingBooking = await _hotelDbContext.Bokings.FindAsync(booking.Id);

            if (existingBooking == null)
            {
                return null;
            }
            existingBooking.PlacedAt = booking.PlacedAt;
            existingBooking.Start = booking.Start;
            existingBooking.End = booking.End;
            existingBooking.Room = booking.Room;
            existingBooking.Guest = booking.Guest;

            await _hotelDbContext.SaveChangesAsync();

            return existingBooking;
        }
    }
}
