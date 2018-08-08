using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BumFactory
{
    public string uuid;
    public string name;
    public string infoUrl;
    public string thumbnailUrl;
    public bool isLoad;
    public List<string> zones = new List<string>();
    public string objId;

    public void deserializer(Dictionary<string ,object> dic)
    {
        foreach (string key in dic.Keys)
        {
            switch (key)
            {
                case "uuid": uuid = BumJsonTool.getStringValue(dic, key); break;
                case "name": name = BumJsonTool.getStringValue(dic,key); break;
                case "infoUrl": infoUrl = BumJsonTool.getStringValue(dic,key); break;
                case "zones": zones = BumJsonTool.getStringListValue(dic, key); break;
                case "objId": objId = BumJsonTool.getStringValue(dic, key); break;
                case "isLoad": isLoad = BumJsonTool.getBoolValue(dic, key); break;
                case "thumbnailUrl": thumbnailUrl = BumJsonTool.getStringValue(dic, key); break;
                default: Debug.LogWarning("没有找到对应的键"+ key); break;
            }
        }
    }

    public bool hasZone()
    {
        return (zones.Count > 0) ? true : false;
    }

    public bool hasClickZone(string uuid) {
        return zones.Contains(uuid);
    }
}
