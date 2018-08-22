using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using HoloToolkit.Examples.GazeRuler;
public class MeasureView : MonoBehaviour ,BaseInput
{
    private string material_normal;
    private string material_Highlighted;
    private MeshRenderer meshRenderer;
    private FunctionMachine functionMachine
    {
        get
        {
            return FunctionMachine.Instance;
        }
    }
    private AudioManager audioManager
    {
        get
        {
            return AudioManager.Instance;
        }
    }
    private ResourcesPacker ResourcesPacker
    {
        get
        {
            return ResourcesPacker.Instance;
        }
    }
    private void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        material_normal = "mat_button_Measure_01";
        material_Highlighted = "mat_button_Measure_02";
        
    }

    public void OnFocusEnter()
    {
        //meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_Highlighted];
    }

    public void OnFocusExit()
    {
        //meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_normal];
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        audioManager.OnClickAudio();
        functionMachine.setState(MeasureState.Name);
    }

    public void OnInputDown(InputEventData eventData)
    {
    }

    public void OnInputUp(InputEventData eventData)
    {
    }
}
