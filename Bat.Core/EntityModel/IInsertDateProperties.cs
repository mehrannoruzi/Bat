using System;

namespace Bat.Core
{
    public interface IInsertDateProperties : IBaseProperties
    {
        string InsertDateSh { get; set; }
        DateTime InsertDateMi { get; set; }
    }
}
