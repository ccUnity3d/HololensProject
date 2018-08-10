using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Free3DState : InputState {

    public const string Name = "Free3DState";
    TwoHandManipulatable twoHandManipulatable;
    public override void enter()
    {
        base.enter();
        inputStateMachine.targetTransform = null;
    }

    public override void exit()
    {
        base.exit();
    }
}
