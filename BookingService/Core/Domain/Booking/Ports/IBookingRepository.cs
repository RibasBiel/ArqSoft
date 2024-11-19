namespace Domain.Ports
{
    public interface IBookingRepository
    {
        Task<Domain.Entities.Booking> Get(int Id);
        Task<int> Create(Domain.Entities.Booking booking);
        Task<Domain.Entities.Booking> Update(Domain.Entities.Booking booking);
        Task<Domain.Entities.Booking> Delete(Domain.Entities.Booking booking);
    }
}
