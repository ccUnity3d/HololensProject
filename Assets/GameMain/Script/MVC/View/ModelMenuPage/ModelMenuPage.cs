using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMenuPage :UIPage<ModelMenuPage> {

    public Transform OneLevelPlane;
    public Transform Editor_Button;
    public Transform Particular_Button;
    public Transform TwoLevelPlane;
    public Transform EditorPlane;
    public Transform Editor_Destory_Button;
    public Transform Editor_Move_Button;
    public Transform Editor_Rotate_Button;
    public Transform Editor_Scale_Button;
    public Transform Editor_Home_Button;
    public Transform Particular_Home_Button;
    public Transform ParticularPlane;
    public Transform Particular_OverviewOfEquipment_Button;
    public Transform Particular_BimFrom_Button;
    public Transform Particular_Explode_Button;
    public Transform Particular_PrinciplePlay_Button;

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
        Particular_Button = BumUITool.GetUIComponent<Transform>(OneLevelPlane, "Particular_Button");
        TwoLevelPlane = BumUITool.GetUIComponent<Transform>(skin.transform, "TwoLevel");
        EditorPlane = BumUITool.GetUIComponent<Transform>(TwoLevelPlane, "EditorPlane");
        Editor_Destory_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Destory_Button");
        Editor_Move_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Move_Button");
        Editor_Rotate_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Rotate_Button");
        Editor_Scale_Button = BumUITool.GetUIComponent<Transform>(EditorPlane,"Editor_Scale_Button");
        Editor_Home_Button = BumUITool.GetUIComponent<Transform>(EditorPlane, "Editor_Home_Button");
        ParticularPlane = BumUITool.GetUIComponent<Transform>(TwoLevelPlane, "ParticularPlane");
        Particular_OverviewOfEquipment_Button = BumUITool.GetUIComponent<Transform>(ParticularPlane, "Particular_OverviewOfEquipment_Button");
        Particular_BimFrom_Button = BumUITool.GetUIComponent<Transform>(ParticularPlane, "Particular_BimFrom_Button");
        Particular_Explode_Button = BumUITool.GetUIComponent<Transform>(ParticularPlane, "Particular_Explode_Button");
        Particular_PrinciplePlay_Button = BumUITool.GetUIComponent<Transform>(ParticularPlane, "Particular_PrinciplePlay_Button");
        Particular_Home_Button = BumUITool.GetUIComponent<Transform>(ParticularPlane, "Particular_Home_Button");
        OneLevelPlane.gameObject.AddComponent<Sort>();
        //modelMenuPage.TwoLevelPlane.gameObject.AddComponent<Sort>();
        EditorPlane.gameObject.AddComponent<Sort>();
        ParticularPlane.gameObject.AddComponent<Sort>();

    }

}
