using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class BumMD5HashFile 
{
    public static string GetMD5Hash(string fileName)
    {
        string LOCAL_RES_PATH = null;
#if UNITY_ANDROID
        LOCAL_RES_PATH = Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
        LOCAL_RES_PATH = Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        LOCAL_RES_PATH = Application.dataPath + "/StreamingAssets/";
#endif

        string pathName = string.Format("{0}{1}", LOCAL_RES_PATH, fileName);

        string strResult = "";
        string strHashData = "";

        byte[] arrbytHashValue;
        System.IO.FileStream oFileStream = null;

        System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                   new System.Security.Cryptography.MD5CryptoServiceProvider();

        oFileStream = new System.IO.FileStream(pathName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
        arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
        oFileStream.Dispose();
        strHashData = System.BitConverter.ToString(arrbytHashValue);
        strHashData = strHashData.Replace("-", "");
        strResult = strHashData;

        return strResult;
    }
}
