using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : UIPage<LoginPage> {

    public Transform LoginPlane;
    public Button enterMainPageButton;
    public Button setButton;
    public Transform SetPlane;
    public ToggleButton toggleButton;
    public Button backLoginPageButton;
    public override void OnInstance()
    {
        base.OnInstance();
        getPrefabPath = "EnterMainPage";
    }

    protected override void Ready(Object arg1)
    {
        base.Ready(arg1);
        RectTransform skin = (arg1 as GameObject).transform as RectTransform;
        skin.anchoredPosition3D = new Vector3(0, 0, 2);
        skin.localScale = new Vector3(0.001196975F, 0.001196975F, 0.001196975F);
        LoginPlane = BumUITool.GetUIComponent<Transform>(skin,"EnterBackGround");
        enterMainPageButton = BumUITool.GetUIComponent<Button>(LoginPlane, "EnterButton");
        setButton = BumUITool.GetUIComponent<Button>(LoginPlane, "SetButton");
        SetPlane = BumUITool.GetUIComponent<Transform>(skin, "SetBackGround");
        toggleButton = BumUITool.AddUIComponent<ToggleButton>(SetPlane, "GroupToggle/item/toggleButton");
        backLoginPageButton = BumUITool.GetUIComponent<Button>(SetPlane,"BackButton");
        BumUITool.SetActionFalse(SetPlane.gameObject);
    }
}
