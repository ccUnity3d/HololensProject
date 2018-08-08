using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SetTextureTool
{
    private static SetTextureTool instance;
    public static SetTextureTool Instance
    {
        get {
            if (instance == null) instance = new SetTextureTool();
            return instance;
        }
    }

    public static void SetTexture(GameObject go, string path, string isLocal)
    {
        if (string.IsNullOrEmpty(path))
        {
            //Debug.LogWarning("SetTexture(path = " + path+")");
            return;
        }
        Instance.setTexture(go, path, isLocal);
    }

    public void setTexture(GameObject go, string path, string isLocal)
    {
        BumResourceManager.loadResource(path, (object obj) => {
            Texture2D texture = (Texture2D)obj;
            RawImage image = go.GetComponent<RawImage>();
            if (image != null)
            {
                image.texture = texture;
                return;
            }
        }, null, BumResourceType.eBumResourceType_texture2D, BumLoadingType.eBumLoadingType_auto);
    }

    public static void SetTexture(RawImage go, string path, string isLocal)
    {
        Instance.setTexture(go, path, isLocal);
    }
    public void setTexture(RawImage go, string path, string isLocal)
    {
        if (go == null)
        {
            Debug.LogWarning("setTexture RawImage go == null");
            return;
        }
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogWarning("setTexture path == null");
            return;
        }

        BumResourceManager.loadResource(path, (object obj) =>
        {
            Texture2D texture = (Texture2D)obj;
            go.texture = texture;

        }, null, BumResourceType.eBumResourceType_texture2D, BumLoadingType.eBumLoadingType_auto,  BumResourcePoolType .None, go);
    }

}
