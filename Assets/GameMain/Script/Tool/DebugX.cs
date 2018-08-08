using UnityEngine;
using System;
using System.Text;

#if !UNITY_WEBPLAYER
using System.IO;
#endif

public interface ILog
    {
        void log(string msg);
    }

/// <summary>
/// 调试辅助类.
/// </summary>
public class DebugX
{
    public static bool enabled = true;

#if !UNITY_WEBPLAYER
    private static string outLogPath = "";
#endif

    public static void UnhandledExceptionInit()
    {
        Application.logMessageReceived += handleException;
        Application.logMessageReceivedThreaded += handleException;

#if !UNITY_WEBPLAYER
        outLogPath = Application.persistentDataPath + "/log.txt";
        if (File.Exists(outLogPath) == false)
        {
            File.CreateText(outLogPath);
        }
        else
        {
            FileInfo fileInfo = new FileInfo(outLogPath);

            if (fileInfo.Length > 2 * 1024 * 1024)
            {
                File.Delete(outLogPath);
                File.CreateText(outLogPath);
            }
        }
#endif
    }

    private static void handleException(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            LogError("{0}:{1}", condition, stackTrace);
        }
    }


    public static void Log(object message, params object[] args)
    {
        if (!DebugX.enabled || message == null)
        {
            return;
        }
        string msg = String.Format(message.ToString(), args);
        logToFile(msg);
        Debug.Log(msg);
    }

    public static void LogWarning(object message, params object[] args)
    {
        if (!DebugX.enabled || message == null)
        {
            return;
        }
        string msg = String.Format(message.ToString(), args);
        logToFile(msg);
        Debug.LogWarning(msg);
    }

    public static void LogError(string message, params object[] args)
    {
        if (!DebugX.enabled || message == null)
        {
            return;
        }

        string msg = String.Format(message.ToString(), args);
        logToFile(msg);
        postToServer(msg);
        Debug.LogError(msg);
    }

    private static void logToFile(string msg)
    {
#if !UNITY_WEBPLAYER
        //try
        //{
        //    using (StreamWriter writer = new StreamWriter(outLogPath, true, Encoding.UTF8))
        //    {
        //        writer.WriteLine(msg);
        //    }
        //}
        //catch (Exception)
        //{
        //}
#endif
    }

    private static void postToServer(string msg)
    {
        
    }
}
