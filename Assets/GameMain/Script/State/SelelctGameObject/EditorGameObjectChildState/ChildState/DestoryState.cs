using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryState : EditorState
{
   
    public const string Name = "DestoryState";

    private GlobalConfig globalConfig
    {
        get
        {
            return GlobalConfig.Instance;
        }
    }

    private MainPageController mainPageController
    {
        get
        {
            return MainPageController.Instance;
        }
    }

    public override void enter()
    {
        base.enter();
        if (inputStateMachine.targetTransform == null)
        {
            return;
        }
        GameObject.DestroyObject(inputStateMachine.targetTransform.gameObject, 0.1f);
        inputStateMachine.targetTransform = null;
        inputStateMachine.setState(Free3DState.Name);
        //functionMachine.setState(HomeFunctionState.Name);
        //if (globalConfig.IsLoadWorldAnchorStore)
        //{
        //    string[] ids = mainPageController.worldAnchorStore.GetAllIds();
        //    if (ids.Length ==0)
        //    {
        //        return;
        //    }
        //    if (ids.Equals(inputStateMachine.targetTransform.gameObject.name))
        //    {
        //        bool deleted = mainPageController.worldAnchorStore.Delete(inputStateMachine.targetTransform.gameObject.name);
        //    }
        //}
    }

    public override void exit()
    {
        base.exit();
    }
}
