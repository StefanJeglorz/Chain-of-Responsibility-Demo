namespace Demo.Handler;

public interface IChainHandler
{
    public Task HandleAsync(int id);
}