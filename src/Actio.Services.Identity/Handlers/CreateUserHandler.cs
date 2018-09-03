using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Services;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient busClient, ILogger<CreateUserHandler> logger, IUserService userService)
        {
            _userService = userService;
            _busClient = busClient;
            _logger = logger;
        }
        public async Task HandleAsync(CreateUser command)
        {
            Console.WriteLine($"Creating user: {command.Email} {command.Name}");
            _logger.LogInformation($"Creating user: {command.Email} {command.Name}");
            try
            {

                await _userService.RegisterAsync(command.Email, command.Password, command.Name);

                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
                return;
            }
            catch (ActioException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
                _logger.LogError(ex.Message);
            }

        }
    }
}