namespace WebApplication1
{
    public class TypeService : ITypeService
    {
        private readonly IServiceTypeDataAccess _dataAccess;

        public TypeService(IServiceTypeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Task<IEnumerable<ServiceType>> GetAllAsync() => _dataAccess.GetAllAsync();

        public Task<ServiceType?> GetByIdAsync(int id) => _dataAccess.GetByIdAsync(id);

        public Task<ServiceType> CreateAsync(ServiceType service) =>
            _dataAccess.CreateAsync(service);

        public Task<bool> DeleteAsync(int id) => _dataAccess.DeleteAsync(id);
    }
}
