namespace Action.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason { get; }

        public string Code { get; }

         public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        

        protected CreateActivityRejected()
        {

        }

        public CreateActivityRejected(string name, string code, string reason, string category, string description)
        {
            Category = category;
            Description = description;
            Name = name;
            Code = code;
            Reason = reason;
        }
    }
}