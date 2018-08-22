using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoState : FunctionState
{
    public const string Name = "InfoState";
    ModelView modelView;
    public override void enter()
    {
        base.enter();
        // 显示表单
        if (inputStateMachine.targetTransform == null)
        {
            return;
        }
        modelView = inputStateMachine.targetTransform.GetComponent<ModelView>();
        if (modelView == null)
        {
            return;
        }
        //UITool.SetActionTrue(modelView.OverviewOfEquipment.gameObject);
    }
    public override void exit()
    {
        base.exit();
        //UITool.SetActionFalse(modelView.OverviewOfEquipment.gameObject);
    }

}
