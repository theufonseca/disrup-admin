using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class PhoneFormatException : ExceptionBase
    {
        public PhoneFormatException(string sentPhone, int sentPhoneLength, List<int> sentPhoneLengthExpected)
        {
            Title = "Phone in incorret format";
            Detail = $"The phone '{sentPhone}' was sent with incorret length '{sentPhoneLength}'. The expected length is '{string.Join(',', sentPhoneLengthExpected)}'";
        }
    }
}
