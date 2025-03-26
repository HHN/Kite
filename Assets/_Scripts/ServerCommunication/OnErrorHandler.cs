namespace Assets._Scripts.Server_Communication
{
    public interface IOnErrorHandler
    {
        void OnError(Response response);
    }
}