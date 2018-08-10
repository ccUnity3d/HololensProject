﻿using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MyState {


    public ModelMenuPage modelMenuPage
    {
        get
        {
            return ModelMenuPage.Instance;
        }
    }


    protected InputStateMachine inputStateMachine
    {
        get
        {
            return InputStateMachine.Instance;
        }
    }

    protected FunctionMachine functionMachine
    {
        get
        {
            return FunctionMachine.Instance;
        }
    }
    protected Select3DMachine select3DMachine
    {
        get
        {
            return Select3DMachine.Instance;
        }
    }

    protected GazeManager gazeManager
    {
        get
        {
            return GazeManager.Instance;
        }
    }
    public override void enter()
    {
        base.enter();
    }

    public override void exit()
    {
        base.exit();
    }
    public override void mUpdate()
    {
        base.mUpdate();
    }

    public virtual void Ready()
    {

    }
}
