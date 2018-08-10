using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortState : FunctionState
{
    public const string Name = "OverviewOfEquipmentState";
    ModelView modelView;
    public override void enter()
    {
        base.enter();
        //  // 显示设备概述
        if (inputStateMachine.targetTransform == null)
        {
            return;
        }
        modelView = inputStateMachine.targetTransform.GetComponent<ModelView>();
        if (modelView == null)
        {
            return;
        }
        
        //UITool.SetActionTrue(modelView.BimFrom.gameObject);
    }
    public override void exit()
    {
        base.exit();
        //UITool.SetActionFalse(modelView.BimFrom.gameObject);

    }
}
