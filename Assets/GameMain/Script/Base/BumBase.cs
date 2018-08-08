using System;
using System.IO;
using System.Diagnostics;

public class BumBase
{
    static bool m_bDebugBuild;
    static BumBase()
    {
        m_bDebugBuild = UnityEngine.Debug.isDebugBuild;
    }

    enum VLogType
    {
        NORMAL,
        WARNING,
        ERROR,
    }

    public static void Assert(bool result)
    {
        if (result)
            return;
        LogErrorWithStack("Assertion Failed!", 2);
        throw new Exception("Assert"); // 中断当前调用
    }

    public static void Assert(int result)
    {
        if (result != 0)
            return;
        LogErrorWithStack("Assertion Failed!", 2);
        throw new Exception("Assert"); // 中断当前调用
    }

    public static void Assert(Int64 result)
    {
        if (result != 0)
            return;
        LogErrorWithStack("Assertion Failed!", 2);
        throw new Exception("Assert"); // 中断当前调用
    }

    public static void Assert(object obj)
    {
        if (obj != null)
            return;
        LogErrorWithStack("Assertion Failed!", 2);
        throw new Exception("Assert"); // 中断当前调用
    }

    public static void Log(string log)
    {
        log = string.Format("[{0}] {1}\n\n===============================================================================\n\n", DateTime.Now.ToString("HH:mm:ss"), log);
        DoLog(log, VLogType.NORMAL);

        ServerLog(log);
    }

    public static void Log(string log, params object[] args)
    {
        Log(string.Format(log, args));
    }

    public static void Logs(params object[] logs)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < logs.Length; ++i)
        {
            sb.Append(logs[i].ToString());
            sb.Append(", ");
        }
        Log(sb.ToString());
    }

    public static void ServerLog(string log)
    {

    }

    public static void ServerLog(string log, params object[] args)
    {
        ServerLog(string.Format(log, args));
    }

    public static void LogErrorWithStack(string err = "", int stack = 1)
    {
        //StackFrame[] stackFrames = new StackTrace(true).GetFrames(); 
        //StackFrame sf = stackFrames[stack];
        //string log = string.Format("[ERROR]{0}:{1}\t{2}\t{3}\n", sf.GetFileName(), sf.GetFileLineNumber(), sf.GetMethod(), err);
        //Console.Write(log);
        //DoLog(log, VLogType.ERROR);
    }

    public static void LogError(string err, params object[] args)
    {
        LogErrorWithStack(string.Format(err, args), 1);
    }

    public static void LogError(object err, params object[] args)
    {
        LogErrorWithStack(string.Format((string)err, args), 1);
    }

    public static void LogWarning(string err, params object[] args)
    {
        string log = string.Format(err, args);
        DoLog(log, VLogType.WARNING);
    }

    public static void Pause()
    {
        UnityEngine.Debug.Break();
    }

    private static void DoLog(string szMsg, VLogType emType)
    {
        switch (emType)
        {
            case VLogType.NORMAL:
                UnityEngine.Debug.Log(szMsg);
                break;
            case VLogType.WARNING:
                UnityEngine.Debug.LogWarning(szMsg);
                break;
            case VLogType.ERROR:
                UnityEngine.Debug.LogError(szMsg);
                break;
        }

        LogToFile("game.log", szMsg);
    }

    public static void LogToFile(string logPath, string szMsg)
    {
        LogToFile(logPath, szMsg, true); // 默认追加模式
    }

    // 是否写过log file
    public static bool HasLogFile(string logFile)
    {
        if (m_bDebugBuild)
        {
            string fullPath = GetLogPath() + logFile;
            if (File.Exists(fullPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
    // 写log文件
    public static void LogToFile(string logFile, string szMsg, bool append)
    {
        //if (m_bDebugBuild)  //  开发者模式true:写log IO文件+响应服务器log
        //{
        //    string fullPath = GetLogPath() + logFile;
        //    string dir = Path.GetDirectoryName(fullPath);
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    using (StreamWriter writer = new StreamWriter(fullPath, append))  // Append
        //    {
        //        writer.Write(szMsg);
        //    }
        //}
    }

    // 用于写日志的可写目录
    public static string GetLogPath()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        string logPath = "logs/";
#else
		string logPath = UnityEngine.Application.persistentDataPath + "/" + "logs/";
#endif
        return logPath;
    }
}
