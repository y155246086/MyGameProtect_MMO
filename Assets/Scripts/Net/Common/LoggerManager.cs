using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public class LoggerManager
	{
		private static LoggerManager instance = null;
		public static LoggerManager Instance
		{
			get
			{
				if (instance == null)
					instance = new LoggerManager();

				return instance;
			}
		}



		List<ILogger> loggers = new List<ILogger>();


		public LoggerManager(){
			this.AddLogger(new LoggerConsole());
		}

		public void AddLogger(ILogger logger)
		{
			if (loggers.Contains(logger))
				return;

			loggers.Add(logger);
		}

		public void RemoveLogger(ILogger logger)
		{
			loggers.Remove(logger);
		}


		public void Debug(object msg)
		{
			foreach (var logger in loggers)
				logger.Debug(msg);
		}
		public void Debug(string format, params Object[] args)
		{
			foreach (var logger in loggers)
				logger.Debug(format,args);
		}
		public void Info(object msg)
		{
			foreach (var logger in loggers)
				logger.Info(msg);
		}
		public void Info(string format, params Object[] args)
		{
			foreach (var logger in loggers)
				logger.Info(format,args);
		}
		public void Warn(object msg)
		{
			foreach (var logger in loggers)
				logger.Warn(msg);
		}
		public void Warn(string format, params Object[] args)
		{
			foreach (var logger in loggers)
				logger.Warn(format, args);
		}
		public void Error(object msg)
		{
			foreach (var logger in loggers)
				logger.Error(msg);
		}
		public void Error(string format, params Object[] args)
		{
			foreach (var logger in loggers)
				logger.Error(format, args);
		}
	}
}
