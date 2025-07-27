using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Enums;

namespace Tms.Domain.Exceptions;
public class CustomException : Exception
{
    public ExceptionType Type { get; set; }

    public CustomException(ExceptionType type, string message) : base(message)
    {
        Type = type;
    }

    public CustomException(ExceptionType type, string message, Exception innerException) : base(message, innerException)
    {
        Type = type;
    }
}
