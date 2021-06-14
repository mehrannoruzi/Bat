namespace Bat.Core
{
    public interface IAuditLogProperties : IBaseProperties, IInsertDateProperties, ISoftDeleteProperty
    {
        public string UserId { get; set; }
        public string ActionType { get; set; }
        public string EntityName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}