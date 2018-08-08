using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchToExplode : MonoBehaviour
{
    public bool isExplodeState;

    public Dictionary<string, MeshRenderer> showDetailDic = new Dictionary<string, MeshRenderer>();

    public Transform showDetail_jiaohuanji;
    public Transform showDetail_beng;
    public Transform showDetail_xuleng;


    public Camera mainCamera;
    // Use this for initialization
    void Start()
    {
        //if (!showDetailDic.ContainsKey("model_info_beng"))
        //{
        //    showDetailDic.Add("model_info_beng", showDetail_beng);
        //}
        //if (!showDetailDic.ContainsKey("model_info_jiaohuanji"))
        //{
        //    showDetailDic.Add("model_info_jiaohuanji", showDetail_jiaohuanji);
        //}
        //if (!showDetailDic.ContainsKey("model_info_xuleng"))
        //{
        //    showDetailDic.Add("model_info_xuleng", showDetail_xuleng);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (isExplodeState)
        {
            mUpdate();
            mPhoneUpdate();
        }
    }
    void mUpdate()
    {

        if (Input.GetMouseButton(0))
        {
 
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
 
                if (showDetailDic.ContainsKey(hitInfo.collider.gameObject.name))
                {
                
                    ShowPanel(hitInfo);

                }
                else
                {
            
                    showDetailDic.Add(hitInfo.collider.gameObject.name, hitInfo.collider.gameObject.GetComponentInChildren<MeshRenderer>());
                    ShowPanel(hitInfo);
                }

            }
        }

    }

    void mPhoneUpdate()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (showDetailDic.ContainsKey(hitInfo.collider.gameObject.name))
                    {
                        ShowPanel(hitInfo);
                    }
                    else
                    {
                        showDetailDic.Add(hitInfo.collider.gameObject.name, hitInfo.collider.gameObject.GetComponentInChildren<MeshRenderer>());
                        ShowPanel(hitInfo);
                    }

                }
            }
        }
    }

    void ShowPanel(RaycastHit hitInfo)
    {
        Debug.Log("5");

        foreach (string name in showDetailDic.Keys)
        {
            showDetailDic[name].enabled = false;
            if (hitInfo.collider.gameObject.name == name)
            {
                if (showDetailDic[name].enabled == true)
                {
                    showDetailDic[name].enabled = false;
                }
                else
                {
                    showDetailDic[name].enabled = true;
                }
            }
        }

    }

    public void OnExitExplode()
    {
        foreach (MeshRenderer item in showDetailDic.Values)
        {
            item.enabled = false;
        }
    }
}