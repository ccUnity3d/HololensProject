using UnityEngine;
using System.Collections;
using System;

public class UIController<T> : MySingleton<T>, IController where T : IInstance, new()
{
    public IUIData uiData;

    public IPage page;

    public IPage getPage
    {
        get
        {
            return page;
        }
    }

    public GameObject skin;
    /// <summary>
    /// 每一次打开
    /// </summary>
    public virtual void awake()
    {
        
    }
    /// <summary>
    /// 每一次加载完成
    /// </summary>
    public virtual void ready()
    {
        
    }
    /// <summary>
    /// 生产页面
    /// </summary>
    /// <param name="goClone"></param>
    public  void SetData(GameObject goClone,bool noReset)
    {
        skin = goClone;
        if (page != null) page.SetData(skin, noReset);
        ready();
        MyCallLater.Add(awake);
    }
    /// <summary>
    /// 每一次关闭
    /// </summary>
    public virtual void sleep()
    {
        
    }
}
public interface IController
{
    IPage getPage { get;  }
    void SetData(GameObject goClone,bool reset);
    void ready();
    void awake();
    void sleep();
}
