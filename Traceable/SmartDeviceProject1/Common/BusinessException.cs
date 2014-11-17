using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SmartDeviceProject1.Common
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
