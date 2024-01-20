namespace Bat.Core;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class BiggerThanZero : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (int.TryParse(value.ToString(), out _))
            if (int.Parse(value.ToString()) > 0) return true;

        return false;
    }
}