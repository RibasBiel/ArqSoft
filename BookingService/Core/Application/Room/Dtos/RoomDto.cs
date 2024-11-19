using Domain.Entities;
using Domain.Enums;
using Domain.Room.Enums;
using Domain.Room.ValueObjects;
using Domain.ValueObjects;

namespace Application.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        
        public bool InMaintenance { get; set; }
        
        public static Domain.Entities.Room MapToEntity(RoomDto roomDto)
        {
            return new Domain.Entities.Room
            {
                Id = roomDto.Id,
                Name = roomDto.Name,
                Level = roomDto.Level,
                InMaintenance = roomDto.InMaintenance,
                

            };

        }

        public static RoomDto MapToDto(Domain.Entities.Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                
            };

        }

        
    }
}
