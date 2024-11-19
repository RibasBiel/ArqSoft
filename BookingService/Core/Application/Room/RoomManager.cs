using Application.Dtos;
using Application.Guest.Requests;
using Application.Ports;
using Application.Responses;
using Application.Room.Requests;
using Application.Room.Responses;
using Domain.Entities;
using Domain.Guest.Exceptions;
using Domain.Ports;

namespace Application.Room
{
    public class RoomManager : IRoomManager
    {
        private IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public async Task<RoomResponse> CreateRoom(CreateRoomRequest request)
        {
            try
            {
                var guest = RoomDto.MapToEntity(request.Data);

                
                await guest.Save(_roomRepository);
                request.Data.Id = guest.Id;

                return new RoomResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            
            catch (MissingRequiredInformation)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing passed required information"
                };
            }
            
            catch (Exception)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public Task<RoomResponse> CreateRoom()
        {
            throw new NotImplementedException();
        }

        public async Task<RoomResponse> GetRoom(int guestId)
        {
            //throw new NotImplementedException();
            var guest = await _roomRepository.Get(guestId);
            if (guest == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No room record was found with the given id"
                };
            }
            return new RoomResponse
            {
                Data = RoomDto.MapToDto(guest),
                Success = true,
            };
        }

        


        public async Task<RoomResponse> DeleteRoom(int roomId)
        {
            //throw new NotImplementedException();
            var room = await _roomRepository.Get(roomId);
            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No room record was found with the given id"
                };
            }
            room.Delete(_roomRepository);
            //_guestRepository.Delete(guest);
            return new RoomResponse
            {

                Success = true,
            };
        }

        public async Task<RoomResponse> UpdateRoom(int roomId)
        {

            //var  room= GuestDto.MapToEntity(request.Data);
            var room = await _roomRepository.Get(roomId);
            if (room == null)
            {
                return new RoomResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No room record was found with the given id"
                };
            }
            //guest.Surname = Surname;
            room.Update(_roomRepository);
            return new RoomResponse
            {

                Success = true,
            };
        }

        public Task<RoomResponse> DeleteRoom(RoomResponse room)
        {
            throw new NotImplementedException();
        }

        public Task<RoomResponse> UpdateRoom(RoomResponse room)
        {
            throw new NotImplementedException();
        }
    }
}
