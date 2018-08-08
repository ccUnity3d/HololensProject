using UnityEngine;

using System.Collections;

using System.Collections.Generic;

public class TouchToOperationGuide : MonoBehaviour

{
    [HideInInspector]
    public Camera operationCamera;
    [HideInInspector]
    public bool isOperationEnter;
    [HideInInspector]
    public int index;
    [HideInInspector]
    public Animation myAnimation;
    [HideInInspector]
    public AnimationClip[] animationClip;

    // Update is called once per frame

    void Update()
    {
        if (isOperationEnter) {
           // Mupdate();
            MPhoneUpdate();
        }
      
    }
    void Mupdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = operationCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider == null)
                {
                    return;
                }
                if (hitInfo.collider.gameObject.tag == "GuideHand")
                {
                    // AndroidHelper.ShowAndroidToastMessage("进行下一步动画");
                   // hitInfo.collider.gameObject.SetActive(false);

                    OnPlay();
                }
            }
            else
            {
                Debug.Log("没有点击到");
            }
        }
  
    }
    public void MPhoneUpdate()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    AndroidHelper.ShowAndroidToastMessage(touch.tapCount.ToString());

        //    if (touch.phase == TouchPhase.Began)
        //    {

        //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //        RaycastHit hitInfo;
        //        if (Physics.Raycast(ray, out hitInfo))
        //        {
        //            AndroidHelper.ShowAndroidToastMessage("发射射线");

        //            if (hitInfo.collider.gameObject.tag == "GuideHand")
        //            {
        //                GameObject.Destroy(hitInfo.collider.gameObject);
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("没有点击到");
        //        }
        //    }
        //}
        //else
        //{
        //    //AndroidHelper.ShowAndroidToastMessage("mPhoneUpdate");
        //}
        if (isOperationEnter)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                //AndroidHelper.ShowAndroidToastMessage(touch.tapCount.ToString() + operationCamera.name);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = operationCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider == null)
                        {                          
                            return;
                        }
                        if (hitInfo.collider.gameObject.tag == "GuideHand")
                        {
                            // AndroidHelper.ShowAndroidToastMessage("进行下一步动画");
                            hitInfo.collider.gameObject.SetActive(false);
                            OnPlay();
                        }
                    }
                    else
                    {
                        Debug.Log("没有点击到");
                    }
                }
            } 
        }
    }

    public void OnPlay()
    {
        if (index == animationClip.Length )
        {
            index = 0;
        }
        myAnimation.Play("queue_" + index);
        index++;
    }


}