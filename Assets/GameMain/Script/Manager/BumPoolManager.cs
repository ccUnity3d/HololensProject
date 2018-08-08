using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum BumPoolDataType
{
    eBumPoolDataType_byte,
    eBumPoolDataType_json,
    eBumPoolDataType_texture2D,
    eBumPoolDataType_prefab
}

public class BumPoolManager
{
    public Dictionary<string, BumPoolData> poolDic = new Dictionary<string, BumPoolData>();
    public List<string> loadingkeyUrlList = new List<string>();
    public List<SimpleLoader> waitingLoaderList = new List<SimpleLoader>();

    public BumPoolManager()
    {

    }

    public void init()
    {

    }

    public bool tryGet(string key, out BumPoolData value)
    {
        return poolDic.TryGetValue(key, out value);
    }

    public bool addLoading(SimpleLoader loader)
    {
        for (int i = 0; i < loadingkeyUrlList.Count; i++)
        {
            if (loadingkeyUrlList[i] == loader.keyUrl)
            {
                waitingLoaderList.Add(loader);
                return false;
            }
        }
        loadingkeyUrlList.Add(loader.keyUrl);
        return true;
    }

    public bool removeLoading(SimpleLoader loader)
    {
        for (int i = 0; i < loadingkeyUrlList.Count; i++)
        {
            if (loadingkeyUrlList[i] == loader.keyUrl)
            {
                loadingkeyUrlList.RemoveAt(i);
                for (int k = 0; k < waitingLoaderList.Count; k++)
                {
                    SimpleLoader item = waitingLoaderList[k];
                    if (item.keyUrl == loader.keyUrl)
                    {
                        item.state = loader.state;
                        if (item.needClone == false)
                        {
                            item.loadedData = loader.loadedData;
                        }
                        else
                        {
                            BumPoolData data;
                            if (poolDic.TryGetValue(loader.keyUrl,out data))
                            {
                                if (data is BumPoolPrefabData)
                                {
                                    if (item.canceled == false && item.onloaded !=null)
                                    {
                                        item.loadedData = (data as BumPoolPrefabData).getNew();
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("pool.TryGetValue == false keyUrl =" + loader.keyUrl);
                            }
                        }
                        item.EndOnly();
                        waitingLoaderList.RemoveAt(k);
                        k--;
                    }
                }
                return true;
            }
        }
        return false;
    }

    private void remove(string keyUrl)
    {
        if (poolDic.ContainsKey(keyUrl))
        {
            poolDic.Remove(keyUrl);
        }
    }

    public void loadError(SimpleLoader loader)
    {
        addError(loader.keyUrl);
        for (int i = 0; i < loadingkeyUrlList.Count; i++)
        {
            if (loadingkeyUrlList[i] == loader.keyUrl)
            {
                loadingkeyUrlList.RemoveAt(i);
                for (int k = 0; k < waitingLoaderList.Count; k++)
                {
                    SimpleLoader item = waitingLoaderList[k];
                    if (item.keyUrl == loader.keyUrl)
                    {
                        item.state = loader.state;
                        item.EndOnly();
                        waitingLoaderList.RemoveAt(k);
                        k--;
                    }
                }
            }
        }
        MyCallLater.Add(1, removeError, loader.keyUrl);
    }

    public BumPoolErrorData addError(string keyUrl)
    {
        BumPoolErrorData data;
        if (poolDic.ContainsKey(keyUrl))
        {
            data = poolDic[keyUrl] as BumPoolErrorData;
            data.errorCount++;
        }
        else
        {
            data = new BumPoolErrorData(keyUrl);
            data.errorCount = 1;
            poolDic.Add(keyUrl, data);
        }
        return data;
    }

    public void removeError(object data)
    {
        string keyUrl = data.ToString();
        BumPoolErrorData errorData;
        if (poolDic.ContainsKey(keyUrl))
        {
            errorData = poolDic[keyUrl] as BumPoolErrorData;
            if (errorData !=null )
            {
                poolDic.Remove(keyUrl);
            }
        }
    }

    public void addTexture(string keyUrl, object resource, bool DonnotClear = false )
    {
        if (poolDic.ContainsKey(keyUrl))
        {
            if (poolDic[keyUrl] is BumPoolErrorData)
            {
                poolDic.Remove(keyUrl);
            }
            else
            {
                Debug.Log("重复添加："+keyUrl);
                return;
            }
        }
        BumPoolTextureData loadPoolData = new BumPoolTextureData(keyUrl,resource,DonnotClear);
        poolDic.Add(keyUrl,loadPoolData);
    }

    public void addJson(string keyUrl,object resource,bool DonnotClear = false)
    {
        if (poolDic.ContainsKey(keyUrl))
        {
            if (poolDic[keyUrl] is BumPoolErrorData)
            {
                poolDic.Remove(keyUrl);
            }
            else
            {
                Debug.Log("重复添加");
                return;
            }
        }
        BumPoolJsonData jsonLoadData = new BumPoolJsonData(keyUrl,resource,DonnotClear);
        poolDic.Add(keyUrl, jsonLoadData);
    }

    public BumPoolPrefabData addPrefab(string keyUrl,object resource,bool DonnotClear = false)
    {
        if (poolDic.ContainsKey(keyUrl))
        {
            if (poolDic[keyUrl] is BumPoolErrorData)
            {
                poolDic.Remove(keyUrl);
                Debug.Log(" ");
            }
            else
            {
                Debug.Log("重复添加"+keyUrl);
                return poolDic[keyUrl] as BumPoolPrefabData;
            }
        }
        BumPoolPrefabData prefabLoadData = new BumPoolPrefabData(keyUrl,resource,DonnotClear);
        poolDic.Add(keyUrl,prefabLoadData);
        return prefabLoadData;
    }

    public void dispose(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Dispos target == null");
            return;
        }
        int count = 0;
        int id = target.GetInstanceID();
        foreach (BumPoolData item in poolDic.Values)
        {
            if (item.type == BumPoolDataType.eBumPoolDataType_texture2D)
            {
                BumPoolTextureData textureData = item as BumPoolTextureData;
                if (textureData.remove(id))
                {
                    count++;
                    //texture是引用不能销毁物体  如需销毁在外部自己做此处不应提供if (target != null) GameObject.Destroy(target);
                }
            }
            if (item.type == BumPoolDataType.eBumPoolDataType_prefab)
            {
                BumPoolPrefabData prefabData = item as BumPoolPrefabData;
                if (prefabData.remove(id))
                {
                    count++;
                    //只有prefab实例会销毁  
                    if (target != null) GameObject.Destroy(target);
                }
            }
        }

        //foreach (LoadPoolData item in pool.Values)
        //{
        //    if (item.type == PoolDataType.Prefab)
        //    {
        //        PrefabData prefabData = item as PrefabData;
        //        prefabData.Remove(id);
        //        count++;
        //        只有prefab实例会销毁
        //        GameObject.Destroy(target);
        //    }
        //}

        if (count == 0)
        {
            Debug.LogWarning("Dispos未找到" + target.name);
        }
    }

    public abstract class BumPoolData
    {
        public const float waitingTime = 30f;   //当资源池没有数据的时候等待30f移除一个资源池
        public string keyUrl;       //资源加载路径
        public object resource;     //加载到的资源
        public float laodedTime;    //加载使用的时间
        private float _newUseTime;  //最近一次 的时间
        public float newRemoveTime; //最近移除 的时间
        public BumPoolDataType type;   //资源此类型
        public bool donotClear;     //不可以清除

        //最近一次使用时间
        public float newUseTime
        {
            get
            {
                return _newUseTime;
            }
            set
            {
                _newUseTime = value;
                onNewUseTime();
            }
        }
        
        public virtual void onNewUseTime()
        {

        }
        //重置
        public abstract void dispose();
        //清除
        public virtual bool clear()
        {
            if (resource == null) return false;
            if (donotClear == true) return false;
            dispose();
            return true;
        }

        protected virtual void onNewUse()
        {

        }
    }

    public class BumPoolErrorData : BumPoolData
    {
        public const int maxErrorCount = 3;
        public int errorCount = 0;

        public BumPoolErrorData(string keyUrl)
        {
            this.keyUrl = keyUrl;
        }

        public override void dispose()
        {

        }
    }

    public class BumPoolPrefabData : BumPoolData
    {
        public UnityEngine.Object prefab;
        public Dictionary<int, GameObject> targetDic = new Dictionary<int, GameObject>();

        public BumPoolPrefabData(string keyUrl, object resource, bool donotClear = false)
        {
            type = BumPoolDataType.eBumPoolDataType_prefab;
            this.laodedTime = Time.realtimeSinceStartup;
            this.newUseTime = this.laodedTime;
            this.keyUrl = keyUrl;
            this.resource = resource;
            this.donotClear = donotClear;
            prefab = resource as UnityEngine.Object;
        }

        public object getNew()
        {
            Debug.LogWarning("GameObject.Instantiate " + keyUrl);

            GameObject newObj = GameObject.Instantiate(prefab) as GameObject;
            targetDic.Add(newObj.GetInstanceID(),newObj);
            return newObj;
        }

        public override void dispose()
        {
            GameObject.DestroyImmediate(prefab,true);
            Resources.UnloadUnusedAssets();
        }

        public bool remove(int id)
        {
            if (targetDic.ContainsKey(id))
            {
                newRemoveTime = Time.realtimeSinceStartup;
                if (targetDic[id] != null)
                {
                    GameObject.DestroyImmediate(targetDic[id],true);
                }
                targetDic.Remove(id);
                if (targetDic.Count == 0)
                {
                    MyCallLater.Add(waitingTime,removeFromResourcePool);
                }
                return true;
            }
            return false;
        }

        public void removeFromResourcePool()
        {
            if (targetDic.Count>0)
            {
                return;
            }

            //资源池没用资源的时候 会移除资源池
            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime>=waitingTime)
            {
                BumLogic.poolManager.remove(keyUrl);
                dispose();
            }
            else
            {
                MyCallLater.Add(waitingTime-withoutUseTime,removeFromResourcePool);
            }
        }

        public override bool clear()
        {
            if (resource == null) return false;
            if (donotClear == true) return false;
            foreach (var item in targetDic.Values)
            {
                if (item !=null)
                {
                    GameObject.DestroyImmediate(item,true);
                }
            }
            targetDic.Clear();
            dispose();
            return true;
        }
    }

    public class BumPoolJsonData : BumPoolData
    {
        public string json;

        public BumPoolJsonData(string keyUrl,object resource,bool donotClear = false)
        {
            type = BumPoolDataType.eBumPoolDataType_json;
            this.keyUrl = keyUrl;
            this.resource = resource;
            this.donotClear = donotClear;
            json = resource.ToString();
        }

        protected override void onNewUse()
        {
            base.onNewUse();
            MyCallLater.Remove(removeFromResourcePool);
            MyCallLater.Add(waitingTime,removeFromResourcePool);
        }

        public void removeFromResourcePool()
        {
            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime > waitingTime)
            {
                dispose();
                BumLogic.poolManager.remove(keyUrl);
            }
            else
            {
                MyCallLater.Add(waitingTime - withoutUseTime, removeFromResourcePool);
            }
        }

        public override void dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class BumPoolTextureData : BumPoolData
    {
        public Texture texture;
        public List<int> targetList = new List<int>();

        public BumPoolTextureData(string keyUrl,object resource ,bool donotClear = false )
        {
            type = BumPoolDataType.eBumPoolDataType_texture2D;
            this.keyUrl = keyUrl;
            this.resource = resource;
            this.donotClear = donotClear;
            texture = resource as Texture;
        }

        public bool remove(int id)
        {
            if (targetList.IndexOf(id) !=-1)
            {
                newRemoveTime = Time.realtimeSinceStartup;
                targetList.Remove(id);
                if (targetList.Count ==0)
                {
                    MyCallLater.Add(waitingTime,removeFromResourcePool);
                }
            }
            return true;
        }

        public void removeFromResourcePool()
        {
            if (targetList.Count>0) return;

            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime>=waitingTime)
            {
                BumLogic.poolManager.remove(keyUrl);
                dispose();
            }
            else
            {
                MyCallLater.Add(waitingTime - withoutUseTime,removeFromResourcePool);
            }
        }

        public override void dispose()
        {
            GameObject.DestroyImmediate(texture,true);
            Resources.UnloadUnusedAssets();
        }
    }
}
