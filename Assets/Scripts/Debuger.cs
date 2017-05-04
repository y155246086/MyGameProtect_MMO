using UnityEngine;
using System.Collections;

public class Debuger {

    public static bool isLog = true;
    public static void Log(object message)
    {
        if (isLog == false)
            return;
        Debug.Log(message);
    }

    public static void Log(object message, Object context)
    {
        if (isLog == false)
            return;
        Debug.Log(message, context);
    }
    public static void LogError(object message)
    {
        if (isLog == false)
            return;
        Debug.LogError(message);
    }

    public static void LogError(object message, Object context)
    {
        if (isLog == false)
            return;
        Debug.LogError(message, context);
    }

    public static void LogWarning(object message)
    {
        if (isLog == false)
            return;
        Debug.LogWarning(message);
    }

    public static void LogWarning(object message, Object context)
    {
        if (isLog == false)
            return;
        Debug.LogWarning(message);
    }
}
