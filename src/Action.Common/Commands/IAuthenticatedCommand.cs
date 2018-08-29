using System;

namespace Action.Common.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
         Guid UserId {get;set;}
    }
}