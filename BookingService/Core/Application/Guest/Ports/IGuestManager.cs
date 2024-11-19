using Application.Guest.Requests;
using Application.Responses;

namespace Application.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
        Task<GuestResponse> DeleteGuest(GuestResponse guest);
        Task<GuestResponse> UpdateGuest(GuestResponse guest);
    }
}
