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
        twoHandManipulatable = inputStateMachine.targetTransform.GetComponent<TwoHandManipulatable>();
        if (twoHandManipulatable == null)
        {
            twoHandManipulatable = inputStateMachine.targetTransform.gameObject.AddComponent<TwoHandManipulatable>();
        }
        twoHandManipulatable.enabled = true;
    }

    public override void exit()
    {
        base.exit();
        twoHandManipulatable.enabled = false;
    }
}
