using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理工程中所有读写路径
/// </summary>
public class ResourcePathManager : MySingleton<ResourcePathManager> {



        public   string StreamingAssetURL =
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_WINRT_10_0 || UNITY_WSA_10_0 || WINDOWS_UWP
        "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID   //安卓
        "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE  //iPhone
        "file://" + Application.dataPath + "/Raw/";
#else
                string.Empty;
#endif

    public readonly string platform =
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBPLAYER
        "UWP/";
#elif UNITY_ANDROID   //安卓
        "Android/";
#elif UNITY_IPHONE  //iPhone
        "IOS/";
//#elif UNITY_WINRT_10_0 || UNITY_WSA_10_0 // WINDOWS_UWP //
////        "WSAP"
#else
        "WSAP";
#endif

    //可写的，持久存储的路径//
    public static string getMyPersistentPath(string name)
    {
        string path = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.persistentDataPath + "/" + name;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            //path = Application.persistentDataPath + "/" + name;
            path = Application.temporaryCachePath + "/" + name;
        }
        else
        {
            path = Application.dataPath + "/" + name;
        }
        return path;
    }

    // streamingAsset 路径
    public static string getMyStreamingAssetsPath(string name)
    {
        string path = "";
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.dataPath + "/Raw/" + name;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.temporaryCachePath + "/" + name;
        }
        else
        {
            path = Application.streamingAssetsPath + "/" + name;
        }
        return path;
    }




}
