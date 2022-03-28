namespace Bat.Core;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
public class SwaggerExcludeAttribute : Attribute
{ }