using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BumProduct 
{
    public string uuid;
    public string seekid;

    public bool isLoad;

    public string productName;
    public string size;//多少M
    public string thumbnailUrl;

    public string avdioUrl;//视频播放路径
    public List<string> options = new List<string>();//设备选项菜单
    public List<string> infosUrl = new List<string>();//详情表单

    public string objId;

    public void deserializer(Dictionary<string, object> dic)
    {
        foreach (string key in dic.Keys)
        {
            switch (key)
            {
                case "uuid":                    uuid = BumJsonTool.getStringValue(dic, key); break;
                case "seekid":                seekid = BumJsonTool.getStringValue(dic, key); break;
                case "productName":     productName = BumJsonTool.getStringValue(dic, key); break;
                case "isLoad":                isLoad = BumJsonTool.getBoolValue(dic, key); break;
                case "size":                    size = BumJsonTool.getStringValue(dic, key); break;
                case "thumbnailUrl":      thumbnailUrl = BumJsonTool.getStringValue(dic, key); break;
                case "avdioUrl":             avdioUrl = BumJsonTool.getStringValue(dic, key); break;
                case "options":               options = BumJsonTool.getStringListValue(dic, key); break;
                case "infosUrl":              infosUrl = BumJsonTool.getStringListValue(dic, key); break;
                case "objId":                   objId = BumJsonTool.getStringValue(dic, key); break;
                default: break;
            }
        }
    }
}
