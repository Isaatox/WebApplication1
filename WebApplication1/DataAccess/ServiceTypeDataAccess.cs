using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ServiceTypeDataAccess : IServiceTypeDataAccess
    {
        private readonly AppDbContext _context;

        public ServiceTypeDataAccess(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceType>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<ServiceType?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<ServiceType> CreateAsync(ServiceType service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return false;

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
