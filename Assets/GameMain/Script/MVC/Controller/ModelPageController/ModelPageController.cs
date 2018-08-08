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
    }
}
