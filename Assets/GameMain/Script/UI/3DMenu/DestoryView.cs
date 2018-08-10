using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class DestoryView : MonoBehaviour , BaseInput
{
    private string material_normal;
    private string material_Highlighted;
    private MeshRenderer meshRenderer;

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

    private InputStateMachine inputStateMachine
    {
        get
        {
            return InputStateMachine.Instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        material_normal = "mat_button_Destory_01";
        material_Highlighted = "mat_button_Destory_02";
    }

    public void OnFocusEnter()
    {
        audioManager.OnEnterAudio();
        meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_Highlighted];
    }

    public void OnFocusExit()
    {
        meshRenderer.sharedMaterial = ResourcesPacker.mLaodMater[material_normal];
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (inputStateMachine.targetTransform == null)
        {
            return;
        }
        audioManager.OnClickAudio();
        editorMachine.setState(DestoryState.Name);
        UITool.SetActionFalse(ModelMenuPage.Instance.EditorPlane.gameObject);
        UITool.SetActionFalse(ModelMenuPage.Instance.skin.transform.gameObject);
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
