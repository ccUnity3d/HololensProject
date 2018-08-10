using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionMachine : BaseStateMachine<FunctionState, FunctionMachine> {

    public override void Inject()
    {
        addState(HomeFunctionState.Name, new HomeFunctionState());
        addState(InfoState.Name, new InfoState());
        addState(MaterialState.Name, new MaterialState());
        addState(MeasureState.Name, new MeasureState());
        addState(SortState.Name, new SortState());
    }

    public void Ready()
    {
        foreach (var item in stateDic.Values)
        {
            item.Ready();
        }
    }
}
