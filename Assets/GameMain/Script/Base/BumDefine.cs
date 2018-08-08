using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class BumDefine:MySingleton<BumDefine>
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBPLAYER ||UNITY_WSA
    public static readonly string bumpersistentPath =  Application.persistentDataPath + "/";
#elif UNITY_ANDROID   //安卓
    public static readonly string bumpersistentPath =  Application.persistentDataPath + "/";
#elif UNITY_IPHONE  //iPhone
    public static readonly string bumpersistentPath =  Application.persistentDataPath + "/";
#else
    public static readonly string bumpersistentPath = string.Empty;
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBPLAYER||UNITY_WSA
    public static readonly string bumpersistentPathFile = "file:///" + Application.persistentDataPath + "/";
#elif UNITY_ANDROID   //安卓
    public static readonly string bumpersistentPathFile = "file://" + Application.persistentDataPath + "/";
#elif UNITY_IPHONE  //iPhone
    public static readonly string bumpersistentPathFile = "file://" + Application.persistentDataPath + "/";
#else
    public static readonly string bumpersistentPathFile = string.Empty;
#endif
  
    public static readonly string bumUserConfigPath = bumpersistentPath + "alluserData/UserConfig/";
    public static readonly string bumAssetBundlePath = bumpersistentPath + "alluserdata/BuMie/Assetbundle/";
    public static readonly string bumDataConfigPath = bumpersistentPath + "alluserdata/BuMie/Project/";
    public static readonly string bumThumbnailPath = bumpersistentPath + "alluserdata/BuMie/Thumbnail/";
    //public static readonly string bumUserPath = bumpersistentPathFile + "alluserdata/UserInfo/";

   
    public static readonly string bumUserConfigPathFile = bumpersistentPathFile + "alluserData/UserConfig/";
    public static readonly string bumAssetBundlePathFile = bumpersistentPathFile + "alluserdata/BuMie/Assetbundle/";
    public static readonly string bumDataConfigPathFile = bumpersistentPathFile + "alluserdata/BuMie/Project/";
    public static readonly string bumThumbnailPathFile = bumpersistentPathFile + "alluserdata/BuMie/Thumbnail/";
    //public static readonly string bumUserPathFile = bumpersistentPathFile + "alluserdata/UserInfo/";

 
    public static string getIdByUrl(string url)
    {
        string id = "";
        string[] strs = url.Split('/');
        if (strs.Length > 1)
        {
            string temp = strs[strs.Length - 1];
            string[] strss = temp.Split('.');
            if (strss.Length > 1) id = strss[0];
            else id = temp;
        }
        else
        {
            id = url;
        }
        return id;
    }

    public static string getFileByUrl(string url)
    {
        string file = "";
        string[] strs = url.Split('/');
        if (strs.Length > 1)
        {
            file = strs[strs.Length - 2];
        }
        else
        {
            file = url;
        }
        return file;
    }

    public static string getJsonFileByUrl(string url)
    {
        string file = "";
        string[] strs = url.Split('/');
        if (strs.Length > 1)
        {
            file = strs[strs.Length - 1];
        }
        else
        {
            file = url;
        }
        return file;
    }
    public static readonly float uiAutoHideDelay =1.5f;

    public static bool isDebug = false;

    private static bool IsTangoDevice;
    public static bool isTangoDevice
    {
        get
        {
            return SystemInfo.deviceModel.Contains("Lenovo") && SystemInfo.deviceModel.Contains("PB2");
        }
    }

    private static string CapturePath;
    public static string capturePath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (isTangoDevice)
                {
                    CapturePath = "/sdcard/Sanpu屏幕截图";
                }
                else
                {
                    CapturePath = "/sdcard/DCIM/Camera";
                }
                if (!Directory.Exists(CapturePath))
                {
                    Directory.CreateDirectory(CapturePath);
                }

            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                CapturePath = Application.persistentDataPath;
            }
            return CapturePath;
        }
    }

    public Transform poolManager
    {
        get
        {
            return GameObject.Find("3DScene/PoolManager").transform;
        }
    }

    public Transform factoryObject
    {
        get
        {
            return GameObject.Find("3DScene/PoolManager/FactoryObject").transform;
        }
    }

    public Transform zoneObject
    {
        get
        {
            return GameObject.Find("3DScene/PoolManager/ZoneObject").transform;
        }
    }

    public Transform productObject
    {
        get
        {
            return GameObject.Find("3DScene/PoolManager/ProductObject").transform;
        }
    }

    private static string CaptureFileName;

    public static string captureFileName
    {
        get
        {
            System.DateTime now = new System.DateTime();
            now = System.DateTime.Now;
            return string.Format("image{0}{1}{2}{3}.png", now.Day, now.Hour, now.Minute, now.Second);
        }
    }

    public static GameObject currentModelForSlam;

}
