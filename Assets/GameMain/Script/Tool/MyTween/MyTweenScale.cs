using UnityEngine;

public class MyTweenScale : MyTween
{
    [HideInInspector]
    public Vector3 from = Vector3.one;
    [HideInInspector]
    public Vector3 to = Vector3.one;

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
            return mtran.localScale;
        }
        set
        {
            mtran.localScale = value;
        }
    }

    public override void ResetToBeginning()
    {
        base.ResetToBeginning();
        value = from;
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
