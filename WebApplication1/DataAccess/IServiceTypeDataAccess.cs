namespace WebApplication1
{
    public interface IServiceTypeDataAccess
    {
        Task<IEnumerable<ServiceType>> GetAllAsync();
        Task<ServiceType?> GetByIdAsync(int id);
        Task<ServiceType> CreateAsync(ServiceType serviceType);
        Task<bool> DeleteAsync(int id);
    }
}
