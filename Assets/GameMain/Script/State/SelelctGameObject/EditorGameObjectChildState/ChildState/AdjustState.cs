using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustState : EditorState {

    public const string Name = "AdjustState";
    private ShowBoundingBoxRig showBoundingBoxRig {
        get {
            return ShowBoundingBoxRig.Instance;
        }
    }
    public override void enter()
    {
        base.enter();
        // 旋转
        showBoundingBoxRig.SetTarget(inputStateMachine.targetTransform.gameObject);
        showBoundingBoxRig.Activate();
    }

    public override void exit()
    {
        base.exit();
        showBoundingBoxRig.Deactivate();
    }
}
