using System.Reflection;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using RawRabbit;

namespace Action.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
                    => bus.SubscribeAsync<TCommand>(msg
                                    => handler.HandleAsync(msg),
                                                    ctx => ctx.UseSubscribeConfiguration(cfg
                                                                    => cfg.FromDeclaredQueue(q
                                                                            => q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
                            => bus.SubscribeAsync<TEvent>(msg
                                            => handler.HandleAsync(msg),
                                                            ctx => ctx.UseSubscribeConfiguration(cfg
                                                                            => cfg.FromDeclaredQueue(q
                                                                                    => q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
}