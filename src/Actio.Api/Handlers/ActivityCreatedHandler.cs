using System;
using System.Threading.Tasks;
using Actio.Api.Models;
using Actio.Api.Repositories;
using Action.Common.Events;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity{
                Category = @event.Category,
                Name = @event.Name,
                UserId = @event.UserId,
                Id = Guid.NewGuid(),
                CreatedAt = @event.CreatedAt,
                Description = @event.Description
            });
            Console.WriteLine($"Activity Created: {@event.Name}");
        }
    }
}