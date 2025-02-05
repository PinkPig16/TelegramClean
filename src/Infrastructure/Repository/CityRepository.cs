using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository;

public class CityRepository : ICityRepository
{
    private readonly ApplicationDB _context;
    ITransliterate _transliterate;

    public CityRepository(ApplicationDB context, ITransliterate transliterate) 
    {
        _context = context;
        _transliterate = transliterate;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.citys.ToListAsync(); 
    }

    public async Task<City?> GetAsyncById(int id)
    {
        return await _context.citys.FirstAsync(x => x.Id == id);
    }

    public async Task<City?> GetAsyncByName(string name)
    {
        return await _context.citys.FirstAsync(x => x.Name == name);
    }
    public async Task<City?> GetAsyncByNameArray(List<string> ListCity)
    {
        return await _context.citys.Where(c => ListCity.Contains(c.Name)).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(City city)
    {
        _context.Update(city);
        await SaveChangeAsync();
    }
    public async Task Add(City city)
    {
        _context.Add(city);
        await SaveChangeAsync();
    }

    public async Task DeleteAsync(City city)
    {
        _context.Remove(city);
        await SaveChangeAsync();
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}

