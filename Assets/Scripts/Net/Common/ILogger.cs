using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public interface ILogger
	{
		void Debug(object msg);
		void Debug(string format, params Object[] args);
		void Info(object msg);
		void Info(string format, params Object[] args);
		void Warn(object msg);
		void Warn(string format, params Object[] args);
		void Error(object msg);
		void Error(string format, params Object[] args);
	}
}
