using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public class LoggerConsole : ILogger
	{
		public void Debug(object msg)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +" DEBUG:"+ msg);
		}

		public void Debug(string format, params Object[] args)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " DEBUG:" + String.Format(format, args));
		}

		public void Info(object msg)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  INFO:" + msg);
		}

		public void Info(string format, params Object[] args)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  INFO:" + String.Format(format, args));

		}
		public void Warn(object msg)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  WARN:" + msg);
		}

		public void Warn(string format, params Object[] args)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  WARN:" + String.Format(format, args));
		}

		public void Error(object msg)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ERROR:" + msg);
		}

		public void Error(string format, params Object[] args)
		{
			Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " ERROR:" + String.Format(format, args));
		}
	}
}
