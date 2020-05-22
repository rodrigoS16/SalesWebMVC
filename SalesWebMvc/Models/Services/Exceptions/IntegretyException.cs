using System;

namespace SalesWebMvc.Models.Services.Exceptions
{
    public class IntegretyException : ApplicationException
    {
        public IntegretyException(string message) : base(message)
        {

        }

    }
}
