using Application.Interfaces;
using Infrastructure.DTO;

namespace Web.Service;

public class CallbackFactory : ICallbackFactory
{
    public string CreateAddMethod(int MessageId, string action)
    {
        CallbackBase callbackDTO = new CallbackDTO()
        {
            Action = action,
            MessageId = MessageId,
        };

        return ValidateCallbackDataLength(callbackDTO.ToJson());
    }
    public string ValidateCallbackDataLength(string json)
    {
        if (json.Length > 64) throw new Exception("Error. callback_data more 64 bite.");

        return json;
    }
}
