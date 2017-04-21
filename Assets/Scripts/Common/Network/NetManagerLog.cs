using System;
using System.Collections.Generic;
using Common;

public class NetManagerLog :ILogger
{
    public void Debug(string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void Debug(object msg)
    {
        UnityEngine.Debug.Log(msg.ToString());
    }

    public void Error(string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void Error(object msg)
    {
        throw new NotImplementedException();
    }

    public void Info(string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void Info(object msg)
    {
        throw new NotImplementedException();
    }

    public void Warn(string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void Warn(object msg)
    {
        throw new NotImplementedException();
    }
}
