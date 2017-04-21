using System;
namespace ClientHelper
{
	public class UnsupportedMessageException : Exception
	{
		public UnsupportedMessageException (string message) : base(message)
		{
		}
	}
}

