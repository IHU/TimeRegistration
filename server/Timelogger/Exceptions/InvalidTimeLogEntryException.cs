using System;

namespace Timelogger.Exceptions
{
	public class InvalidTimeLogEntryException : Exception
	{
		public InvalidTimeLogEntryException(string message) : base(message)
		{
		}

		public InvalidTimeLogEntryException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
