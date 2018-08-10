using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEditorState : EditorState {

    public const string Name = "HomeEditorState";

    public override void enter()
    {
        base.enter();
        UITool.SetActionTrue(modelMenuPage.OneLevelPlane.gameObject);
        UITool.SetActionFalse(modelMenuPage.EditorPlane.gameObject);
    }
    public override void exit()
    {
        base.exit();
    }

}
