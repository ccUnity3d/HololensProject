using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SimpleLoadState
{
    None,
    Loading,
    Failed,
    Success
}

public abstract class SimpleLoader : MyEventDispatcher
{
    public static MyEventDispatcher StaticEventDispatcher = new MyEventDispatcher();

    //一次加载的数量超出的存放到队列里面
    private static Stack<SimpleLoader> loaderStack = new Stack<SimpleLoader>();

    //一次加载的数量上限
    private static List<SimpleLoader> loaders = new List<SimpleLoader>();

    public const int Loadercount = 5;

    public object bringData;//携带的数据
    public string uri;//具体路径(本地路径)
    public Action<object> onloaded;//回调
    public Action<GameObject, object> onLoadedBeforClone;//通过数据来实例化物体
    public BumResourceType type;//加载类型
    public SimpleLoadState state;  //加载状态
    public object loadedData;//加载到的数据
    public bool canceled = false;//取消加载
    public bool justEndReturn = false;
    public float progress = 0;

    //使用WWW加载
    protected WWW www;
    // 完整的加载路径
    public virtual string url
    {
        get
        {
            return uri;
        }
    }

    public virtual string keyUrl
    {
        get
        {
            return url;
        }
    }
    public bool needClone
    {
        get
        {
            switch (type)
            {
                case BumResourceType.eBumResourceType_json:
                case BumResourceType.eBumResourceType_texture2D:
                default:
                    return false;
                case BumResourceType.eBumResourceType_assetBundle:
                    return true;
            }
        }
    }

    public ResourcePathManager resourcePath
    {
        get
        {
            return ResourcePathManager.Instance;
        }
    }

    public virtual void StartLoad()
    {
       
      
    }

    public void Loading()
    {
        if (string.IsNullOrEmpty(url))
        {
            Debug.LogErrorFormat("NeedLoadString.IsNullOrEmpty(url) == true");
            return;
        }
        state = SimpleLoadState.Loading;
        BumPoolManager.BumPoolData data;
        // 资源池里面已经存在的数据
        if (BumLogic.poolManager.tryGet(url,out data))
        {
            data.newUseTime = Time.realtimeSinceStartup;
            MyCallLater.Add(LoadInPool,0,data);
            return;
        }
        bool add = BumLogic.poolManager.addLoading(this);
        if (add)
        {
            if (loaders.Count < Loadercount)
            {
                loaders.Add(this);
                StartLoad();
            }
            else
            {
                loaderStack.Push(this);
            }
        }
    }

    public virtual void OnLoaded()
    {

        LoadNext();
        EndOnly();
        if (state == SimpleLoadState.Failed)
        {
            BumLogic.poolManager.loadError(this);
        }
        else
        {

        }
    }

    public void EndOnly()
    {
        progress = 1;
        //this.dispatchEvent();
        //this.dispatchEvent();
        if (canceled == false && onloaded !=null)
        {
            onloaded(this);
        }
    }

    protected void LoadNext()
    {
        loaders.Remove(this);
        if (loaderStack.Count>0)
        {
            SimpleLoader loader = loaderStack.Pop();
            loaders.Add(loader);
            loader.StartLoad();
        }
    }

    public static void RemoveLoader(SimpleLoader loader)
    {
        int loadIndex = loaders.IndexOf(loader);
        int waitingIndex = -1;
        if (waitingIndex !=-1)
        {
            loader.StopLoad();
        }
        else
        {
            waitingIndex = BumLogic.poolManager.waitingLoaderList.IndexOf(loader);
            if (waitingIndex !=-1)
            {
                BumLogic.poolManager.waitingLoaderList.RemoveAt(waitingIndex);
            }
        }
    }

    protected virtual void StopLoad()
    {

    }

    public void CancelLoad()
    {

    }

    protected void LoadProgress()
    {

    }

    private void LoadInPool(object data)
    {
        BumPoolManager.BumPoolData poolData = data as BumPoolManager.BumPoolData;
        if (poolData is BumPoolManager.BumPoolErrorData)
        {
            BumPoolManager.BumPoolErrorData errorData = poolData as BumPoolManager.BumPoolErrorData;
            errorData.errorCount++;
            this.state = SimpleLoadState.Failed;
            this.EndOnly();
            return;
        }

        #region PoolData
        switch (poolData.type)
        {
            case BumPoolDataType.eBumPoolDataType_byte:
                break;
            case BumPoolDataType.eBumPoolDataType_json:
                loadedData = poolData.resource;
                break;
            case BumPoolDataType.eBumPoolDataType_texture2D:
                loadedData = poolData.resource;
                break;
            case BumPoolDataType.eBumPoolDataType_prefab:
                BumPoolManager.BumPoolPrefabData prefabData = poolData as BumPoolManager.BumPoolPrefabData;
                loadedData = poolData.resource;
                if (canceled == false)
                {
                    loadedData = prefabData.getNew();
                }
                break;
            default:
                loadedData = null;
                Debug.LogWarning("没有此类型的池");
                break;
        }
        #endregion

        EndOnly();
    }


}
