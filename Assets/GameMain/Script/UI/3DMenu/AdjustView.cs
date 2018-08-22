using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class AdjustView : MonoBehaviour, BaseInput
{
    private string material_normal;
    private string material_Highlighted;
    private MeshRenderer meshRenderer;
    private bool toggle = false;
    private Select3DMachine select3DMachine
    {
        get
        {
            return Select3DMachine.Instance;
        }
    }
    private ResourcesPacker ResourcesPacker
    {
        get
        {
            return ResourcesPacker.Instance;
        }
    }
    private EditorMachine editorMachine
    {
        get
        {
            return EditorMachine.Instance;
        }
    }
    private AudioManager audioManager
    {
        get
        {
            return AudioManager.Instance;
        }
    }
    private void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        material_normal = "mat_button_Adjust_01";
        material_Highlighted = "mat_button_Adjust_02";
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
        toggle = !toggle;
        if (toggle)
        {
            editorMachine.setState(AdjustState.Name);
        }
        else
        {
            editorMachine.setState(AdjustCancelState.Name);
        }
    }

    public void OnInputDown(InputEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnInputUp(InputEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
