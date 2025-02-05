using Telegram.Bot.Types;

namespace Infrastructure.Command;
public class ParseMessage
{
    Dictionary<string,string> subscriptionsKey = new Dictionary<string, string>() 
    {
        {"subscriptions","" },
        {"city",""} 
    };

    public List<string> SubscriptionsParse(Update upd)
    {
        string messageText = upd.Message.Text;

        var words = messageText.Trim().ToLower().Split(' ')
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .Skip(1).ToList();
        return words;
            
    }
}

