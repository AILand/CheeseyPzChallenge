namespace Application.Core
{
    public interface IUseCaseRequest<T>
    {
    }

    public interface IUseCaseRequest : IUseCaseRequest<NoContent>
    {
    }

    public class NoContent
    {
        public static NoContent Value = new();
    }
}
