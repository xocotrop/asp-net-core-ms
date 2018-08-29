using System;

namespace Action.Common.Events
{
    public interface IAuthenticatedEvent  : IEvent
    {
         Guid UserId {get;}
    }
}