using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStateMachine : BaseStateMachine<InputState, InputStateMachine> {

    public Transform targetTransform;
    public override void Inject()
    {
        addState(Free3DState.Name,new Free3DState());
        addState(SelectGoodState.Name, new SelectGoodState());
        addState(GazeMoveState.Name,new GazeMoveState());
    }
    public  void Ready()
    {
        foreach (InputState item in stateDic.Values)
        {
            item.Ready();
        }
    }
}
