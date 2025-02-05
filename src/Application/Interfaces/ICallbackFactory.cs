namespace Application.Interfaces;
public interface ICallbackFactory
{

    public string CreateAddMethod(int MessageId, string action);
    public string ValidateCallbackDataLength(string json);

}


