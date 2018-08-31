using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        protected Activity()
        {

        }

        public Activity(Guid id, string name, Category category, Guid userId, string description, DateTime createdAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name.ToLowerInvariant();
            Category = category.Name;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}