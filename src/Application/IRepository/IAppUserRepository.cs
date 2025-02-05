using Domain.Entities;

namespace Application.IRepository;

public interface IAppUserRepository
{
    Task<AppUser?> GetAsyncById(long id);
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task Add(AppUser user);
    Task UpdateAsync(AppUser user);
    Task DeleteAsync(AppUser user);
    Task SaveChangeAsync(AppUser user);
}
