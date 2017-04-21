using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
	public class Logger
	{
		private static bool enableLog = true;
		public static bool EnableLog
		{
			get { return enableLog; }
			set { enableLog = value; }
		}

		private static bool enableSimpleLog = true;
		public static bool EnableSimpleLog
		{
			get { return enableSimpleLog; }
			set { enableSimpleLog = value; }
		}

		public static void Debug(object msg)
		{
			if (enableLog) LoggerManager.Instance.Debug(msg);
		}
		public static void Debug(string format, params Object[] args)
		{
			if (enableLog) LoggerManager.Instance.Debug(format, args);
		}
		public static void Info(object msg)
		{
			if (enableLog) LoggerManager.Instance.Info(msg);
		}
		public static void Info(string format, params Object[] args)
		{
			if (enableLog) LoggerManager.Instance.Info(format, args);
		}
		public static void Warn(object msg)
		{
			if (enableLog) LoggerManager.Instance.Warn(msg);
		}
		public static void Warn(string format, params Object[] args)
		{
			if (enableLog) LoggerManager.Instance.Warn(format, args);
		}
		public static void Error(object msg)
		{
			if (enableLog) LoggerManager.Instance.Error(msg);
		}
		public static void Error(string format, params Object[] args)
		{
			if (enableLog) LoggerManager.Instance.Error(format, args);
		}


		public static void Simple(object msg)
		{
			if (enableSimpleLog) Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff  ") + " SIMPLE:" + msg);
		}
		public static void Simple(string format, params Object[] args)
		{
			if (enableSimpleLog) Console.Out.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff  ") + " SIMPLE:" + String.Format(format, args));
		}

		static private string logpathlog = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

		static public void writelog(string classname)
		{
			string path = logpathlog;
			if (!File.Exists(path))
			{
				// Create a file to write to.
				using (File.Create(path)) { }
			}

			using (StreamWriter sw = File.AppendText(path))
			{
				//sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + "\t\n");
				sw.WriteLine(classname + "\t\n");
				//sw.WriteLine("------------------------------------------------------------------------" + "\t\n");
				sw.Close();
			}

		}

		static public void writelog(string format, params object[] obj)
		{
			writelog(string.Format(format, obj));
		}

	}
}
