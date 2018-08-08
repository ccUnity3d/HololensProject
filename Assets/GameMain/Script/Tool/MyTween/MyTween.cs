using UnityEngine;
using System;


public abstract class MyTween : MonoBehaviour
{
    [HideInInspector]
    public MoveType moveType = MoveType.Linear;
    [HideInInspector]
    public Style style = Style.Once;

    [HideInInspector]
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f),
        new Keyframe(1f, 1f, 1f, 0f));

    [HideInInspector]
    public bool ignoreTimeScale = false;
    [HideInInspector]
    public float delay = 0f;
    [HideInInspector]
    public float duration = 1f;

    private Action<object[]> onFinished;
    private object[] args;
    private bool mStarted = false;
    private float mStartTime = 0f;
    private float mDuration = 0f;
    private float mAmountPerDelta = 1000f;
    private float mFactor = 0f;

    private float realtime = -1;

    public void SetFinishFunc(Action<object[]> onFinished, params object[] args)
    {
        this.onFinished = onFinished;
        this.args = args;
    }

    public float amountPerDelta
    {
        get
        {
            if (mDuration != duration)
            {
                mDuration = duration;
                mAmountPerDelta = Mathf.Abs((duration > 0f) ? 1f / duration : 1000f) * Mathf.Sign(mAmountPerDelta);
            }
            return mAmountPerDelta;
        }
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
        mStarted = false;
    }

    public virtual void ResetToBeginning()
    {
        mStarted = false;
        mFactor = (amountPerDelta < 0f) ? 1f : 0f;
        Sample(mFactor);
    }

    public virtual void ResetToEnd()
    {
        mStarted = false;
        mFactor = (amountPerDelta < 0f) ? 0f : 1f;
        Sample(mFactor);
    }

    public void PlayForward()
    {
        Play(true);
    }

    public void PlayReverse()
    {
        Play(false);
    }

    private void Play(bool forward)
    {
        mStarted = false;
        mFactor = forward ? 0f : 1f;
        mAmountPerDelta = Mathf.Abs(amountPerDelta);
        if (!forward) mAmountPerDelta = -mAmountPerDelta;
        enabled = true;

    }

    public virtual void SetStartToCurrentValue()
    {
    }

    public virtual void SetEndToCurrentValue()
    {
    }

    protected virtual void Update()
    {
        float deltaT = 0;
        if (realtime != -1)
        {
            deltaT = Time.realtimeSinceStartup - realtime;
        }
        float delta = ignoreTimeScale ? deltaT : Time.deltaTime;
        float time = ignoreTimeScale ? Time.realtimeSinceStartup : Time.time;

        realtime = Time.realtimeSinceStartup;

        if (!mStarted)
        {
            mStarted = true;
            mStartTime = time + delay;
        }

        if (time < mStartTime) return;

        // Advance the sampling factor
        mFactor += amountPerDelta * delta;

        // Loop style simply resets the play factor after it exceeds 1.
        if (style == Style.Loop)
        {
            if (mFactor > 1f)
            {
                mFactor -= Mathf.Floor(mFactor);
            }
        }
        else if (style == Style.PingPong)
        {
            // Ping-pong style reverses the direction
            if (mFactor > 1f)
            {
                mFactor = 1f - (mFactor - Mathf.Floor(mFactor));
                mAmountPerDelta = -mAmountPerDelta;
            }
            else if (mFactor < 0f)
            {
                mFactor = -mFactor;
                mFactor -= Mathf.Floor(mFactor);
                mAmountPerDelta = -mAmountPerDelta;
            }
        }

        // If the factor goes out of range and this is a one-time tweening operation, disable the script
        if ((style == Style.Once) && (duration == 0f || mFactor > 1f || mFactor < 0f))
        {
            mFactor = Mathf.Clamp01(mFactor);
            Sample(mFactor);
            enabled = false;

            if (onFinished != null)
            {
                onFinished(args);
            }
        }
        else Sample(mFactor);
    }

    public void Sample(float factor)
    {
        // Calculate the sampling value
        float val = Mathf.Clamp01(factor);

        if (moveType == MoveType.EaseIn)
        {
            val = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - val));
        }
        else if (moveType == MoveType.EaseOut)
        {
            val = Mathf.Sin(0.5f * Mathf.PI * val);
        }
        else if (moveType == MoveType.EaseInOut)
        {
            const float pi2 = Mathf.PI * 2f;
            val = val - Mathf.Sin(val * pi2) / pi2;
        }
        else if (moveType == MoveType.BounceIn)
        {
            val = BounceLogic(val);
        }
        else if (moveType == MoveType.BounceOut)
        {
            val = 1f - BounceLogic(1f - val);
        }

        // Call the virtual update
        OnUpdate((curve != null) ? curve.Evaluate(val) : val);
    }

    protected abstract void OnUpdate(float factor);

    private float BounceLogic(float val)
    {
        if (val < 0.363636f) // 0.363636 = (1/ 2.75)
        {
            val = 7.5685f * val * val;
        }
        else if (val < 0.727272f) // 0.727272 = (2 / 2.75)
        {
            val = 7.5625f * (val -= 0.545454f) * val + 0.75f; // 0.545454f = (1.5 / 2.75) 
        }
        else if (val < 0.909090f) // 0.909090 = (2.5 / 2.75) 
        {
            val = 7.5625f * (val -= 0.818181f) * val + 0.9375f; // 0.818181 = (2.25 / 2.75) 
        }
        else
        {
            val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
        }
        return val;
    }

}

