using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.GazeRuler;
public class MeasureState : FunctionState
{
    public const string Name = "ExPlodeExitState";
    public bool idDown = false;
    private MeasureManager measureManager {
        get { return MeasureManager.Instance; }
    }
    ModelView modelView;
    public override void enter()
    {
        base.enter();
        measureManager.isMeasureManager = true;
    }
    public override void exit()
    {
        base.exit();
        measureManager.isMeasureManager = false;

    }
}
