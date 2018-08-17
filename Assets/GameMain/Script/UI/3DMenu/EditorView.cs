using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class EditorView : MonoBehaviour , BaseInput
{
    private string material_normal;
    private string material_Highlighted;
    private MeshRenderer meshRenderer;
    private Select3DMachine select3DMachine
    {
        get
        {
            return Select3DMachine.Instance;
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
        material_normal = "mat_button_Editor_01";
        material_Highlighted = "mat_button_Editor_02";
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
        select3DMachine.setState(EditorGameObjectState.Name);
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
