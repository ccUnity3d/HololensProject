using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeFunctionState : FunctionState
{
    public const string Name = "HomeFunctionState";
    // Use this for initialization
    public override void enter()
    {
        base.enter();
        // 返回上一级菜单
        UITool.SetActionTrue(modelMenuPage.OneLevelPlane.gameObject);
        UITool.SetActionFalse(modelMenuPage.FunctionPlane.gameObject);
    }
    public override void exit()
    {
        base.exit();
    }
}
