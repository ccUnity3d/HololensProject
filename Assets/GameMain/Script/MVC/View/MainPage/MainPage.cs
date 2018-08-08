using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

//UIPage<MainPage>
public class MainPage : UIPage<MainPage> {

    public Transform DownPlane;
    public Transform AdjustPlane;
    public Transform adjustConfirm;
    public Button downButton;
    public Button cancelButton;
    public Transform ProductPlane;
    public Button adjustButton;
    public Button hideButton;
    public Button extendButton;
    public ProductScrollView producScroll;
    public override void OnInstance()
    {
        base.OnInstance();
        getPrefabPath = "ProductListPage";
    }

    protected override void Ready(UnityEngine.Object arg1)
    {
        base.Ready(arg1);
        RectTransform skin = (arg1 as GameObject).transform as RectTransform;
      
        skin.anchoredPosition3D = new Vector3(0f, 0f, 2);
        skin.localScale = new Vector3(0.00120576F, 0.00120576F, 0.00120576F);
        ProductPlane = BumUITool.GetUIComponent<Transform>(skin,"Plane");
        //DownPlane = UITool.GetUIComponent<Transform>(ProductPlane, "DownBackGround");
        //downRemoveButton = UITool.GetUIComponent<Button>(DownPlane,"Remove");
        AdjustPlane = BumUITool.GetUIComponent<Transform>(ProductPlane, "AdjustBackGround");
        BumUITool.AddUIComponent<ProductPageDragView>(AdjustPlane.gameObject);
        adjustButton = BumUITool.GetUIComponent<Button>(AdjustPlane,"Adjust");
        hideButton = BumUITool.GetUIComponent<Button>(AdjustPlane,"Hide");
        extendButton = BumUITool.GetUIComponent<Button>(skin,"Extend");
        producScroll = BumUITool.AddUIComponent<ProductScrollView>(ProductPlane, "ProducctPlane/ProductScroll");
        adjustConfirm = BumUITool.GetUIComponent<Transform>(ProductPlane, "AdjustConfirm");
        downButton = BumUITool.GetUIComponent<Button>(adjustConfirm,"Down");
        cancelButton = BumUITool.GetUIComponent<Button>(adjustConfirm,"Cancel");
        BumUITool.SetActionFalse(extendButton.gameObject);
        BumUITool.SetActionFalse(downButton.gameObject);
        BumUITool.SetActionFalse(cancelButton.gameObject);
        BumUITool.SetActionFalse(extendButton.gameObject);
    }


}
