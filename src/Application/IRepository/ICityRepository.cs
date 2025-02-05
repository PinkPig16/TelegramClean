using Domain.Entities;

namespace Application.IRepository;
public interface ICityRepository 
{
    Task<City?> GetAsyncById(int id);
    Task<City?> GetAsyncByName(string name);
    Task<IEnumerable<City>> GetAllAsync();
    Task Add(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(City city);
    Task SaveChangeAsync();
    Task<City> GetAsyncByNameArray(List<string> names);
}
