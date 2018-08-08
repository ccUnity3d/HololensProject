using UnityEngine;
using System.Collections;

public class GlobalConfig : MySingleton<GlobalConfig> {

    /// <summary>
    /// 模型根目录
    /// </summary>
    private Transform ModelParent;
    public Transform modelRoot
    {
        get
        {
            if (ModelParent == null)
            {
                ModelParent = GameObject.Find("SceneContent/ModelRoot").transform;
            }
            return ModelParent;
        }
    }
    /// <summary>
    /// UI 根目录
    /// </summary>
    private Transform UIRoot;
    public Transform uiRoot
    {
        get
        {
            if (UIRoot == null)
            {
                UIRoot = GameObject.Find("SceneContent/UIRoot").transform;
            }
            return UIRoot;
        }
    }
    /// <summary>
    /// 空间锚
    /// </summary>
    private Transform WorldAnchor;
    public Transform worldAnchor
    {
        get
        {
            if (WorldAnchor == null)
            {
                WorldAnchor = GameObject.Find("SceneContent/ModelRoot").transform; 
            }
            return WorldAnchor;
        }
    }

    private bool isLoadWorldAnchorStore;
    public bool IsLoadWorldAnchorStore
    {
        get
        {
            return isLoadWorldAnchorStore;
        }
        set
        {
            isLoadWorldAnchorStore = value;
        }
    }
    /// <summary>
    /// MainCamera
    /// </summary>
    public Camera cameraMain
    {
        get
        {
            return Camera.main;
        }
    }
    /// <summary>
    ///空间映射进度
    /// </summary>
    private Transform SpatialProcessing;
    public Transform spatialProcessing
    {
        get
        {
            if (SpatialProcessing == null)
            {
                SpatialProcessing = GameObject.Find("Hololens/SpatialProcessing").transform;
            }
            return SpatialProcessing;
        }
    }
    /// <summary>
    /// 空间映射管理
    /// </summary>
    private Transform SpatialMapping;
    public Transform spatialMapping
    {
        get
        {
            if (SpatialMapping == null)
            {
                SpatialMapping = GameObject.Find("Hololens/SpatialMapping").transform;
            }
            return SpatialMapping;
        }
    }
    /// <summary>
    ///分享管理
    /// </summary>
    private Transform ShareCompent;
    public Transform shareCompent
    {
        get
        {
            if (ShareCompent == null)
            {
                ShareCompent = GameObject.Find("Hololens/ShareCompent").transform;
            }
            return ShareCompent;
        }
    }
    /// <summary>
    /// 光标
    /// </summary>
    private Transform CursorTransform;
    public Transform cursorTransform
    {
        get
        {
            if (CursorTransform == null)
            {
                CursorTransform = GameObject.Find("Hololens/Cursor").transform;
            }
            return CursorTransform;
        }
    }
    private Transform ManagerTransform;
    public Transform managerTransform
    {
        get
        {
            if (ManagerTransform == null)
            {
                ManagerTransform = GameObject.Find("Hololens/Manager").transform;
            }
            return ManagerTransform;
        }
    }

    /// <summary>
    /// 是否可以输出
    /// </summary>
    public static bool isMyDebug = false;
    internal int placeCount =0;
}
