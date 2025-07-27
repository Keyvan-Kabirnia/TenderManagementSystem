using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Enums;
public enum ExceptionType
{
    NotFound,
    BadRequest,
    Unauthorized,
    Forbidden,
    ValidationError,
    InternalServerError
}
