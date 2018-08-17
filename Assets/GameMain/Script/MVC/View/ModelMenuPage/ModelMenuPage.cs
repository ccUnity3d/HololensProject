using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMenuPage :UIPage<ModelMenuPage> {

    public Transform OneLevelPlane;
    public Transform Editor_Button;
    public Transform Function_Button;
    public Transform TwoLevelPlane;
    public Transform EditorPlane;
    public Transform Editor_Destory_Button;
    public Transform Editor_Place_Button;
    public Transform Editor_Adjust_Button;
    public Transform Editor_Home_Button;
    public Transform Info_Button;
    public Transform FunctionPlane;
    public Transform Function_Home_Button;
    public Transform Material_Button;
    public Transform Measure_Button;
    public Transform Sort_Button;

    public override void OnInstance()
    {
        base.OnInstance();
        getPrefabPath = "3DGOODMenu";


    }
    
    protected override void Ready(Object arg1)
    {
        base.Ready(arg1);

        OneLevelPlane = BumUITool.GetUIComponent<Transform>(skin.transform, "OneLevel");
        Editor_Button = BumUITool.GetUIComponent<Transform>(OneLevelPlane, "Editor_Button");
        Function_Button = BumUITool.GetUIComponent<Transform>(OneLevelPlane, "Function_Button");
        TwoLevelPlane = BumUITool.GetUIComponent<Transform>(skin.transform, "TwoLevel");
        EditorPlane = BumUITool.GetUIComponent<Transform>(TwoLevelPlane, "EditorPlane");
        Editor_Destory_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Destory_Button");
        Editor_Place_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Place_Button");
        Editor_Adjust_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Adjust_Button");
        Editor_Home_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Home_Button");
        FunctionPlane = BumUITool.GetUIComponent<Transform>(TwoLevelPlane, "FunctionPlane");
        Function_Home_Button = BumUITool.GetUIComponent<Transform>(FunctionPlane, "Function_Home_Button");
        Material_Button = BumUITool.GetUIComponent<Transform>(FunctionPlane, "Material_Button");
        Sort_Button = BumUITool.GetUIComponent<Transform>(FunctionPlane, "Sort_Button");
        Measure_Button = BumUITool.GetUIComponent<Transform>(FunctionPlane, "Measure_Button");
        Info_Button = BumUITool.GetUIComponent<Transform>(FunctionPlane, "Info_Button");
        //OneLevelPlane.gameObject.AddComponent<Sort>();
        //modelMenuPage.TwoLevelPlane.gameObject.AddComponent<Sort>();
        //EditorPlane.gameObject.AddComponent<Sort>();
        //FunctionPlane.gameObject.AddComponent<Sort>();

    }

}
