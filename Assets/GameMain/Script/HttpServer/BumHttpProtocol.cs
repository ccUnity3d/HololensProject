using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate IEnumerator DispatchProtocolDelegate(WWW www);

public enum BumProtocolType
{
    eProtocolType_POST,
    eProtocolType_GET,
    eProtocolType_POST_WITH_COOKIES,
};

public class BumProtocolInfo
{
    public DispatchProtocolDelegate dispatchProtocol;
    public BumProtocolType protocolType;
    public C2SProtocol protocol;
    public string url;
}

public class BumHttpProtocol
{
    public Dictionary<int, BumProtocolInfo> protocolCluster = new Dictionary<int, BumProtocolInfo>();
    public string serverURL = "http://139.219.105.140:9090/";

    public void send(C2SProtocol protocol, WWWForm postForm)
    {
        BumProtocolInfo pi = getProtocolInfo(protocol);
        doSend(pi, postForm);
    }

    void doSend(BumProtocolInfo protocolInfo, WWWForm postForm)
    {
        WWW www = null;
        
        switch(protocolInfo.protocolType)
        {
            case BumProtocolType.eProtocolType_GET: www = new WWW(serverURL + protocolInfo.url); break;
            case BumProtocolType.eProtocolType_POST: www = new WWW(serverURL + protocolInfo.url, postForm); break;
            case BumProtocolType.eProtocolType_POST_WITH_COOKIES:
                BumBase.Assert(BumLogic.clientUser.userConfig.cookieRequestHeader != null);
                www = new WWW(serverURL + protocolInfo.url, postForm.data, BumLogic.clientUser.userConfig.cookieRequestHeader);
                break;
        }
        
        BumApp.Instance.StartCoroutine(protocolInfo.dispatchProtocol(www));
    }

    public void registerProtocol(C2SProtocol protocol, BumProtocolType type,string url, DispatchProtocolDelegate dispatchCallBack)
    {
        BumProtocolInfo newProtocolInfo = new BumProtocolInfo();

        newProtocolInfo.protocol = protocol;
        newProtocolInfo.url = url;
        newProtocolInfo.protocolType = type;
        newProtocolInfo.dispatchProtocol = dispatchCallBack;

        protocolCluster.Add((int)protocol, newProtocolInfo);
    }

    public BumProtocolInfo getProtocolInfo(C2SProtocol protocol)
    {
        return protocolCluster[(int)protocol];
    }

    public void callHttpServerMethod(C2SProtocol protocol, WWWForm postForm)
    {
        send(protocol, postForm);
    }
}
