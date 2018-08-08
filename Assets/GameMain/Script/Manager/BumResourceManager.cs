using UnityEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;

public enum BumResourceType
{
    eBumResourceType_json,
    eBumResourceType_texture2D,
    eBumResourceType_assetBundle,
    eBumResourceType_userInfo
}

public enum BumResourcePoolType
{
    FactoryObject,
    ZoneObject,
    ProductObject,
    None
}

public enum BumLoadingType
{
    eBumLoadingType_auto,
    eBumLoadingType_local,
    eBumLoadingType_www,
    eBumLoadingType_native
}

public class BumResourceManager
{
    static BumResourceLoader loader = new BumResourceLoader();

    public static void loadResource(string url, Action<object> onloaded, Action<object> progressEvent = null, BumResourceType resType = BumResourceType.eBumResourceType_assetBundle,
        BumLoadingType loadingType = BumLoadingType.eBumLoadingType_www, BumResourcePoolType resourceType = BumResourcePoolType.ProductObject, object param = null, Action<GameObject, object> beforeClone = null)
    {
        switch (loadingType)
        {
            case BumLoadingType.eBumLoadingType_auto: loader.auto(url, onloaded, progressEvent, resType, resourceType, param, beforeClone); break;
            case BumLoadingType.eBumLoadingType_local: loader.loadFromLocal(url, onloaded); break;
            case BumLoadingType.eBumLoadingType_www: loader.loadFromWWW(url, onloaded, progressEvent, resType, resourceType,param, beforeClone); break;
            default:break;
        }
    }

    //public static void loadAsyncHttpResource(string url, DownloadProgressChangedEventHandler progressEvent, AsyncCompletedEventHandler completeEvent, 
    //     BumResourceType resType = BumResourceType.eBumResourceType_assetBundle, object data = null, Action<object> stopEvent = null)
    //{
    //    AcyncItem item = new AcyncItem();
    //    item.init( url, progressEvent
    //    //(object sender, DownloadProgressChangedEventArgs e)=> 
    //    //{
    //    //    progressEvent(sender,e);
    //    //    // 处理进度。。。
    //    //    item.progress = (float)e.BytesReceived / e.TotalBytesToReceive;
    //    //}
    //    ,
    //   (object sender, AsyncCompletedEventArgs e) =>
    //    {
    //        completeEvent(sender, e);
    //        // 处理加载完成。。。
    //        item.isComplete = true;
    //    },
    //   //completeEvent,
    //    resType,  data,  stopEvent);
    //    BumLogic.acyncManager.addAcyncLoad(item);
    //}

    //#region acync
    //public static void loadAsyncHttpResource(List<AcyncItem> listsItem)
    //{
    //    BumLogic.acyncManager.addAcyncLoad(listsItem);
    //}

    //public static void loadAsyncHttpResource(AcyncItem listsItem)
    //{
    //    BumLogic.acyncManager.addAcyncLoad(listsItem);
    //}

    //public static bool stopAllAsyncHttpResource()
    //{
    //    return BumLogic.acyncManager.stopAllAsync();
    //}

    //public static bool stopAsyncHttpResource(AcyncItem item)
    //{
    //    return BumLogic.acyncManager.reamove(item);
    //}
    //#endregion

    //C#原生API异步加载resources
    public void loadAsyncFromNative()
    {

    }
    /*
      BumResourceManager.loadAsyncHttpResource("http://appimg.bumie.org/Windows/3e7243ed-7ec1-4095-bde5-75c343c72cfe/product",
      (object sender, DownloadProgressChangedEventArgs e) =>
      {
          Debug.Log((float)e.BytesReceived / e.TotalBytesToReceive);
      },
      (object sender, AsyncCompletedEventArgs e) =>
      {
          Debug.Log("Complete1");
      });
      */
}
