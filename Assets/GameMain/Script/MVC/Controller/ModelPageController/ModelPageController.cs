using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPageController : UIController<ModelPageController> {

    private ModelMenuPage modelMenuPage;
    private ModelPageController modelPageController;
    public override void OnInstance()
    {
        base.OnInstance();
        modelPageController = ModelPageController.Instance;
        page = modelMenuPage = ModelMenuPage.Instance;
    }
   
    public override void ready()
    {
        base.ready();
        //modelMenuPage.skin.AddComponent<Sort>();
        UITool.AddUIComponent<EditorView>(modelMenuPage.Editor_Button.gameObject);
        UITool.AddUIComponent<FunctionView>(modelMenuPage.Function_Button.gameObject);
        UITool.AddUIComponent<DestoryView>(modelMenuPage.Editor_Destory_Button.gameObject);
        UITool.AddUIComponent<PlaceView>(modelMenuPage.Editor_Place_Button.gameObject);
        UITool.AddUIComponent<AdjustView>(modelMenuPage.Editor_Adjust_Button.gameObject);
        UITool.AddUIComponent<HomeEditorView>(modelMenuPage.Editor_Home_Button.gameObject);
        UITool.AddUIComponent<HomeFunctionView>(modelMenuPage.Function_Home_Button.gameObject);
        UITool.AddUIComponent<MaterialView>(modelMenuPage.Material_Button.gameObject);
        UITool.AddUIComponent<MeasureView>(modelMenuPage.Measure_Button.gameObject);
        UITool.AddUIComponent<InfoView>(modelMenuPage.Info_Button.gameObject);
        UITool.AddUIComponent<SortView>(modelMenuPage.Sort_Button.gameObject);

        UITool.SetActionFalse(modelMenuPage.EditorPlane.gameObject);
        UITool.SetActionFalse(modelMenuPage.FunctionPlane.gameObject);
    }
}
