using System;
using System.Collections.Generic;

namespace Bat.EntityFrameworkCore
{
    public class SaveChangeResult
    {
        public bool IsSuccessful { get; set; }
        public Dictionary<string, ValidationError> ValidationErrors { get; set; }
        public Exception Exception { get; set; }
        public SaveChangeResultType ResultType { get; set; }
        public string Message { get; set; }
        public int Result { get; set; }
    }
}