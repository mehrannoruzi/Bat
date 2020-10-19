using System;

namespace Bat.AspNetCore
{
    public class TokenValidationTime
    {
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}