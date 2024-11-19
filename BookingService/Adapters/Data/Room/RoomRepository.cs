using Domain.Entities;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public RoomRepository(HotelDbContext hotelDbContext) 
        {
            _hotelDbContext = hotelDbContext;
        
        }
        public async Task<int> Create(Domain.Entities.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public async Task<Domain.Entities.Room> Delete(Domain.Entities.Room room)
        {
            var existingRoom = await _hotelDbContext.Rooms.FindAsync(room.Id);

            if (existingRoom == null)
            {
                return null; // Ou lançar uma exceção, se preferir
            }

            // Remover o guest do contexto
            _hotelDbContext.Rooms.Remove(existingRoom);

            // Salvar as alterações no banco
            await _hotelDbContext.SaveChangesAsync();

            return existingRoom;
        }

        public Task<Domain.Entities.Room> Get(int Id)
        {
            //throw new NotImplementedException();
            return _hotelDbContext.Rooms.Where(g =>g.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<Domain.Entities.Room> Update(Domain.Entities.Room room)
        {
            var existingRoom = await _hotelDbContext.Rooms.FindAsync(room.Id);

            if (existingRoom == null)
            {
                return null; // Ou lançar uma exceção, se preferir
            }

            // Atualizar as propriedades do guest
            existingRoom.Name = room.Name;
            existingRoom.Level = room.Level;
            existingRoom.InMaintenance = room.InMaintenance;
            existingRoom.Price = room.Price;
            
            await _hotelDbContext.SaveChangesAsync();

            return existingRoom;
        }
    }
}
