using Application.Guest.Requests;
using Application.Responses;
using Application.Room.Requests;
using Application.Room.Responses;

namespace Application.Ports
{
    public interface IRoomManager
    {
        Task<RoomResponse> CreateRoom(CreateRoomRequest request);
        Task<RoomResponse> GetRoom(int roomId);
        Task<RoomResponse> DeleteRoom(RoomResponse room);
        Task<RoomResponse> UpdateRoom(RoomResponse room);
    }
}
