using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/// <summary>
/// UGUI 判断是否点到UI 在 Up的时候 有很大的坑  明明点到却返回没点到//所以做UGUIHitUI 自己判断是否点到
/// </summary>
public class UGUIHitUI : MySingleton<UGUIHitUI> {

    private bool hitResult;
    public bool uiHited
    {
        get {
            return hitResult;
        }
    }

    public void RunIfUIIsHited()
    {
        if (GlobalConfig.isMyDebug == true)
        {
#if !UNITY_ANDROID && !UNITY_IPHONE  //安卓  
            hitResult = EventSystem.current.IsPointerOverGameObject();
            return;
#else
            if (Input.touchCount == 0)
            {
                hitResult = false;
                return;
            }

            PointerEventData pointdata;
            if (MyStandaloneInputModule.current.tryGetMyMousePointerEventData(out pointdata) == false)
            {
                hitResult = false;
                return;
            }
            List<RaycastResult> list = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointdata, list);
            if (list.Count == 0)
            {
                hitResult = false;
                return;
            }
            hitResult = true;

            return;
#endif
        }
        else {
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            {
                hitResult = EventSystem.current.IsPointerOverGameObject();
                return;
            }

            if (Input.touchCount == 0)
            {
                hitResult = false;
                return;
            }

            PointerEventData pointdata;
            if (MyStandaloneInputModule.current.tryGetMyMousePointerEventData(out pointdata) == false)
            {
                hitResult = false;
                return;
            }
            List<RaycastResult> list = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointdata, list);
            if (list.Count == 0)
            {
                hitResult = false;
                return;
            }

            hitResult = true;
            return;
        }
    }

}
