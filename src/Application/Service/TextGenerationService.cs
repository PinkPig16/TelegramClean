using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service;

public class TextGenerationService
{
    public string ListText(IEnumerable<Subscriptions> list)
    {
        var text = new StringBuilder("Активные подписки:\n \n");
        var index = 1;
        foreach (var subscription in list) 
        {
            text.Append($"{index}. `{subscription.Name} {(subscription.City != null ? subscription.City.Name : "")}`\n");
            index++;
        }

        return text.ToString();
    }
}