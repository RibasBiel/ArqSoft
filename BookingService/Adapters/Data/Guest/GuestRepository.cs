using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext) 
        {
            _hotelDbContext = hotelDbContext;
        
        }
        public async Task<int> Create(Domain.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public async Task<Domain.Entities.Guest> Delete(Domain.Entities.Guest guest)
        {
            var existingGuest = await _hotelDbContext.Guests.FindAsync(guest.Id);

            if (existingGuest == null)
            {
                return null; // Ou lançar uma exceção, se preferir
            }

            // Remover o guest do contexto
            _hotelDbContext.Guests.Remove(existingGuest);

            // Salvar as alterações no banco
            await _hotelDbContext.SaveChangesAsync();

            return existingGuest;
        }

        public Task<Domain.Entities.Guest> Get(int Id)
        {
            
            return _hotelDbContext.Guests.Where(g =>g.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.Guest> Update(Domain.Entities.Guest guest)
        {
            var existingGuest = await _hotelDbContext.Guests.FindAsync(guest.Id);

            if (existingGuest == null)
            {
                return null; 
            }
            existingGuest.Name = guest.Name;
            existingGuest.Surname = guest.Surname;
            existingGuest.Email = guest.Email;
            existingGuest.DocumentId = guest.DocumentId;

            await _hotelDbContext.SaveChangesAsync();

            return existingGuest;
        }
    }
}
