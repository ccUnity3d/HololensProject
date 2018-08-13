using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MySingleton<UIManager> {

    protected Dictionary<PageType, UIControllerData> controllerDic = new Dictionary<PageType, UIControllerData>();
    protected Dictionary<PageType, Action<bool>> WaitLoadDic = new Dictionary<PageType, Action<bool>>();
    private readonly bool DestroyWhenClose = true;

    private MainPageController mapPageController
    {
        get
        {
            return MainPageController.Instance;
        }
    }

    private Transform uiParant
    {
        get
        {
            return GlobalConfig.Instance.uiRoot;
        }
    }

    public override void OnInstance()
    {
        base.OnInstance();
        Inject();
    }

    public void Inject()
    {
        inject(PageType.ModelMenuPage, ModelPageController.Instance);
        //inject(PageType.LoginPage,LoginPageController.Instance);
        inject(PageType.MainPage,MainPageController.Instance);
      
    }

    private UIControllerData inject(PageType pageType, IController uiControllerData)
    {
        UIControllerData controllerData;
        if (controllerDic.TryGetValue(pageType,out controllerData) == true)
        {
            Debug.LogError("Controller注册重复：" + pageType);
            return controllerData;
        }
        controllerData = new UIControllerData(uiControllerData);
        controllerDic.Add(pageType, controllerData);
        return controllerData;
    }

    public void OnOpen(PageType pageType,Action<bool> onOpened = null )
    {
        onOpen(pageType, onOpened);
    }
    public IEnumerator OnOpenDelay(PageType pageType,float delay)
    {
        yield return new WaitForSeconds(delay);
        onOpen(pageType);
    }
    private void onOpen(PageType pageType, Action<bool> onOpen = null)
    {
        if (controllerDic.ContainsKey(pageType) == false)
        {
            Debug.Log("未注册");
            return;
        }
        UIControllerData ControllerData = controllerDic[pageType];
        ControllerData.currentType = 1;
        switch (ControllerData.simpleLoadedState)
        {
            case SimpleLoadedState.None:
                if (WaitLoadDic.ContainsKey(pageType)==false)
                    WaitLoadDic.Add(pageType, onOpen);
                else
                    WaitLoadDic[pageType] = onOpen;
                LoadPage(pageType);
                break;
            case SimpleLoadedState.Loading:
                if (WaitLoadDic.ContainsKey(pageType) == false)
                    WaitLoadDic.Add(pageType, onOpen);
                else
                    WaitLoadDic[pageType] = onOpen;
                break;
            case SimpleLoadedState.Success:
                if (controllerDic[pageType].skin==null)
                {
                    controllerDic[pageType].skin = controllerDic[pageType].controller.getPage.skin;
                }
                controllerDic[pageType].skin.SetActive(true);
                controllerDic[pageType].controller.awake();
                if (onOpen != null) onOpen(true); 
                break;
            case SimpleLoadedState.Failed:
                break;
            default:
                if (onOpen != null) onOpen(false);
                //上一次加载错误
                break;
        }
    }


    private void close(PageType page)
    {
        if (controllerDic.ContainsKey(page) == false)
        {
            Debug.Log(page+"未注册");
            return;
        }
        controllerDic[page].currentType = 0;
        switch (controllerDic[page].simpleLoadedState)
        {
            
            case SimpleLoadedState.None:
                Debug.Log(page.ToString());

                break;
            case SimpleLoadedState.Loading:
                Debug.Log(page.ToString());

                break;
            case SimpleLoadedState.Failed:
                Debug.Log(page.ToString());

                break;
            case SimpleLoadedState.Success:
                Debug.Log(page.ToString());

                controllerDic[page].controller.sleep();
                if (DestroyWhenClose == true && controllerDic[page].destroyable == true)
                {
                    controllerDic[page].skin.SetActive(false);
                }
                else
                {
                    controllerDic[page].simpleLoadedState = SimpleLoadedState.None;
                    //ResourcePool.Dispos(controllerDic[page].skin);
                }
                break;
            default:
                break;
        }
    }

    public void Close(PageType page)
    {
        Instance.close(page);
    }

    public IEnumerator CloseDelay(PageType page,float delay)
    {
        yield return new WaitForSeconds(delay);
        Instance.close(page);
    }
    private void LoadPage(PageType pageType)
    {
        try
        {
            string prefabPath = controllerDic[pageType].controller.getPage.GetPrefabPath();
            MyLoader loader = new MyLoader();
            loader.LoadPrefab(prefabPath, 0, onLoaded, pageType);
        }
        catch (Exception e)
        {
            Debug.Log(" LoadPage " + e.ToString());
            throw;
        }
      
    }

    private void onLoaded(UnityEngine.Object arg1,object arg2)
    {

        try
        {
            PageType pageType = (PageType)arg2;
            if (arg1 != null)
            {
                GameObject goClone = arg1 as GameObject;
                goClone.SetActive(true);
                goClone.transform.SetParent(uiParant);
                UIControllerData controllerData = controllerDic[pageType];
                controllerData.controller.SetData(goClone, false);
                controllerData.skin = goClone;
                controllerData.simpleLoadedState = SimpleLoadedState.Success;
                if (controllerData.currentType == 0)
                {
                    controllerData.controller.sleep();
                }
                if (WaitLoadDic[pageType] != null) WaitLoadDic[pageType](controllerDic[pageType].currentType == 1);
            }
            else
            {
                if (WaitLoadDic[pageType] != null) WaitLoadDic[pageType](false);
            }
        }
        catch (Exception e)
        {
            Debug.Log(" onLoaded" + e.ToString());

        }
        finally
        {
            //Debug.Log(" onLoaded");
        }



    }
}
public enum SimpleLoadDataType
{

    Byte,
    Json,
    Texture2D,
    prefabAssetBundle,
    prefab,
}
public enum SimpleLoadedState
{
    None,
    Loading,
    Failed,
    Success
}
public class UIControllerData
{
    public IController controller;

    public int currentType;

    public GameObject skin;

    public SimpleLoadedState simpleLoadedState = SimpleLoadedState.None;

    public bool destroyable = true;

    public UIControllerData(IController controller)
    {
        this.controller = controller;
    }

}

public enum PageType
{
    LoginPage,
    MultiPage,
    MainPage,
    ModelMenuPage,
    Rest,

}