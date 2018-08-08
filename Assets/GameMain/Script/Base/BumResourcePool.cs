using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BumObjType
{
    eBumObjType_Product,
    eBumObjType_Factory,
    eBumObjType_Zone
}
public class BumResourcePool
{

    private Dictionary<string, LoadPoolData> poolDic = new Dictionary<string, LoadPoolData>();

    private int maxLimit;



    private Transform factoryObject
    {
        get
        {
            return BumDefine.Instance.factoryObject;
        }
    }

    private Transform zoneObject
    {
        get
        {
            return BumDefine.Instance.zoneObject;
        }
    }

    private Transform productObject
    {
        get
        {
            return BumDefine.Instance.productObject;
        }
    }

    public LoadPoolData addData(string url, object resource, BumResourceType type)
    {
        switch (type)
        {
            case BumResourceType.eBumResourceType_json: addJson(url, resource); break;
            case BumResourceType.eBumResourceType_texture2D: addTexture(url, resource); break;
            case BumResourceType.eBumResourceType_assetBundle: addPrefab(url, resource); break;
            default: addError(url); break;
        }
        return null;
    }

    //public void recyclePoolData(string url,GameObject gameobject, BumResourcePoolType resourcePoolType)
    //{
    //    Dictionary<string, List<GameObject>> tempDci;
    //    List<GameObject> tempList;
    //    bool checkDic = resourcePoolDic.TryGetValue(resourcePoolType,out tempDci);
    //    if (checkDic == false) return;
    //    bool checkKey = tempDci.TryGetValue(url, out tempList);
    //    resourcePoolDic[resourcePoolType][url].Add(Rest(gameobject, resourcePoolType));
    //}

    //private GameObject getPoolObject(string url, BumResourcePoolType resourcePoolType)
    //{
    //    Dictionary<string, List<GameObject>> tempDci;
    //    List<GameObject> tempList;
    //    GameObject GO  = null;
    //    bool checkDic = resourcePoolDic.TryGetValue(resourcePoolType, out tempDci);
    //    if (checkDic == false) return null;
    //    bool checkKey = tempDci.TryGetValue(url, out tempList);
    //    if (tempList.Count>0)
    //    {
    //        GO = tempList[0];
    //        tempList.Remove(GO);
    //    }
    //    return GO;
    //}

    private GameObject Rest(GameObject go, BumResourcePoolType resourcePoolType)
    {
        switch (resourcePoolType)
        {
            case BumResourcePoolType.FactoryObject:
                go.transform.SetParent(factoryObject);
                break;
            case BumResourcePoolType.ZoneObject:
                go.transform.SetParent(zoneObject);
                break;
            case BumResourcePoolType.ProductObject:
                go.transform.SetParent(productObject);
                break;
            default:
                break;
        }
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.SetActive(false);
        return go;
    }

    public bool TryGet(string url, Action<object> onloaded, Action<object> progressEvent = null,
        BumResourceType resType = BumResourceType.eBumResourceType_assetBundle, BumResourcePoolType resourcePoolType = BumResourcePoolType.ProductObject, object param = null, Action<GameObject, object> beforeClone = null)
    {
        LoadPoolData loopdata = null;
        bool hasPool = poolDic.TryGetValue(url, out loopdata);
        if (hasPool)
        {

            if (onloaded != null) onloaded(loopdata.resouce);
            if (progressEvent != null) progressEvent(1.0f);
            if (beforeClone != null) beforeClone(loopdata.resouce as GameObject, param);
        }
        return hasPool;
        //GameObject resource = getPoolObject(url, resourcePoolType);
        //if (resource == null)
        //{
        //    resource = (GameObject)GameObject.Instantiate<GameObject>(loopdata.resouce as GameObject);
        //    Rest(resource, resourcePoolType);
        //}
    }

    public void disPos(object target, BumResourceType resType)
    {
        switch (resType)
        {
            case BumResourceType.eBumResourceType_texture2D:
                disTexturePos(target as Texture); break;
            case BumResourceType.eBumResourceType_assetBundle:
                disPrefabDis(target as GameObject);
                break;
            default: Debug.LogWarningFormat("不存在{0}类型", resType); break;
        }

    }

    #region AddData
    public PrefabData addPrefab(string url, object resouce)
    {
        if (poolDic.ContainsKey(url))
        {
            if (poolDic[url] is ErrorData) poolDic.Remove(url);
            else
            {
                Debug.LogWarningFormat("重复添加资源到对象池{0}", url);
                return poolDic[url] as PrefabData;
            }
        }
        PrefabData loadPoolData = new PrefabData(url, resouce);
        poolDic.Add(url, loadPoolData);
        return loadPoolData;
    }

    public void addTexture(string url, object resouce)
    {
        if (poolDic.ContainsKey(url))
        {
            if (poolDic[url] is ErrorData) poolDic.Remove(url);
            else
            {
                poolDic.Remove(url);
                //Debug.LogWarningFormat("重复添加资源到对象池{0}", url);
            }
        }
        TextureData loadPoolData = new TextureData(url, resouce);
        poolDic.Add(url, loadPoolData);
    }

    public JsonData addJson(string url, object resouce)
    {
        if (poolDic.ContainsKey(url))
        {
            if (poolDic[url] is ErrorData) poolDic.Remove(url);
            else
            {
                Debug.LogWarningFormat("重复添加资源到对象池{0}", url);
                return poolDic[url] as JsonData;
            }
        }
        JsonData loadPoolData = new JsonData(url, resouce);
        poolDic.Add(url, loadPoolData);
        return loadPoolData;
    }

    public ErrorData addError(string url)
    {
        ErrorData data;
        if (poolDic.ContainsKey(url))
        {
            //Debug.Log("重复添加：" + keyUrl);
            data = poolDic[url] as ErrorData;
            data.errorCount++;
        }
        else
        {
            data = new ErrorData(url);
            data.errorCount = 1;
            poolDic.Add(url, data);
        }
        return data;
    }
    #endregion

    #region RemoveData

    public void removeError(object data)
    {
        string keyUrl = data.ToString();
        ErrorData errorData;
        if (poolDic.ContainsKey(keyUrl))
        {
            errorData = poolDic[keyUrl] as ErrorData;
            if (errorData != null) poolDic.Remove(keyUrl);
        }
    }
    #endregion

    #region CleraPool

    public void clearPool()
    {
        List<string> clear = new List<string>();
        foreach (string item in poolDic.Keys)
        {
            bool cleared = poolDic[item].Clear();
            if (cleared == true)
            {
                clear.Add(item);
            }
        }
        for (int i = 0; i < clear.Count; i++)
        {
            poolDic.Remove(clear[i]);
        }
    }

    #endregion

    #region DisPos
    public void disPrefabDis(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Dispos target == null");
            return;
        }
        int count = 0;
        int id = target.GetInstanceID();
        foreach (LoadPoolData item in poolDic.Values)
        {
            if (item.poolType == BumResourceType.eBumResourceType_assetBundle)
            {
                PrefabData prefabData = item as PrefabData;
                if (prefabData.remove(id))
                {
                    count++;
                    //只有prefab实例会销毁  
                    if (target != null) GameObject.Destroy(target);
                }
            }
        }
    }

    public void disTexturePos(Texture target)
    {
        if (target == null)
        {
            Debug.LogWarning("Dispos target == null");
            return;
        }
        int count = 0;
        int id = target.GetInstanceID();
        foreach (LoadPoolData item in poolDic.Values)
        {
            if (item.poolType == BumResourceType.eBumResourceType_texture2D)
            {
                TextureData textureData = item as TextureData;
                if (textureData.Remove(id))
                {
                    count++;
                    //texture是引用不能销毁物体  如需销毁在外部自己做此处不应提供if (target != null) GameObject.Destroy(target);
                }
            }
        }
    }
    #endregion

    #region LoadPoolData
    public abstract class LoadPoolData
    {
        public const float waitingTime = 30f;
        public string keyUrl;
        public object resouce;
        /// <summary>
        /// 加载的时间
        /// </summary>
        public float loadedTime;
        /// <summary>
        /// 最新使用时间
        /// </summary>
        private float _newUseTime;
        public float newUseTime
        {
            get { return _newUseTime; }
            set
            {
                _newUseTime = value;
                OnNewUse();
            }
        }

        public float newRemoveTime;

        public BumResourceType poolType;

        public bool DonnotClear;

        //protected BumResourcePool resourcesPool
        //{
        //    get { return BumResourcePool.Instance; }
        //}

        protected virtual void OnNewUse()
        {

        }

        public abstract void disPos();

        public virtual bool Clear()
        {
            if (resouce == null) return false;
            if (DonnotClear == true) return false;
            disPos();
            return true;
        }
    }
    #endregion

    #region PrefabData
    public class PrefabData : LoadPoolData
    {
        public UnityEngine.Object prefab;

        private Dictionary<int, GameObject> prefabDic = new Dictionary<int, GameObject>();

        public PrefabData(string url, object source)
        {
            this.poolType = BumResourceType.eBumResourceType_assetBundle;
            this.loadedTime = Time.realtimeSinceStartup;
            this.newUseTime = this.loadedTime;
            this.keyUrl = url;
            this.resouce = source;
            prefab = source as UnityEngine.Object;
        }

        public object getNewPrefab()
        {
            GameObject newPrefab = GameObject.Instantiate(prefab) as GameObject;
            prefabDic.Add(newPrefab.GetInstanceID(), newPrefab);
            return newPrefab;
        }

        public bool remove(int id)
        {
            if (prefabDic.ContainsKey(id))
            {
                newRemoveTime = Time.realtimeSinceStartup;
                if (prefabDic[id] != null)
                {
                    GameObject.DestroyImmediate(prefabDic[id], true);
                }
                prefabDic.Remove(id);
                if (prefabDic.Count == 0)
                {
                    MyCallLater.Add(waitingTime, RemoveFromResoucePool);
                }
                return true;
            }
            return false;
        }

        private void RemoveFromResoucePool()
        {
            if (prefabDic.Count > 0) return;
            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime >= waitingTime)
            {
                //resourcesPool.Remove(keyUrl);
                disPos();
            }
            else
            {
                MyCallLater.Add(waitingTime - withoutUseTime, RemoveFromResoucePool);
            }
        }

        public override bool Clear()
        {
            if (resouce == null) return false;
            //if (DonnotClear == true) return false;
            foreach (GameObject item in prefabDic.Values)
            {
                if (item != null)
                {
                    GameObject.DestroyImmediate(item, true);
                }
            }
            prefabDic.Clear();
            disPos();
            return true;
        }

        public override void disPos()
        {
            GameObject.DestroyImmediate(prefab, true);
            Resources.UnloadUnusedAssets();
        }
    }
    #endregion

    #region TextureData
    public class TextureData : LoadPoolData
    {
        public Texture texture;

        public List<int> poolList = new List<int>();

        public TextureData(string url, object source)
        {
            this.poolType = BumResourceType.eBumResourceType_texture2D;
            this.keyUrl = url;
            this.resouce = source;
            //this.DonnotClear = DonnotClear;
            texture = resouce as Texture;
        }

        public bool Remove(int id)
        {
            if (poolList.IndexOf(id) != -1)
            {
                newRemoveTime = Time.realtimeSinceStartup;
                poolList.Remove(id);
                if (poolList.Count == 0)
                {
                    MyCallLater.Add(waitingTime, RemoveFromResoucePool);
                }
                return true;
            }
            return false;
        }

        private void RemoveFromResoucePool()
        {
            if (poolList.Count > 0) return;
            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime >= waitingTime)
            {
                //resourcesPool.Remove(keyUrl);
                disPos();
            }
            else
            {
                MyCallLater.Add(waitingTime - withoutUseTime, RemoveFromResoucePool);
            }
        }

        public override void disPos()
        {
            Debug.Log("释放图片:" + texture);
            GameObject.DestroyImmediate(texture, true);
            Resources.UnloadUnusedAssets();
        }
    }
    #endregion

    #region JsonData
    public class JsonData : LoadPoolData
    {
        public string json;

        public JsonData(string url, object resouce)
        {
            this.poolType = BumResourceType.eBumResourceType_json;
            this.keyUrl = url;
            this.resouce = resouce;
            json = resouce.ToString();
        }

        protected override void OnNewUse()
        {
            base.OnNewUse();
            MyCallLater.Remove(RemoveFromResoucePool);
            MyCallLater.Add(waitingTime, RemoveFromResoucePool);
        }

        private void RemoveFromResoucePool()
        {
            float withoutUseTime = Time.realtimeSinceStartup - newRemoveTime;
            if (withoutUseTime > waitingTime)
            {
                disPos();
                //resourcesPool.Remove(keyUrl);
            }
            else
            {
                MyCallLater.Add(waitingTime - withoutUseTime, RemoveFromResoucePool);
            }
        }

        public override void disPos()
        {

        }
    }
    #endregion

    #region ErrorData
    public class ErrorData : LoadPoolData
    {
        public readonly int MaxErrorCount = 3;
        public int errorCount = 0;

        public ErrorData(string keyUrl)
        {
            this.keyUrl = keyUrl;
        }

        public override void disPos()
        {

        }
    }
    #endregion


    #region tempPool

    Dictionary<BumObjType, Dictionary<string, Queue<GameObject>>> tempPoolDic = new Dictionary<BumObjType, Dictionary<string, Queue<GameObject>>>();

    public GameObject getPoolData(string url, BumObjType objType)
    {
        Dictionary<string, Queue<GameObject>> tempDic;
        Queue<GameObject> queue;
        if (tempPoolDic.TryGetValue(objType, out tempDic))
            if (tempDic.TryGetValue(url, out queue))
                return queue.Dequeue();
        return null;
    }
    public void recyclePoolData(string url, GameObject go, BumObjType objtype)
    {
        Dictionary<string, Queue<GameObject>> tempDic;
        Queue<GameObject> queue;
        if (tempPoolDic.TryGetValue(objtype, out tempDic))
        {
            if (tempDic.TryGetValue(url, out queue))
            {
                queue.Enqueue(go);
            }
            else
            {
                queue = new Queue<GameObject>();
                queue.Enqueue(go);
                tempDic.Add(url, queue);
            }
        }
        else
        {
            queue = new Queue<GameObject>();
            queue.Enqueue(go);
            tempDic.Add(url, queue);
            tempPoolDic.Add(objtype,tempDic);
        }
    }

    #endregion


}
