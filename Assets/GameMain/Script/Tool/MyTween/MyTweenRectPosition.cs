using UnityEngine;
using System.Collections;
using System;

public class MyTweenRectPosition : MyTween
{
    [HideInInspector]
    public Vector2 from;
    [HideInInspector]
    public Vector2 to;
    [HideInInspector]
    public bool worldSpace = false;

    private RectTransform mtran;



    private float _speed;
    [HideInInspector]
    public float speed
    {
        set
        {
            _speed = value;
            if (speed < 0.01f)
            {
                duration = 0;
            }
            else
            {
                duration = (to - from).magnitude / speed;
            }
        }
        get
        {
            return _speed;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        mtran = transform.GetComponent<RectTransform>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnUpdate(float factor)
    {
        value = Vector3.Lerp(from, to, factor);
    }

    public Vector2 value
    {
        get
        {
            return worldSpace ? mtran.position : mtran.anchoredPosition3D;
        }
        set
        {
            if (worldSpace)
            {
                temp = value;
                temp.z = mtran.position.z;
                mtran.position = temp;
            }
            else
            {
                temp = value;
                temp.z = mtran.anchoredPosition3D.z;
                mtran.anchoredPosition3D = temp;
            }
        }
    }

    private Vector3 temp = Vector3.zero;

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
