namespace Bat.Core;

public interface IModifyDateProperties : IBaseProperties
{
    string ModifyDateSh { get; set; }
    DateTime ModifyDateMi { get; set; }
}