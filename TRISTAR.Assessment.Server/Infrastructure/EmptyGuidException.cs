using System;

namespace TRISTAR.Assessment.Infrastructure
{
    /// <summary>
    /// Thrown when an empty unique identifier is provided as an argument.
    /// </summary>
    public class EmptyGuidException : ArgumentException
    {
        private const string ExceptionMessage = "A non-empty GUID is required!";

        public EmptyGuidException()
            : base(ExceptionMessage)
        {
        }

        public EmptyGuidException(string paramName)
            : base(ExceptionMessage, paramName)
        {
        }

        public EmptyGuidException(string paramName, Exception innerException)
            : base(ExceptionMessage, paramName, innerException)
        {
        }
    }
}
