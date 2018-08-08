using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ProductScrollView : UGUIScrollView ,IDragHandler{

    
        
    private GameObject specialBottomskin;
    public Action<BumModel> OnModelClick;
    public Arrangement _arrangement = Arrangement.Vertical;
    protected override void Init()
    {
        base.Init();
        ItemSkin = transform.Find("item").gameObject;
        ItemSkin.AddComponent<ProductItemFunction>();
        UDragScroll uscr = ItemSkin.AddComponent<UDragScroll>();
        uscr.scroRect = ScroRect;
        SetData(440,460, _arrangement,100,100,50,50,2);
    }

    public override void Display(List<ItemData> data)
    {
        base.Display(data);

    }

    protected override void ItemAddListion(UGUIItemFunction func)
    {
        base.ItemAddListion(func);
        ProductItemFunction itemFunc = func as ProductItemFunction;
        itemFunc.onClickByShowCAD = OnItemClick;
    }

    private void OnItemClick(BumModel obj)
    {
        Debug.Log("OnItemClick");
        if (OnModelClick != null) OnModelClick(obj);
    }

    protected override void ItemChildGameObject(GameObject obj = null)
    {
        base.ItemChildGameObject(obj);
    }

    public override void RefreshDisplay(List<ItemData> data = null, bool restPos = false, bool isChange = false)
    {
        //base.RefreshDisplay(data, restPos, isChange);
        foreach (UGUIItemFunction item in itemDic.Values)
        {
            item.gameObject.SetActive(false);
            skinList.Push(item.gameObject);
        }
        itemDic.Clear();
        if (restPos == true) ResetPostion();
        if (data != null)
        {
            this.Msgs = data;
        }
        if (data != null || isChange)
        {
            SetContentSize(this.Msgs.Count + 2);
        }
        for (int i = 0; i < this.Msgs.Count; i++)
        {
            if ((i < CurrentIndex - UpperLimitIndex) && (CurrentIndex > LowerLimitIndex) && !isChange)
            {
                return;
            }
            skinClone = GetInstance();
            skinClone.transform.SetParent(ContentRectTrans);
            skinClone.transform.localPosition = GetLoaclPosByIndex(i );
            skinClone.transform.localScale = Vector3.one;
            skinClone.GetComponent<RectTransform>().SetSiblingIndex(i );
            ProductItemFunction func = skinClone.GetComponent<ProductItemFunction>();
            func.scroRect = ScroRect;
            func.data = this.Msgs[i];
            func.index = i;
            itemDic.Add(i , func);
            ItemAddListion(func);
            ItemChildGameObject(skinClone);
        }
      
        //// 容器的区域限制
        //if (data != null || isChange == true)
        //{
        //    int length = Msgs.Count;
        //    if (length >= 6)
        //    {
        //        ScrollRectTrans.sizeDelta = new Vector2(ContentRectTrans.sizeDelta.x, CellHeight * 6 + CellHeightSpace * (6));
        //    }
        //    else
        //    {
        //        ScrollRectTrans.sizeDelta = new Vector2(ContentRectTrans.sizeDelta.x, CellHeight * (length + 1) + CellHeightSpace * (length + 1));
        //    }
        //}

    }

    //public override void SetContentSize(int length)
    //{
    //    int lineCount = length / MaxPerLine;
    //    switch (_arrangement)
    //    {
    //        case Arrangement.Horizontal:
    //            ContentRectTrans.sizeDelta = new Vector2(CellWidth * lineCount + CellWidthSpace * (lineCount - 1), ContentRectTrans.sizeDelta.y);
    //            break;
    //        case Arrangement.Vertical:
    //            ContentRectTrans.sizeDelta = new Vector2(ContentRectTrans.sizeDelta.x, CellHeight * lineCount + CellHeightSpace * (lineCount));
    //            break;
    //    }
    //}

    protected override void ResetPostion()
    {
        base.ResetPostion();
    }

    public override Vector3 GetLoaclPosByIndex(int index)
    {
        return base.GetLoaclPosByIndex(index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //SetDraggedPosition(eventData);
    }
    //public void SetDraggedPosition(PointerEventData data)
    //{
    //    MyTweenRectPosition myTween = MaterialController.GetComponent<MyTweenRectPosition>();
    //    if (myTween == null) myTween = MaterialController.gameObject.AddComponent<MyTweenRectPosition>();
    //    if (ScrollRectTrans.sizeDelta.y < MaterialController.sizeDelta.y)
    //    {
    //        MaterialController.sizeDelta = new Vector2(100, ScrollRectTrans.sizeDelta.y + 100);
    //    }
    //    else
    //    {
    //        MaterialController.sizeDelta = new Vector2(100, MaterialController.sizeDelta.y + 100);
    //    }
    //    myTween.from = Vector2.up * -30f;
    //    myTween.to = Vector2.up * 130f;
    //    myTween.duration = 1;
    //    if (data.delta.y > 0)
    //    {
    //        myTween.SetStartToCurrentValue();
    //        myTween.to = Vector2.up * 130f;
    //        myTween.PlayForward();
    //        return;
    //    }
    //    myTween.SetStartToCurrentValue();
    //    myTween.to = Vector2.up * -30;
    //    myTween.PlayForward();

    //}
}
