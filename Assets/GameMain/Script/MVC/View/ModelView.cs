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

    public void SetData(BumModel bumModel) {
        this.modelData = bumModel;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        // 显示UI
        inputStateMachine.setState(SelectGoodState.Name);
        Debug.Log("enter SelectGoodState");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
