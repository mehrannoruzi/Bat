using System;
using System.ComponentModel.DataAnnotations;

namespace Bat.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DoubleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double.TryParse(value.ToString(), out double result);
            if (result != 0) return true;
            else return false;
        }
    }
}