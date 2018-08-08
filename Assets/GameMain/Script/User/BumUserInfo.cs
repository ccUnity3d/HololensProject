using System.Collections.Generic;
using System.IO;

public class BumUserInfo
{
    public string userId = null;
    public string userPhone = "22514";
    public string createTime = null;
    public string loginStatus = null;

    public Dictionary<string, string> cookieRequestHeader;
    public List<string> uuidModel = new List<string>();

    public BumUserInfo()
    {

    }

    public void setCookies(string cookies) 
    {
        if (cookieRequestHeader == null)
            cookieRequestHeader = new Dictionary<string, string>();
        else
            cookieRequestHeader.Clear();
        
        cookieRequestHeader.Add("Cookie", cookies);
    }
    public void setUserId(string id) { userId = id; }
    public void setUserPhone(string phone) { userPhone = phone; }
    public void setStatus(string status) { loginStatus = status; }

    public void Deserializer(Dictionary<string, object> dic)
    {
        foreach (string key in dic.Keys)
        {
            switch (key)
            {
                case "createdTime":
                    createTime = BumJsonTool.getStringValue(dic, key);
                    break;
                case "userId":
                    userId = BumJsonTool.getStringValue(dic, key);
                    break;
                case "status":
                    loginStatus = BumJsonTool.getStringValue(dic, key);
                    break;
                case "userPhone":
                    userPhone = BumJsonTool.getStringValue(dic, key);
                    BumBase.LogWarning(userPhone);
                    break;
                case "uuidModel":
                    uuidModel = BumJsonTool.getStringListValue(dic,key);
                    break;
                default:
                    break;
            }
        }
    }

    public bool hasCachedModel(string uuid)
    {
        if (uuidModel.Count == 0) return false;
        return uuidModel.Contains(uuid);
    }

    public void init()
    {
        FileInfo info = new FileInfo(BumDefine.bumUserConfigPath+ BumLogic.clientUser.userLocalRecord.userPhone+".json");
        if (!info.Exists)
        {
            return;
        }
        string url = BumDefine.bumUserConfigPathFile + BumLogic.clientUser.userLocalRecord.userPhone + ".json";
        BumResourceManager.loadResource(url, (object data) =>
        {
            string json = data.ToString();
            if (json ==null)
            {
                return;
            }
            object jsonData = BumJsonTool.FromJson(json);
           
            Deserializer(jsonData as Dictionary<string, object>);
        },  null,BumResourceType.eBumResourceType_userInfo);
    }
    
}
