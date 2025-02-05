using System.Text.Json;

namespace Infrastructure.DTO
{
    public abstract class CallbackBase
    {
        public string Method { get; set; } = "add";
        public string Action { get; set; }
        public int MessageId { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
        public CallbackBase FromJson(string json)
        {
            return JsonSerializer.Deserialize<CallbackBase>(json);
        }
    }
}