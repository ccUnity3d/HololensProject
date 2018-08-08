using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BumModel:ItemData
{
    public string uuid;
    public string modelName;
    public int PlacementSurfaces;
    public string modelUri;
    public string thumbnailUri;

    public void Deserializable(Dictionary<string, object> obj)
    {
        foreach (string key in obj.Keys)
        {
            switch (key)
            {
                case "uuid": uuid = BumJsonTool.getStringValue(obj, key); break;
                case "modelName": modelName = BumJsonTool.getStringValue(obj, key); break;
                case "PlacementSurfaces": PlacementSurfaces = BumJsonTool.getIntValue(obj, key); break;
                case "modelUri": modelUri = BumJsonTool.getStringValue(obj, key); break;
                case "thumbnailUri": thumbnailUri = BumJsonTool.getStringValue(obj, key); break;
                default:
                Debug.Log("no find key" + key);
                break;
            }
        }
    }
}
