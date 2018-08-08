using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGoodState : InputState {

    public const string Name = "SelectGoodState";
    public Transform targetTransform;
    public Transform threeDUI;
    public override void enter()
    {
        base.enter();
        if (gazeManager.HitObject.transform !=null)
        {
            targetTransform = gazeManager.HitObject.transform;
        }
        if (targetTransform == null)
        {
            inputStateMachine.setState(Free3DState.Name);
            return;
        }
        //if (targetTransform.GetComponent<ModelView>()==null)
        //{
        //    inputStateMachine.setState(Free3DState.Name);
        //    return;
        //}
        inputStateMachine.targetTransform = targetTransform;
        threeDUI = modelMenuPage.skin.transform ;
        Vector3 distanceUIByModel = gazeManager.HitPosition;
        threeDUI.rotation = Quaternion.identity;
        // 一直看着 相机
        threeDUI.position = gazeManager.HitPosition;
        threeDUI.forward = gazeManager.GazeNormal;
        threeDUI.Translate(-gazeManager.GazeNormal * 0.5f,Space.World);
        //threeDUI.position = distanceUIByModel.x * Vector3.right + distanceUIByModel.y * Vector3.up +Vector3.forward * (distanceUIByModel.z - .5f) * threeDUI.forward.z;
        //threeDUI.position = distanceUIByModel.x * Vector3.right + distanceUIByModel.y * Vector3.up + Vector3.forward * (distanceUIByModel.z - .5f) * threeDUI.forward.z;
        BumUITool.SetActionTrue(threeDUI.gameObject);
    

    }

    public override void exit()
    {
        base.exit();
        BumUITool.SetActionFalse(modelMenuPage.skin);
    }
    public override void Ready()
    {
        base.Ready();

    }
}
