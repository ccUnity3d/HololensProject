using UnityEngine;
using System.Collections;

public interface IStateMachine<T> : IStateMachine
{
    void addState(string stateName, T type);
}

public interface IStateMachine
{
    void Inject();
    void setState(string stateName);
}