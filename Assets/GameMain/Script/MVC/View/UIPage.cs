using UnityEngine;
using System.Collections;
using System;

public class UIPage<T> : MySingleton<T>, IPage where T : IInstance, new()
{
    public string getPrefabPath;

    private GameObject _skin;

    public GameObject skin
    {
        get
        {
            return _skin;
        }

        set
        {
            _skin = value;
        }
    }

    public string GetPrefabPath()
    {
        return  getPrefabPath;
    }

    public RectTransform skinRectTran;
    public Transform skinTran;

    public void SetData(UnityEngine.Object arg,bool noReset)
    {
        skin = (GameObject)arg;
        skinRectTran = skin.GetComponent<RectTransform>();
        if (skinRectTran==null)
        {
            skinTran = skin.GetComponent<Transform>();
            skinTran.position = Vector3.zero;
            skinTran.localScale = Vector3.one;
        }
        if (!noReset && skinRectTran !=null)
        {
            skinRectTran.sizeDelta = Vector2.zero;
            skinRectTran.anchoredPosition3D = Vector3.zero;
            skinRectTran.localScale = Vector3.one;
        }
        
        Ready(arg);
    }
    public void ChildPageSetData(UnityEngine.Object arg1)
    {
        skin = (GameObject)arg1;
        Ready(arg1);
    }
    protected virtual void Ready(UnityEngine.Object arg1)
    {

    }
}

public interface IPage
{
    GameObject skin { get; set; }
    void SetData(UnityEngine.Object arg,bool noReset);
    string GetPrefabPath();
}
