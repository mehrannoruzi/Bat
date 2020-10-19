using System;
using System.ComponentModel.DataAnnotations;

namespace Bat.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ByteAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            byte.TryParse(value.ToString(), out byte result);
            if (result != 0) return true;
            else return false;
        }
    }
}