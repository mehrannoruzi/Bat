using System;

namespace Bat.Core
{
    public class IEventLogProperties : IBaseProperties, IInsertDateProperties, ISoftDeleteProperty
    {
        public string UserId { get; set; }
        public string ActionType { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }

        public bool IsDeleted { get; set; }
        public string InsertDateSh { get; set; }
        public DateTime InsertDateMi { get; set; }

    }
}
