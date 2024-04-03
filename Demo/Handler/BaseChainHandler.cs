namespace Demo.Handler;

public class BaseChainHandler(IChainHandler next) : IChainHandler
{
    public async Task HandleAsync(int id)
    {
        if (next is not null)
        {
            await next.HandleAsync(id);
        }
    }
}