using System.Collections;
using UnityEngine;
using System;
using System.IO;

public class BumResourceLoader
{
     static BumResourcePool pool = new BumResourcePool();

    public BumResourceLoader()
    {

    }

    #region CreateCache

    private void createCache(string url, object data, BumResourceType resType)
    {
        switch (resType)
        {
            case BumResourceType.eBumResourceType_assetBundle: createAssetCache(url, data); break;
            case BumResourceType.eBumResourceType_texture2D: createTextureCache(url, data); break;
            case BumResourceType.eBumResourceType_json: createConfigCache(url, data); break;
            case BumResourceType.eBumResourceType_userInfo: createUserConfigCache(url, data); break;
        }
    }

    private void createAssetCache(string url, object data)
    {
        string filename = BumDefine.getFileByUrl(url);
        string filepath = BumDefine.bumAssetBundlePath + filename;
        FileInfo info = new FileInfo(filepath);
        if (info.Exists)
        {
            File.Delete(filepath);
        }
        else
        {
            Directory.CreateDirectory(filepath);
        }

        byte[] bytes = data as byte[];
        using (FileStream stream = File.Open(filepath + "/product", FileMode.OpenOrCreate))
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    private void createTextureCache(string url, object data)
    {
        string filename = BumDefine.getFileByUrl(url);
        string filepath = BumDefine.bumThumbnailPath + filename;
        FileInfo info = new FileInfo(filepath);
        if (info.Exists)
        {
            File.Delete(filepath + "/texture.png");
        }
        else
        {
            Directory.CreateDirectory(filepath);
        }

        Texture2D temp = (Texture2D)data;
        byte[] bytes = temp.EncodeToPNG();
        using (FileStream stream = File.Open(filepath + "/texture.png", FileMode.OpenOrCreate))
        {
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(bytes);
        }
    }
    
    private void createConfigCache(string url, object data)
    {
        string filename = BumDefine.getJsonFileByUrl(url);
        string filepath = BumDefine.bumDataConfigPath;
        FileInfo info = new FileInfo(filepath);
        if (info.Exists)
        {
            Debug.LogWarning(filepath + BumDefine.getJsonFileByUrl(url) + ".json");
            File.Delete(filepath + BumDefine.getJsonFileByUrl(url)+".json");
        }
        else
        {
            Directory.CreateDirectory(filepath);
            //Debug.LogWarning("CreateDirectory");
        }
        string json = data as string;

        //using (StreamWriter writer = new StreamWriter(filepath + filename, true, System.Text.Encoding.UTF8))
        //{
        //    writer.WriteLine(json);
        //}
    }
    private void createUserConfigCache(string url, object data)
    {
        string filename = BumDefine.getJsonFileByUrl(url);
        string filepath = BumDefine.bumUserConfigPath;
        FileInfo info = new FileInfo(filepath);
        if (info.Exists)
        {
            File.Delete(filepath + BumDefine.getJsonFileByUrl(url) + ".json");
        }
        else
        {
            Directory.CreateDirectory(filepath);
            //Debug.LogWarning("CreateDirectory");
        }
        string json = data as string;

        //using (StreamWriter writer = new StreamWriter(filepath + filename, false, System.Text.Encoding.UTF8))
        //{
        //    writer.WriteLine(json);
        //}
    }
    #endregion

    #region CheckCache
    private bool checkLocal(string url)
    {
        return !url.Contains("alluserdata") && !url.Contains(".com");
    }

    private bool checkCache(string url, BumResourceType resType)
    {
        bool exist = false;
        switch (resType)
        {
            case BumResourceType.eBumResourceType_assetBundle: exist = checkAssetCache(url); break;
            case BumResourceType.eBumResourceType_texture2D: exist = checkTextureCache(url); break;
            case BumResourceType.eBumResourceType_json: exist = checkConfigCache(url); break;
            case BumResourceType.eBumResourceType_userInfo: exist = checkUserConfigCache(url); break;
        }

        return exist;
    }

    private bool checkAssetCache(string url)
    {
        string filename = BumDefine.getFileByUrl(url);
        string filepath = BumDefine.bumAssetBundlePath + filename+"/product";
        FileInfo file = new FileInfo(filepath);
        return file.Exists;
    }
    private bool checkTextureCache(string url)
    {
        string filename = BumDefine.getFileByUrl(url);
        string filepath = BumDefine.bumThumbnailPath + filename+"/texture";
        FileInfo file = new FileInfo(filepath);
        return file.Exists;
    }
    private bool checkConfigCache(string url)
    {
        string filename = BumDefine.getJsonFileByUrl(url);
        string filepath = BumDefine.bumDataConfigPath + filename;
        //Debug.LogWarning(filepath);
        FileInfo file = new FileInfo(filepath);
        return file.Exists;
    }
    private bool checkUserConfigCache(string url)
    {
        string filename = BumDefine.getJsonFileByUrl(url);
        string filepath = BumDefine.bumUserConfigPath + filename;
        FileInfo file = new FileInfo(filepath);
        return file.Exists;
    }
    #endregion

    private bool checkPoolData(string url, Action<object> onloaded, Action<object> progressEvent, BumResourceType resType = BumResourceType.eBumResourceType_assetBundle, BumResourcePoolType resourcePoolType = BumResourcePoolType.ProductObject, object param = null, Action<GameObject, object> beforeClone = null)
    {
        return pool.TryGet(url, onloaded, progressEvent, resType, resourcePoolType, param, beforeClone);
    }

    //private void  recyclePoolData(string url,GameObject gameobject, BumResourcePoolType resourcePoolType)
    //{
    //    pool.recyclePoolData(url,gameobject,resourcePoolType);
    //}

    public void auto(string url, Action<object> onloaded, Action<object> progressEvent, BumResourceType resType = BumResourceType.eBumResourceType_assetBundle,BumResourcePoolType resourceType = BumResourcePoolType.ProductObject, object param = null, Action<GameObject, object> beforeClone = null)
    {
        if (checkLocal(url))
        {
            loadFromLocal(url, onloaded);
        }

        loadFromWWW(url, onloaded, progressEvent, resType, resourceType, param, beforeClone);
    }

    //Resource.Load方式加载
    public void loadFromLocal(string url, Action<object> onloaded)
    {

    }

    //unity封装www加载resources
    public void loadFromWWW(string url, Action<object> onloaded, Action<object> progressEvent, BumResourceType resType = BumResourceType.eBumResourceType_assetBundle,BumResourcePoolType resourceTyep = BumResourcePoolType.ProductObject, object param = null, Action<GameObject, object> beforeClone = null)
    {

        //if (checkPoolData( url, onloaded, null, resType, resourceTyep, param, beforeClone)) return;

        bool existAssetCache = checkCache(url, resType);

        switch (resType)
        {
            case BumResourceType.eBumResourceType_assetBundle: BumApp.Instance.StartCoroutine(loadAssetBundleFormWWW(url, onloaded, progressEvent, existAssetCache,param,beforeClone)); break;
            case BumResourceType.eBumResourceType_texture2D: BumApp.Instance.StartCoroutine(loadTexture2DFromWWW(url, onloaded, progressEvent, existAssetCache)); break;
            case BumResourceType.eBumResourceType_json: BumApp.Instance.StartCoroutine(loadJsonFromWWW(url, onloaded, progressEvent, false)); break;
            case BumResourceType.eBumResourceType_userInfo: BumApp.Instance.StartCoroutine(loadUserInfoJsonFromWWW(url, onloaded, progressEvent, existAssetCache)); break;
            default: break;
        }
    }

    private IEnumerator loadTexture2DFromWWW(string url, Action<object> onloaded, Action<object> progressEvent, bool existAssetCache = false)
    {
        string path = url;
        if (existAssetCache)
            path = BumDefine.bumThumbnailPathFile + BumDefine.getFileByUrl(url)+"/texture";
        WWW www = new WWW(path);
        while (!www.isDone)
        {
            if(progressEvent != null)   progressEvent(www.progress);
            yield return null;
        }

        do
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                BumBase.Log(www.error);
                break;
            }

            Texture2D texture = www.texture;
            if (texture != null)
            {
                //texture = bundle.LoadAsset<Texture2D>("texture");
                if (!existAssetCache) createCache(url, texture, BumResourceType.eBumResourceType_texture2D);
                //bundle.Unload(false);
            }
            else
            {
                texture = www.texture;

                //pool.addData(path, texture, BumResourceType.eBumResourceType_texture2D);

                //if (!existAssetCache) createCache(url, texture, BumResourceType.eBumResourceType_texture2D);
            }

            onloaded(texture);

        } while (false);
        
        www.Dispose();
        www = null;
    }

    private IEnumerator loadJsonFromWWW(string url, Action<object> onloaded, Action<object> progressEvent, bool existAssetCache = false)
    {
        string path = url;

        if (existAssetCache)
            path = BumDefine.bumDataConfigPathFile + BumDefine.getJsonFileByUrl(url);

        WWW www = new WWW(url);
        while (!www.isDone)
        {
            if (progressEvent != null) progressEvent(www.progress);
            //yield return www;
        }

        do
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                BumBase.Log(www.error);
                break;
            }
            string json = System.Text.Encoding.UTF8.GetString(www.bytes);
            int index = json.IndexOf("[");
            if (index > 0 && index < 2) json = json.Substring(index);
            if (!existAssetCache) createCache(url, json, BumResourceType.eBumResourceType_json);
            Debug.Log(json);
            onloaded(json);


        } while (false);
        www.Dispose();
        www = null;
        yield return null;
    }

    private IEnumerator loadUserInfoJsonFromWWW(string url, Action<object> onloaded, Action<object> progressEvent, bool existAssetCache = false)
    {
        string path = url;

        if (existAssetCache)
            path = BumDefine.bumUserConfigPathFile + BumDefine.getJsonFileByUrl(url);
        WWW www = new WWW(path);
        while (!www.isDone)
        {
            if (progressEvent != null) progressEvent(www.progress);
            yield return null;
        }

        do
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                BumBase.Log(www.error);
                break;
            }
            string json = System.Text.Encoding.UTF8.GetString(www.bytes);
            if (!existAssetCache) createCache(url, json, BumResourceType.eBumResourceType_userInfo);

            onloaded(json);


        } while (false);
        www.Dispose();
        www = null;
    }

    private IEnumerator loadAssetBundleFormWWW(string url, Action<object> onloaded, Action<object> progressEvent, bool existAssetCache = false, object param = null, Action<GameObject, object> beforeClone = null)
    {

        string path = url;

        if (existAssetCache)
            path = BumDefine.bumAssetBundlePathFile + BumDefine.getFileByUrl(url)+"/product";

        WWW www = new WWW(path);
        while (!www.isDone)
        {
            if (progressEvent != null) progressEvent(www.progress);
            yield return null;
        }

        do
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                BumBase.Log(www.error);
                break;
            }

            AssetBundle bundle = www.assetBundle;
            if (bundle == null) break;

            UnityEngine.Object[] objs = bundle.LoadAllAssets();

            for (int i = 0; i < objs.Length; i++)
            {
                if (!objs[i].GetType().Equals(typeof(GameObject))) continue;
                //if (!objs[i].name.Equals("product")) continue;

                GameObject data = objs[i] as GameObject;
                data.name = BumDefine.getIdByUrl(url);

                //pool.addData(path,data, BumResourceType.eBumResourceType_assetBundle);

                //if (!existAssetCache) createCache(url,www.bytes,BumResourceType.eBumResourceType_assetBundle);
                if (beforeClone != null)    beforeClone(data, param);
                if (onloaded != null) onloaded(data);
            }

            bundle.Unload(false);

        } while (false) ;
        www.Dispose();
        www = null;
    }
}
