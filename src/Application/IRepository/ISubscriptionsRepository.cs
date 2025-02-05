using Domain.Entities;

namespace Application.IRepository;

public interface ISubscriptionsRepository
{
    Task<Subscriptions?> GetAsyncById(int id);
    Task<IEnumerable<Subscriptions>> GetAllAsync();
    Task<IEnumerable<Subscriptions>> GetAllByUserAsync(long appUserID);
    Task Add(Subscriptions subscriptions);
    Task UpdateAsync(Subscriptions subscriptions);
    Task DeleteAsync(Subscriptions subscriptions);
    Task<Subscriptions?> DeleletByIndexAsync(long appUserId, int index);
    Task SaveChangeAsync(Subscriptions subscriptions);
    Task<Subscriptions?> GetSubscriptionsByCallBackId(long appUserID, int id);
    Task<IEnumerable<Subscriptions>> GetSubscriptionsBySub();
}


