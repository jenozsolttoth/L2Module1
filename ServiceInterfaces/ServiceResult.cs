
namespace ServiceInterfaces
{
    public interface IServiceResult<out TPayload>
    {
        TPayload Entity { get; }
        bool Success { get; }
        string Message { get; }
    }
}
