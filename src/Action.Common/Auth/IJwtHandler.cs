using System;

namespace Action.Common.Auth
{
    public interface IJwtHandler
    {
         JsonWebToken Create(Guid userId);
    }
}