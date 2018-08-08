using UnityEngine;
using System.Collections;
using System;

public class MyState : IMyState {

    protected UGUIHitUI uguiHitUI
    {
        get {
            return UGUIHitUI.Instance;
        }
    }

    public void Enter()
    {
        if (GlobalConfig.isMyDebug == true)
        {
#if UNITY_ANDROID || UNITY_IPHONE  //安卓  
            if (needUpdate == true) MyTickManager.Add(mPhoneUpdate);
            enterPhone();
#else
            if (needUpdate == true) MyTickManager.Add(mUpdate);
                        enterNotPhone();
#endif
        }
        else {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //Debug.Log("RuntimePlatform.Android");
                if (needUpdate == true) MyTickManager.Add(mPhoneUpdate);
                enterPhone();
            }
            else {
                //Debug.Log("RuntimePlatform.Win7");
                if (needUpdate == true) MyTickManager.Add(mUpdate);
                enterNotPhone();
            }
        }

        enter();         
    }


    public void Exit()
    {
        if (GlobalConfig.isMyDebug == true)
        {
#if !UNITY_ANDROID && !UNITY_IPHONE  //安卓  
            if (needUpdate == true) MyTickManager.Remove(mUpdate);
            exitNotPhone();
#else
            if (needUpdate == true) MyTickManager.Remove(mPhoneUpdate);
            exitPhone();
#endif
        }
        else {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (needUpdate == true) MyTickManager.Remove(mPhoneUpdate);
                exitPhone();
            }
            else {
                if (needUpdate == true) MyTickManager.Remove(mUpdate);
                exitNotPhone();
            }
        }        

        exit();
    }

    protected virtual bool needUpdate {
        get { return true; }
    }

    public virtual void enter(){}
    public virtual void enterPhone() { }
    public virtual void enterNotPhone() { }

    public virtual void mUpdate(){}

    public virtual void mPhoneUpdate() { }

    public virtual void exit(){}

    public virtual void exitPhone() { }
    public virtual void exitNotPhone() { }
}
