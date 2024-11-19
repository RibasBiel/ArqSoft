using Domain.Ports;
using Domain.Room.Enums;
using Domain.Room.Exceptions;
using Domain.Room.ValueObjects;
using Shared;
using System.Reflection.Metadata;

namespace Domain.Entities
{
    public class Room
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        
        public Price Price { get; set; }

        public bool IsAvailable { 
            get{
                if(this.InMaintenance || HasGuest)
                {
                    return false;
                }
                return true;
            }
            
        }

        public bool HasGuest { 
            get{
                return true;
            }
        }

        private void ValidateState()
        {
            

            if (string.IsNullOrEmpty(Name) ||
                Level.Equals(0) 
                )
            {
                throw new MissingRequiredInformation();
            }

            

        }

        public async Task Save(IRoomRepository roomRepository)
        {
            this.ValidateState();

            if (this.Id == 0)
            {
                this.Id = await roomRepository.Create(this);
            }
            else
            {
                //await
            }
        }

        

        public async Task Update(IRoomRepository roomRepository)
        {


            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot update a room that has not been created.");
            }
            var room = await roomRepository.Get(this.Id);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found for update.");
            }

            room.Name = this.Name;
            room.Level = this.Level;
            room.InMaintenance = this.InMaintenance;
            room.Price = this.Price;
            await roomRepository.Update(room);
        }

        public async Task Delete(IRoomRepository roomRepository)
        {


            if (this.Id == 0)
            {
                throw new InvalidOperationException("Cannot delete a room that has not been created.");
            }
            var room = await roomRepository.Get(this.Id);
            if (room == null)
            {
                throw new InvalidOperationException("Room not found for update.");
            }
            await roomRepository.Delete(room);

        }
    }
}
