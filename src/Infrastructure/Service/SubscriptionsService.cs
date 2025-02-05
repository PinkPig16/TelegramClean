using Application.IRepository;
using Domain.Entities;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Service;
public class SubscriptionsService
{
    private readonly Subscriptions _subscriptions;
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IConfiguration _config;

    public SubscriptionsService(Subscriptions subscriptions, ISubscriptionsRepository subscriptionsRepository, IConfiguration configuration)
    {
        _subscriptions = subscriptions;
        _subscriptionsRepository = subscriptionsRepository;
        _config = configuration;
    }


}
