using UnityEngine;
using System;

public class MyTweenValue : MyTween {

    [HideInInspector]
    public float from;
    [HideInInspector]
    public float to;

    public Action<float> deleValue;

    private float value;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnUpdate(float factor)
    {
        value = (to - from) * factor + from;

        if (deleValue != null)
        {
            deleValue(value);
        }
    }

    public override void ResetToBeginning()
    {
        base.ResetToBeginning();
        value = from;
    }

    public override void ResetToEnd()
    {
        base.ResetToEnd();
        value = to;
    }

    public override void SetEndToCurrentValue()
    {
        base.SetEndToCurrentValue();
        to = value;
    }

    public override void SetStartToCurrentValue()
    {
        base.SetStartToCurrentValue();
        from = value;
    }
}
