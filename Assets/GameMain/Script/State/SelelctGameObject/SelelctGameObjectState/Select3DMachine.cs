using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select3DMachine : BaseStateMachine<Select3DState, Select3DMachine>
{

    public override void Inject()
    {
        addState(SelectFreeGameState.Name,new SelectFreeGameState());
        addState(EditorGameObjectState.Name,new EditorGameObjectState());
        addState(FunctionObjectState.Name,new FunctionObjectState());
    }
    public void Ready()
    {
        foreach (var item in stateDic.Values)
        {
            item.Ready();
        }
    }
}
