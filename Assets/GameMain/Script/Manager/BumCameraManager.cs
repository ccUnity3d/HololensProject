using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BumCameraType
{
    eBumARType_Free,
    eBumARType_Tango,
    eBumARType_Void,
    eBumARType_VoidSlam,
    eBumARType_VoidCloud,
    eBumARType_ARKit,
    eBumARType_ARCore
}

public class BumCameraManager
{
    public Dictionary<BumCameraType, Transform> arCamerasDic = new Dictionary<BumCameraType, Transform>();

    WebCamTexture webcamT;
    public Transform FreePatternCamera
    {
        get
        {
            return BumUITool.GetUIComponent<Transform>(BumRep.modelRoot, "FreePatternCamera");
        }
    }
    public Transform VoidSlamAR
    {
        get
        {
            return BumUITool.GetUIComponent<Transform>(BumRep.modelRoot, "VoidSlamAR");
        }
    }
    public Transform VoidScanAR
    {
        get
        {
            return BumUITool.GetUIComponent<Transform>(BumRep.modelRoot, "VoidScanAR");
        }
    }
    public Transform TangoCamera
    {
        get
        {
            return BumUITool.GetUIComponent<Transform>(BumRep.modelRoot, "TangoCamera");
        }
    }

    public BumCameraManager()
    {

    }

    public void init()
    {
        initVoidAR();
        arCamerasDic.Clear();
        arCamerasDic.Add(BumCameraType.eBumARType_ARCore, null);
        arCamerasDic.Add(BumCameraType.eBumARType_ARKit, null);
        arCamerasDic.Add(BumCameraType.eBumARType_Tango, TangoCamera);
        arCamerasDic.Add(BumCameraType.eBumARType_Void, VoidScanAR);
        arCamerasDic.Add(BumCameraType.eBumARType_VoidCloud, VoidScanAR);
        arCamerasDic.Add(BumCameraType.eBumARType_VoidSlam, VoidSlamAR);
        arCamerasDic.Add(BumCameraType.eBumARType_Free, FreePatternCamera);

        arCamerasDic.Values.ToList().ForEach((t) => { if(t!=null)t.gameObject.SetActive(false); });
    }

    public Transform getCamera(BumCameraType type)
    {
        Transform root = null;
        arCamerasDic.TryGetValue(type, out root);

        return root;
    }

    public void start(BumCameraType type)
    {
        //webcamT = new WebCamTexture();
        //for (int i = 0; i < WebCamTexture.devices.Length; i++)
        //{
        //    Debug.Log(WebCamTexture.devices.Length);
        //    //如果是前置摄像机
        //    if (WebCamTexture.devices[i].isFrontFacing )
        //    {
        //        webcamT.deviceName = WebCamTexture.devices[i].name;
        //        break;
        //    }
        //    //如果是后置摄像机
        //    if (!WebCamTexture.devices[i].isFrontFacing)
        //    {
        //        webcamT.deviceName = WebCamTexture.devices[i].name;
        //        break;
        //    }
        //}
        //if (webcamT.isPlaying) webcamT.Stop();
        Transform transform;
        if (arCamerasDic.TryGetValue(type, out transform)) transform.gameObject.SetActive(true);
    }

    public void stop(BumCameraType type)
    {
        Transform transform;
        if (arCamerasDic.TryGetValue(type, out transform)) transform.gameObject.SetActive(false);
    }

    public void pause(BumCameraType type)
    {
        //switch (type)
        //{

        //    default: break;
        //    case BumARType.eBumARType_Tango: break;
        //    case BumARType.eBumARType_Void: break;
        //    case BumARType.eBumARType_VoidCloud: break;
        //    case BumARType.eBumARType_VoidSlam: break;
        //    case BumARType.eBumARType_ARKit: break;
        //    case BumARType.eBumARType_ARCore: break;
        //}

    }

    public void initVoidAR()
    {

    }

    Camera mainCamera;

    public Camera getMainCamera()
    {
        if (mainCamera == null)
            mainCamera = FreePatternCamera.GetChild(0).gameObject.GetComponent<Camera>();

        return mainCamera;
    }
}
