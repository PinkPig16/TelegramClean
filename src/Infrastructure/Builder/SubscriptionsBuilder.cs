using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Builder;

public class SubscriptionsBuilder
{
    private readonly Subscriptions _subscriptions;
    private readonly IConfiguration _config;
    public SubscriptionsBuilder(Subscriptions subscriptions, IConfiguration config)
    {
        _subscriptions = subscriptions;
        _config = config;
    }
    public SubscriptionsBuilder setName(string position)
    {
        _subscriptions.Name = position;
        return this;
    }
    public SubscriptionsBuilder setAppUser(AppUser appUser)
    {
        _subscriptions.AppUser = appUser;
        return this;
    }
    public SubscriptionsBuilder setCity(City? city)
    {
        _subscriptions.City = city;
        return this;
    }
    public Subscriptions Build()
    { 
        return _subscriptions;
    }
    public SubscriptionsBuilder setUrl(string job)
    {
        string urlSubscriptions = _subscriptions.Name.Trim().Replace(" ", "+");
        if (_subscriptions.City != null)
        {
            _subscriptions.Url = _config["UrlWork"] + Uri.EscapeDataString($"jobs-{_subscriptions.City.NameEng}-{urlSubscriptions}");
        }
        else
        {
            _subscriptions.Url = _config["UrlWork"] + Uri.EscapeDataString($"jobs-{urlSubscriptions}");
        }
        return this;
    }
    public SubscriptionsBuilder setCallBack(int callback)
    {
        _subscriptions.CallBackId = callback;
        return this;
    }
}
