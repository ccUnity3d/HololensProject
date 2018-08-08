using UnityEngine;

public class MyTweenPosition : MyTween
{
    [HideInInspector]
    public Vector3 from;
    [HideInInspector]
    public Vector3 to;
    [HideInInspector]
    public bool worldSpace = false;

    private Transform mtran;

    protected override void OnEnable()
    {
        base.OnEnable();
        mtran = transform;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnUpdate(float factor)
    {
        value = Vector3.Lerp(from, to, factor);
    }

    public Vector3 value
    {
        get
        {
            return worldSpace ? mtran.position : mtran.localPosition;
        }
        set
        {
            if (worldSpace) mtran.position = value;
            else mtran.localPosition = value;
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
