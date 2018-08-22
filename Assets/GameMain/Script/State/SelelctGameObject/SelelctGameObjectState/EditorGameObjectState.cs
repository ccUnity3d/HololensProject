using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGameObjectState : Select3DState
{
    public const string Name = "EditorGameObjectState";

    public override void enter()
    {
        base.enter();
        //if (inputStateMachine.targetTransform==null)
        //{
        //    return;
        //}

        UITool.SetActionFalse(modelMenuPage.OneLevelPlane.gameObject);
        UITool.SetActionTrue(modelMenuPage.EditorPlane.gameObject);
    }
    public override void exit()
    {
        base.exit();
        //UITool.SetActionFalse(modelMenuPage.EditorPlane.gameObject);

    }

}
