using System;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Action.Common.Exceptions;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _activityRepository = activityRepository;
        }
        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(category);
            if(activityCategory == null){
                throw new ActioException("category_not_found", $"Category: {category} was not found");
            }

            var activity = new Activity(id, name, activityCategory, userId, description, createdAt);

            await _activityRepository.AddAsync(activity);
        }
    }
}