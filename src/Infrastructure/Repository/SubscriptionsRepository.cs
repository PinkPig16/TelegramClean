using Application.Interfaces;
using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly ApplicationDB _context;
    private IMessage _message;

    public SubscriptionsRepository(ApplicationDB context,IMessage message)
    {
        _context = context;
        _message = message;

    }
    public async Task Add(Subscriptions subscriptions)
    {
        _context.subscriptions.Add(subscriptions);
        await SaveChangeAsync(subscriptions);
    }
    public async Task DeleteAsync(Subscriptions subscriptions)
    {
        _context.Remove(subscriptions);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Subscriptions>> GetAllAsync()
    {
        return await _context.subscriptions.ToListAsync();
    }

    public async Task<Subscriptions?> GetAsyncById(int id)
    {
        return await _context.subscriptions.FirstAsync(x => x.Id == id);
    }
    public async Task UpdateAsync(Subscriptions subscriptions)
    {
        _context.Update(subscriptions);
        await _context.SaveChangesAsync();
    }
    public async Task SaveChangeAsync(Subscriptions subscriptions)
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
        {

            await _message.Send("Данная позиция уже существует!", subscriptions.AppUserId);
        }
    }
    public async Task<Subscriptions?> GetSubscriptionsByCallBackId(long appUserId, int id)
    {
        return await _context.subscriptions.FirstOrDefaultAsync(x => x.AppUser.Id == appUserId && x.CallBackId == id);
    }
    public async Task<IEnumerable<Subscriptions>> GetAllByUserAsync(long appUserId)
    {
       return await _context.subscriptions.Include(x => x.City).Where(x => x.AppUser.Id == appUserId).ToListAsync();
    }

    public async Task<Subscriptions?> DeleletByIndexAsync(long appUserId, int index)
    {
        var list = await _context.subscriptions.Where(x => x.AppUser.Id == appUserId).ToListAsync();
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index-1)
            {
                await DeleteAsync(list[i]);
                return list[i];
            }
        }
        return null;
    }

    public async Task<IEnumerable<Subscriptions>> GetSubscriptionsBySub()
    {
        return await _context.subscriptions.Where(x => x.SubscriptionАvailable).ToListAsync();
    }
}

