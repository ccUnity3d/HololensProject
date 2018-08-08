using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class UGUIItemFunction : MonoBehaviour {

    public int index;
    public ScrollRect scroRect;
    private ItemData _data;
    public ItemData data
    {
        get { return _data; }
        set
        {
            _data = value;
            Render();
        }
    }
  
    protected abstract void Awake();


    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void Render()
    {

    }
}
