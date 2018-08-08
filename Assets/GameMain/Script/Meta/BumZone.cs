using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BumZone
{
    public string uuid;
    public string name;
    public string thumbnailUrl;
    public string infoUrl;
    public List<string> products = new List<string>();

    public string objId;

    public void deserializer(Dictionary<string ,object> dic)
    {
        foreach (string key in dic.Keys)
        {
            switch (key)
            {
                case "uuid": uuid = BumJsonTool.getStringValue(dic, key); break;
                case "name": name = BumJsonTool.getStringValue(dic,key); break;
                case "thumbnailUrl": thumbnailUrl = BumJsonTool.getStringValue(dic, key); break;
                case "infoUrl": infoUrl = BumJsonTool.getStringValue(dic, key); break;
                case "products": products = BumJsonTool.getStringListValue(dic, key); break;
                case "objId": objId = BumJsonTool.getStringValue(dic, key); break;
                default: break;
            }
        }
    }

    public bool hasClickProduct(string uuid) {
        return products.Contains(uuid);
    }
}
