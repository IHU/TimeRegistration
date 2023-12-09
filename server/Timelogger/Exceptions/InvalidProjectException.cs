using System;

namespace Timelogger.Exceptions
{
	public class InvalidProjectException : Exception
	{
		public InvalidProjectException(string message) : base(message)
		{
		}

		public InvalidProjectException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
