using System;
using UnityEngine;
using System.Collections;
public enum SimpleLoadTypeEnum
{

    /// <summary>
    /// 基本不用
    /// </summary>
    Byte,
    /// <summary>
    /// 外部加载Json使用的类型 传输配置保存和读取类型
    /// </summary>
    Json,
    /// <summary>
    /// 方案-Cache加载Json使用的类型
    /// </summary>
    JsonScheme,
    /// <summary>
    /// 报价-Cache加载Json使用的类型
    /// </summary>
    JsonOffer,
    /// <summary>
    /// 远程图片
    /// </summary>
    texture2D,
    /// <summary>
    /// 打包的UGUIPanel和远程模型
    /// </summary>
    prefabAssetBundle,

    /// <summary>
    /// 暂时没有
    /// </summary>
    Prefab,
    /// <summary>
    /// 暂时没有
    /// </summary>
    texture2DAssetBundle,


}
public class MyLoader  {

    public MyLoader()
    {

    }
    private string loadPath;
    public object bringData;
    public Action<UnityEngine.Object, object> onLoaded;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type">加载类型 0本地Resouce 1本地StreamingAsset 2远程</param>
    /// <param name="Loaded"></param>
    /// <param name="data"></param>
    public void LoadPrefab(string path ,int type,Action<UnityEngine.Object,object> Loaded,object data = null)
    {
        onLoaded = Loaded;
        bringData = data;
        if (type == 0)
        {
            loadPath = path.Split('.')[0];
            string resourceTag = "Prefab/UI/";
            UnityEngine.Object obj = UnityEngine.GameObject.Instantiate(Resources.Load(resourceTag+loadPath));
            if (onLoaded != null) onLoaded(obj,bringData);
        }
        else if (type ==1)
        {
            loadPath = "UI/panel/" + path;
            //LoaderPool.InnerLoad(loadPath,SimpleLoadTypeEnum.prefabAssetBundle, OnLoaded, null);
        }
        else
        {
            Debug.Log("目前只有本地");
        }
    }   
    private void OnLoaded(object obj)
    {
        SimpleLoader loader = obj as SimpleLoader;
        if (loader.state == SimpleLoadState.Failed)
        {
            Debug.LogWarning("Load Fail:"+loader.uri);
            return;
        }
        if (onLoaded == null) return;
        UnityEngine.Object ob = loader.loadedData as UnityEngine.Object;
        onLoaded(ob,bringData);
    }
}
