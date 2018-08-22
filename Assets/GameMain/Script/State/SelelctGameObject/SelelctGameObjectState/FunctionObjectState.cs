using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionObjectState : Select3DState
{
    public const string Name = "FunctionObjectState";

    public override void enter()
    {
        base.enter();
        //if (inputStateMachine.targetTransform == null)
        //{
        //    return;
        //}
        UITool.SetActionFalse(modelMenuPage.OneLevelPlane.gameObject);
        UITool.SetActionTrue(modelMenuPage.FunctionPlane.gameObject);
    }
    public override void exit()
    {
        base.exit();
        //UITool.SetActionFalse(modelMenuPage.FunctionPlane.gameObject);
        //UITool.SetActionTrue(modelMenuPage.OneLevelPlane.gameObject);
    }
}
