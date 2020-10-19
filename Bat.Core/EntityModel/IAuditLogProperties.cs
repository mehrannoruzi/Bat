using System;

namespace Bat.Core
{
    public class IAuditLogProperties : IBaseProperties, IInsertDateProperties, ISoftDeleteProperty
    {
        public string UserId { get; set; }
        public string ActionType { get; set; }
        public string EntityName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public bool IsDeleted { get; set; }
        public string InsertDateSh { get; set; }
        public DateTime InsertDateMi { get; set; }
    }
}
