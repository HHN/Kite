namespace Assets._Scripts.ServerCommunication
{
    public interface IOnErrorHandler
    {
        void OnError(Response response);
    }
}