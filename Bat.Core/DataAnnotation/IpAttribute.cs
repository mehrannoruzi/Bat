﻿using System;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Bat.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class IpAttribute : ValidationAttribute
    {
        private readonly Regex rgx = new Regex(RegexPattern.Ip1);

        public override bool IsValid(object value)
        {
            if (value != null) return rgx.IsMatch(value.ToString());

            return false;
        }
    }
}