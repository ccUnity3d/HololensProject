using UnityEngine;
using System.Collections;
public class BumRep
{
    public static BumCameraManager cameraManager;

    private static Transform ModelParent;
    public static Transform modelRoot
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

    private static Transform UIRoot;
    public static Transform uiRoot
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

   static Camera uiCamera = null;
    public static Camera getUICamera()
    {
        if (uiCamera == null)
            uiCamera = uiRoot.GetChild(1).gameObject.GetComponent<Camera>();

        return uiCamera;
    }

    public void init()
    {
        //uiManager = GameObject.Find("UIRoot").GetComponent<BumUIManager>();
        //uiManager.init();

        //cameraManager = new BumCameraManager();
        //cameraManager.init();
    }

    public GameObject getNewInstanceGoods3DSkin()
    {
        GameObject go = new GameObject();
        go.transform.parent = BumRep.modelRoot;
        go.transform.localScale = Vector3.one;
        go.layer = LayerMask.NameToLayer("Default");

        GameObject emtyMode = GameObject.CreatePrimitive(PrimitiveType.Cube);
        emtyMode.name = "emtyMode";
        emtyMode.transform.parent = go.transform;
        emtyMode.transform.localPosition = Vector2.zero;
        emtyMode.transform.localScale = Vector3.one;
        emtyMode.transform.rotation = Quaternion.Euler(Vector3.zero);
        emtyMode.SetActive(false);
        return go;
    }

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
}
