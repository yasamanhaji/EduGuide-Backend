using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Exceptions
{
    public  class RequestCounselorException(string msg): Exception(msg)
    {
    }
}
