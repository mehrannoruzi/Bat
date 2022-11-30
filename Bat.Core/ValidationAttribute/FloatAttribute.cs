namespace Bat.Core;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class FloatAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return true;

        float.TryParse(value.ToString(), out float result);
        if (result != 0) return true;
        else return false;
    }
}