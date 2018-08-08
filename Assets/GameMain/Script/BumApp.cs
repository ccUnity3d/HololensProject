using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class BumApp : MonoBehaviour
{
    public static BumApp Instance;

    BumLogic appLogic;
    BumRep appRep;

    public bool loadFinished = false;

    public DateTime clientInitTime = DateTime.Now;

    public static bool isFisrtLogin = true;

    void Awake()
    {
        BumBase.Log("====================================================================================");
        BumBase.Log("Application.platform = {0}", Application.platform);
        BumBase.Log("Application.dataPath = {0}", Application.dataPath);
        BumBase.Log("Application.streamingAssetsPath = {0}", Application.streamingAssetsPath);
        BumBase.Log("Application.persistentDataPath = {0}", Application.persistentDataPath);
        BumBase.Log("Application.temporaryCachePath = {0}", Application.temporaryCachePath);
        BumBase.Log("Application.unityVersion = {0}", Application.unityVersion);
        BumBase.Log("SystemInfo.deviceModel = {0}", SystemInfo.deviceModel);
        BumBase.Log("====================================================================================");

        Instance = this;

        appLogic = new BumLogic();
        appRep = new BumRep();

        setDivce();
        DontDestroyOnLoad(this);
    }

    void Start ()
    {
        StartCoroutine(processLoading());
    }

    void FixedUpdate ()
    {
        if (!loadFinished) return;

        appLogic.tick();
    }

    void Update()
    {
        if (BumLogic.inputController != null)
            BumLogic.inputController.tick();
    }

    void setDivce()
    {
        BumDefine.isDebug = true;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    IEnumerator processLoading()
    {
        //初始化UIRoot及相机
        appRep.init();

        StartCoroutine(appLogic.initAppLogic());
        while (!appLogic.loadLocalOver)
            yield return null;

        appLogic.initAppBaseConfig();
        while (!BumLogic.initFinished)
            yield return null;

        //check isFirstLogin state
        //....

        appLogic.syncUserConfig();
        //while (!appLogic.syncUserConfigOver)
            yield return 2;

        loadFinished = true;

        yield return null;

        //startup
        appLogic.Startup();
    }
}

