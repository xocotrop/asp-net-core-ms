namespace Action.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Name { get;  }

        protected UserCreated(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}