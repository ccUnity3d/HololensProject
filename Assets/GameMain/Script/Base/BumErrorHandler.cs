using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ErrorDelegate(string tips);

public class BumErrorInfo
{
    public string ErrorCode;
    public string ErrorID;
    public string ErrorTips;

    public ErrorDelegate ErrorHandler;
}

public class BumErrorHandler 
{
    Dictionary<string, BumErrorInfo> ErrorCodeMap = new Dictionary<string,BumErrorInfo>();
    Dictionary<string, BumErrorInfo> ErrorIdMap = new Dictionary<string,BumErrorInfo>();

    public void init()
    {
        //000
        //RegistErrorHandler("ERR_SERVER_MAINTENANCE", "000001", "维护模式", DelegateQuit);
    }

    void RegistErrorHandler(string code, string id, string tips, ErrorDelegate func)
    {
        BumErrorInfo info = new BumErrorInfo();
        info.ErrorCode = code;
        info.ErrorID = id;
        info.ErrorTips = tips;
        info.ErrorHandler = func;

        ErrorCodeMap.Add(code, info);
        ErrorIdMap.Add(id, info);
    }

    public void parseErrorCode(string id)
    {
        if (!ErrorCodeMap.ContainsKey(id))
        {
            string message = string.Format("can not parse the error code: {0}", id);
            BumBase.LogError(message);
            return;
        }

        BumErrorInfo info = null;
        ErrorCodeMap.TryGetValue(id, out info);
        if (info == null) return;

        info.ErrorHandler(info.ErrorTips);
    }
}
