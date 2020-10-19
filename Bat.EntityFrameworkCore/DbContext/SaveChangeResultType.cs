namespace Bat.EntityFrameworkCore
{
    public enum SaveChangeResultType : int
    {
        Success = 1,

        DuplicateIndexKeyException = -2,
        EntityValidationException = -3,
        UpdateException = -4,
        UpdateConcurrencyException = -5,

        UnknownException = -10
    }
}