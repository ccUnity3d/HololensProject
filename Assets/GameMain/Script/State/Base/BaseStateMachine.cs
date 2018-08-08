using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// T 表示 State   childeType表示 State 每个子状态Machine
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="ChildType"></typeparam>
public abstract class BaseStateMachine<T, ChildType> : MySingleton<ChildType>, IStateMachine<T>
    where T : IMyState
    where ChildType : IInstance, IStateMachine<T>, new ()
{
    protected Dictionary<string, T> stateDic = new Dictionary<string, T>();

    public bool nextIsCurrent { get; set; }

    public override void OnInstance()
    {
        Inject();
    }

    private T currentState = default(T);
    public T CurrentState
    {
        get
        {
            return currentState;
        }
    }

    private T nextState = default(T);
    public T NexttState
    {
        get
        {
            return nextState;
        }
    }

    public abstract void Inject();

    public void addState(string stateName, T mode)
    {
        if (stateDic.ContainsKey(stateName) == true)
        {
            return;
        }
        stateDic.Add(stateName, mode);
    }

    public void setState(string stateName)
    {
        if (stateDic.ContainsKey(stateName) == false)
        {
            Debug.Log(typeof(ChildType).Name + " 未注册： " + stateName);
            return;
        }
        T aimState = stateDic[stateName];
        nextState = aimState;
        nextIsCurrent = false;
        if (currentState != null)//currentMode != aimMode//新旧状态可以相同
        {
            if (nextState.GetType() == currentState.GetType())
            {
                nextIsCurrent = true;
            }
            Debug.Log(typeof(ChildType).Name + " " + currentState.GetType().Name + " Exit()");
            currentState.Exit();
        }
        currentState = aimState;
        Debug.Log(typeof(ChildType).Name + " " + aimState.GetType().Name + " Enter()");
        aimState.Enter();
    }

    public T getState(string stateName)
    {
        if (stateDic.ContainsKey(stateName) == false) return default(T);
        return stateDic[stateName];
    }

    public static void SetState(string stateName)
    {
        Instance.setState(stateName);
    }

}
