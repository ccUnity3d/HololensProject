using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelView : MonoBehaviour , IInputClickHandler
{
    public BumModel modelData;

    protected InputStateMachine inputStateMachine
    {
        get
        {
            return InputStateMachine.Instance;
        }
    }
    protected FunctionMachine FunctionMachine {
    get {
            return FunctionMachine.Instance;
        } }

    public void SetData(BumModel bumModel) {
        this.modelData = bumModel;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //if (FunctionMachine.CurrentState is MeasureState) {
        //}
        //else
        //{
            inputStateMachine.setState(SelectGoodState.Name);
        //}
        // 显示UI
        Debug.Log("enter SelectGoodState");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
