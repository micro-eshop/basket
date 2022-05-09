using Proto;

namespace Basket.Core.Actors;

public class HelloActor : IActor
{
    public Task ReceiveAsync(IContext context)
    {
        var msg = context.Message;
        return Task.CompletedTask;
    }
}