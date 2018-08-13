using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using System.Net;
using System.ComponentModel;

interface IBumInitable
{
    IEnumerator init();
    IEnumerator uninit();
    bool getLoadingState();
}

public class BumLogic
{
    private List<IBumInitable> initModula = new List<IBumInitable>();
    public static BumLogic Instance = null;

    //runtime controller
    public static BumInputController inputController;
    public static BumErrorHandler errorHandler;

    public static BumUser clientUser;

    public static BumTickManager tickManager;

    public static BumSceneManager sceneManager;

    public static BumPoolManager poolManager;

    //template manager
    public static BumModelManager objectManager;

    public int initSpeed = 1;  
    public int initFinishedCount = 0;
    public bool loadLocalOver = false;
    public bool syncUserConfigOver = true;

    public static bool initFinished = false;

    public static BumLogic getInstance()
    {
        if (Instance == null)
            Instance = new BumLogic();

        return Instance;
    }

    public void Startup()
    {
        System.TimeSpan ts = System.DateTime.Now - BumApp.Instance.clientInitTime;
        BumBase.Log("====================================Init Total Use {0}s===========================================", ts.TotalSeconds);

        //do...
        //if (string.IsNullOrEmpty(BumLogic.clientUser.userLocalRecord.userPhone))
        //{

        //    BumRep.uiManager.openWindow<BumUILoginPage>(0);
        //}
        //else
        //{
        //    BumRep.uiManager.openWindow<BumUIMainPage>();
        //    BumRep.uiManager.openWindow<BumUIBottomTools>();
        //}
        //BumRep.uiManager.openWindow<BumUIMainPage>();
        //BumRep.uiManager.openWindow<BumUIBottomTools>();
        //UIManager.Instance.OnOpen(PageType.MainPage);
    }

    public void syncUserConfig()
    {
        clientUser = new BumUser();
        clientUser.init();
        clientUser.getUserConfig();
    }

    public void initAppBaseConfig()
    {
        //-----------------<add runtime controller>-------------------//

        inputController = new BumInputController();
        inputController.init();

        errorHandler = new BumErrorHandler();
        errorHandler.init();


        tickManager = new BumTickManager();
        tickManager.init();

        poolManager = new BumPoolManager();
        poolManager.init();

        sceneManager = new BumSceneManager();
        sceneManager.init();
        //------------------------------------------------------------//
        
        initFinished = true;
    }

    public IEnumerator initAppLogic()
    {
        initModula = new List<IBumInitable>();
        IEnumerator enumtor;

        //-----------------<add data manager>-------------------//

        objectManager = new BumModelManager();
        initModula.Add(objectManager);

        //------------------------------------------------------//

        foreach (IBumInitable modula in initModula)
        {
            enumtor = modula.init();
            while (enumtor.MoveNext() && !modula.getLoadingState())
                yield return null;
        }
        loadLocalOver = true;
        Resources.UnloadUnusedAssets();
    }

    public int getModulaCount()
    {
        return initModula.Count;
    }

    public void tick()
    {
        tickManager.tick();

    }

    public void clear()
    {
        tickManager.clear();
    }
}
