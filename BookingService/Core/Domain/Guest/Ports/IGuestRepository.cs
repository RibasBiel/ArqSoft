using Domain.Entities;

namespace Domain.Ports
{
    public interface IGuestRepository
    {
        Task<Domain.Entities.Guest> Get(int Id);
        Task<Domain.Entities.Guest> Delete(Domain.Entities.Guest guest);
        Task<Domain.Entities.Guest> Update(Domain.Entities.Guest guest);
        Task<int> Create(Domain.Entities.Guest guest);

    }
}
