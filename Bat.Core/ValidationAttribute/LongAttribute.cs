namespace Bat.Core;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class LongAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return true;

        long.TryParse(value.ToString(), out long result);
        if (result != 0) return true;
        else return false;
    }

}