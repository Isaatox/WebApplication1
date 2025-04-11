namespace WebApplication1
{
    public interface ITypeService
    {
        Task<IEnumerable<ServiceType>> GetAllAsync();
        Task<ServiceType?> GetByIdAsync(int id);
        Task<ServiceType> CreateAsync(ServiceType service);
        Task<bool> DeleteAsync(int id);
    }
}
