using System.Net;

namespace PersonIdentificationDirectory.Utility.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException( ErrorCodes errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorCodes ErrorCode { get; set; }
    }
}
