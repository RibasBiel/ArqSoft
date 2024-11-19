using Application.Dtos;
using Application.Guest.Requests;
using Application.Ports;
using Application.Responses;
using Domain.Entities;
using Domain.Guest.Exceptions;
using Domain.Ports;

namespace Application.Guest
{
    public class GuestManager : IGuestManager
    {
        private IGuestRepository _guestRepository;

        public GuestManager(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDto.MapToEntity(request.Data);

                //request.Data.Id = await _guestRepository.Create(guest);
                await guest.Save(_guestRepository);
                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Message = "The passed ID is not valid"
                };
            }
            catch (MissingRequiredInformation)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing passed required information"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_EMAIL,
                    Message = "The given email is not valid"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public Task<GuestResponse> CreateGuest()
        {
            throw new NotImplementedException();
        }

        public async Task<GuestResponse> GetGuest(int guestId)
        {
            //throw new NotImplementedException();
            var guest = await _guestRepository.Get(guestId);
            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            return new GuestResponse
            {
                Data = GuestDto.MapToDto(guest),
                Success = true,
            };
        }

        public async Task<GuestResponse> DeleteGuest(int guestId)
        {
            //throw new NotImplementedException();
            var guest = await _guestRepository.Get(guestId);
            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            guest.Delete(_guestRepository);
            //_guestRepository.Delete(guest);
            return new GuestResponse
            {
                
                Success = true,
            };
        }

        public async Task<GuestResponse> UpdateGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);
            //var guest = GuestDto.MapToEntity(request.Data);
            
            if (guest == null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.NOT_FOUND,
                    Message = "No Guest record was found with the given id"
                };
            }
            //guest.Surname = Surname;
            guest.Update(_guestRepository);
            return new GuestResponse
            {

                Success = true,
            };
        }

        public Task<GuestResponse> DeleteGuest(Domain.Entities.Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task<GuestResponse> DeleteGuest(GuestResponse guest)
        {
            throw new NotImplementedException();
        }

        public Task<GuestResponse> UpdateGuest(GuestResponse guest)
        {
            throw new NotImplementedException();
        }
    }
}
