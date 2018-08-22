using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class InfoView : MonoBehaviour , BaseInput
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
        material_normal = "mat_button_Info_01";
        material_Highlighted = "mat_button_Info_02";
    }

    public void OnFocusEnter()
    {
        audioManager.OnEnterAudio();
        //meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_Highlighted];
    }

    public void OnFocusExit()
    {
        //meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_normal];
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        audioManager.OnClickAudio();
        functionMachine.setState(InfoState.Name);
    }

    public void OnInputDown(InputEventData eventData)
    {
    }

    public void OnInputUp(InputEventData eventData)
    {
    }

}
