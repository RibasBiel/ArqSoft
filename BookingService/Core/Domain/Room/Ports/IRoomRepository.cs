namespace Domain.Ports
{
    public interface IRoomRepository
    {
        Task<Domain.Entities.Room> Get(int Id);
        Task<int> Create(Domain.Entities.Room room);
        
        Task<Domain.Entities.Room> Delete(Domain.Entities.Room room);
        Task<Domain.Entities.Room> Update(Domain.Entities.Room room);
    }
}
