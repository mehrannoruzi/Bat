namespace Bat.Core;

public interface ISoftDeleteProperty : IBaseProperties
{
    bool IsDeleted { get; set; }
}