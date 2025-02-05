using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.IRepository;
using Infrastructure.Data;
using Application.Interfaces;

namespace Infrastructure.Repository;

public class AppUserRepository : IAppUserRepository
{
    private readonly ApplicationDB _context;
    private IMessage _message;

    public AppUserRepository(ApplicationDB context, IMessage message)
    {
        _context = context;
        _message = message;
    }
    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        return await _context.appUsers.ToListAsync();
    }

    public async Task<AppUser?> GetAsyncById(long id)
    {
        return await _context.appUsers.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task UpdateAsync(AppUser appUser)
    {
            _context.Update(appUser);
            await SaveChangeAsync(appUser);
    }
    public async Task Add(AppUser appUser)
    {
        _context.Add(appUser);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(AppUser appUser)
    {
        _context.Remove(appUser);
        await SaveChangeAsync(appUser);
    }

    public async Task SaveChangeAsync(AppUser appUser)
    {
        try
        {
           await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
        {
            await _message.Send("Данная позиция уже существует в базе!", appUser.Id);
        } 
    }
}
