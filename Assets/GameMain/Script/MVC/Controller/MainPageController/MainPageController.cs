using System;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Sharing;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using UnityEngine;
using UnityEngine.EventSystems;



public class MainPageController :UIController<MainPageController> {

    public MainPage mainPage;
    public List<ItemData> items;
    private MyTweenScale myTweenScale;
    private GlobalConfig globalconfig;
    private Transform currentLoadObject;
    public Transform m_currentLoadObject {
    get
        {
            return currentLoadObject;
        }
        set {
            currentLoadObject = value;
        }
    }
    public UnityEngine.XR.WSA.Persistence.WorldAnchorStore worldAnchorStore;

    private InputStateMachine inputStateMachine
    {
        get
        {
            return InputStateMachine.Instance;
        }
    }

    public bool isManipulation;
    public override void OnInstance()
    {
        base.OnInstance();
        page = mainPage = MainPage.Instance;
        globalconfig = GlobalConfig.Instance;
    }

 
    public override void ready()
    {
        base.ready();
        items = new List<ItemData>();
        foreach (BumModel item in BumLogic.objectManager.modelCollection.Values)
        {
            items.Add(item);
        }
        mainPage.producScroll.OnModelClick = OnProductItemClickByShowCAD;
        mainPage.downButton.onClick.AddListener(OnDownButton);
        //mainPage.downRemoveButton.onClick.AddListener(OnDownRemoveButton);
        mainPage.adjustButton.onClick.AddListener(OnAdjustButton);
        mainPage.hideButton.onClick.AddListener(OnHideButton);
        mainPage.cancelButton.onClick.AddListener(OnDownRemoveButton);
        mainPage.extendButton.onClick.AddListener(OnExtendButton);

        mainPage.producScroll.Display(items);
        myTweenScale = mainPage.ProductPlane.gameObject.AddComponent<MyTweenScale>();
        myTweenScale.from = Vector3.zero;
        myTweenScale.to = Vector3.one;
        myTweenScale.style = Style.Once;
        myTweenScale.moveType = MoveType.Linear;
        myTweenScale.duration = .5f;
        
        inputStateMachine.Ready();
        // 打开3D 菜单
        UIManager.Instance.OnOpen(PageType.ModelMenuPage);
    }

    private void OnHideButton()
    {
        myTweenScale.PlayReverse();
        BumUITool.SetActionTrue(mainPage.extendButton.gameObject);
        Debug.Log("隐藏面板");
    }

    private void OnLoadGameObject(object obj)
    {
        SimpleLoader simple = obj as SimpleLoader;
        GameObject go = simple.loadedData as GameObject;
        //ModelView modelview = go.AddComponent<ModelView>();
        //modelview.Init();
        go.transform.parent = GlobalConfig.Instance.worldAnchor;
    }

  

    private void OnExtendButton()
    {
        myTweenScale.PlayForward();
        BumUITool.SetActionFalse(mainPage.extendButton.gameObject);
    }

    private void OnProductItemClickByShowCAD(BumModel bumModel)
    {
        if (this.currentLoadObject==null) {
            BumResourceManager.loadResource(bumModel.modelUri, null, null, BumResourceType.eBumResourceType_assetBundle, BumLoadingType.eBumLoadingType_www, BumResourcePoolType.ProductObject, bumModel, OnLoad);
        }
    }

    private void OnLoad( GameObject go, object obj)
    {
        BumModel model = (BumModel)obj;
        int PlacementSurfacesDic = model.PlacementSurfaces; 
        go = GameObject.Instantiate<GameObject>(go);
        //go.transform.localScale = Vector3.one ;
        inputStateMachine.targetTransform = go.transform;
        ModelView modelView = go.AddComponent<ModelView>();
        modelView.SetData(model);
        Placeable placeable = go.AddComponent<Placeable>();
        go.AddComponent<TwoHandManipulatable>();
        if (PlacementSurfacesDic == 0)
        {
            placeable.PlacementSurface = PlacementSurfaces.Horizontal;
        }
        else {
            placeable.PlacementSurface = PlacementSurfaces.Vertical;
        }
        placeable.OnSelected();
        inputStateMachine.setState(GazeMoveState.Name);
        go.transform.parent = globalconfig.worldAnchor;
        this.currentLoadObject = go.transform;
    }

    private void OnAdjustButton()
    {
        //BumUITool.SetActionFalse(mainPage.adjustButton.gameObject);
        //BumUITool.SetActionTrue(mainPage.downButton.gameObject);
        //BumUITool.SetActionTrue(mainPage.adjustConfirm.gameObject);
        //BumUITool.SetActionFalse(mainPage.AdjustPlane.gameObject);
        BumUITool.SetActionFalse(mainPage.adjustButton.gameObject);
        BumUITool.SetActionFalse(mainPage.hideButton.gameObject);
        BumUITool.SetActionTrue(mainPage.downButton.gameObject);
        BumUITool.SetActionTrue(mainPage.cancelButton.gameObject);
        isManipulation = true;
        DebugX.Log("OnAdjustButton");
    }
    // cancel
    private void OnDownRemoveButton()
    {
        isManipulation = false;
        BumUITool.SetActionTrue(mainPage.adjustButton.gameObject);
        BumUITool.SetActionTrue(mainPage.hideButton.gameObject);
        BumUITool.SetActionFalse(mainPage.downButton.gameObject);
        BumUITool.SetActionFalse(mainPage.cancelButton.gameObject);
        DebugX.Log("OnDownRemoveButton");
    }

    private void OnDownButton()
    {
        isManipulation = false;
        BumUITool.SetActionTrue(mainPage.adjustButton.gameObject);
        BumUITool.SetActionTrue(mainPage.hideButton.gameObject);
        BumUITool.SetActionFalse(mainPage.downButton.gameObject);
        BumUITool.SetActionFalse(mainPage.cancelButton.gameObject);
        DebugX.Log("OnDownButton");
    }
}
