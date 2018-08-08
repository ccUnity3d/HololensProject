using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BumInputController
{
    private float timeInAll;//触摸累积时间
    private float distInAll;//触摸累积距离
    private bool zoomFlag = false;
    private bool moveFlag = false;
    private bool longPressFlag = false;

    public Vector3 getWorldPosition(Vector3 pos)
    {
        Vector3 mousePositionOnScreen = pos;
        mousePositionOnScreen.z = -1;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        return mousePositionInWorld;
    }


    public bool inputValid()
    {
         return true;
    }

    public void init()
    {

    }

    void routineControl()
    {

#if (UNITY_ANDROID || UNITY_IPHONE)

        if (Input.touchCount > 0)
        { 
            Touch touchInfo = Input.GetTouch(0);

            //防止缩放后的立即建造
            if (Input.touchCount > 1)
            {
                zoomFlag = true;
            }else {
                zoomFlag = false;
            }



            if (Input.touchCount == 1 && inputValid() && !zoomFlag)
            {//一个手指触摸屏幕且没点到UI

                for (int i = 0; i < Input.touchCount; ++i)
                {
                    timeInAll += touchInfo.deltaTime;
                }

                if (touchInfo.phase == TouchPhase.Moved)
                {
                    moveFlag = true;
                }

                if (!longPressFlag && !moveFlag && touchInfo.phase == TouchPhase.Stationary && timeInAll >= 0.3f)
                {
                    longPressFlag = true;
                }

                if (!moveFlag && !longPressFlag && touchInfo.phase == TouchPhase.Ended)
                {
                    //VGame.Scene.BuildSceneItemFromInput(GetWorldPosition(touchInfo.position));
                }

                if (longPressFlag && touchInfo.phase == TouchPhase.Ended)
                {
                    longPressFlag = false;
                }

                if (moveFlag && touchInfo.phase == TouchPhase.Ended)
                {
                    moveFlag = false;
                }

                if (touchInfo.phase == TouchPhase.Ended)
                    timeInAll = 0;
            }

        }

        
#elif (UNITY_EDITOR || UNITY_STANDALONE_WIN)

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {

            }
        }

#endif

    }

    private UGUIHitUI uguiHitUI
    {
        get
        {
            return UGUIHitUI.Instance;
        }
    }

    public void tick()
    {
        if (uguiHitUI != null) uguiHitUI.RunIfUIIsHited();

    }
}
