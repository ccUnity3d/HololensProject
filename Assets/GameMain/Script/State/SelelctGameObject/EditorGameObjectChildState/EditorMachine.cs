using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMachine : BaseStateMachine<EditorState, EditorMachine>
{
    public override void Inject()
    {
        addState(HomeEditorState.Name,new HomeEditorState());
        addState(AdjustState.Name, new AdjustState());
        addState(AdjustCancelState.Name, new AdjustCancelState());
        addState(DestoryState.Name,new DestoryState());
        addState(PlaceState.Name,new PlaceState());
    }
    public void Ready()
    {
        foreach (var item in stateDic.Values)
        {
            item.Ready();
        }
    }
}
