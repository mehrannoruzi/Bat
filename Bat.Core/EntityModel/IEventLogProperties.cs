namespace Bat.Core
{
    public interface IEventLogProperties : IBaseProperties, IInsertDateProperties, ISoftDeleteProperty
    {
        public string UserId { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }
    }
}