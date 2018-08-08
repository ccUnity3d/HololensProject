using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// IInputClickHandler , IFocusable 是HoloTookit 工具里面提供的 HoloToolkit.Unity.InputModule
public class ProductItemFunction : UGUIItemFunction,IBeginDragHandler,IEndDragHandler, IInputClickHandler,IFocusable
{
    private RawImage image;
    private Vector2 inputStartPos;
    private bool dragCopyed = false;
    private Vector2 offset = Vector2.zero;
    public Action<BumModel> onClickByShowCAD;
    public Action<PointerEventData> deleFollowMouse;
    public Action<PointerEventData> deleSetDraggedPosition;

    private BumModel productData
    {
        get
        {
            return data as BumModel;
        }
    }

    protected override void Awake()
    {
        image = BumUITool.GetUIComponent<RawImage>(this.transform, "RawImage");
        DrapButton button = this.gameObject.AddComponent<DrapButton>();
        button.onPointerDownDele = onPointerDown;
        button.onPointerUpDele = onPointerUp;
        button.onDragDele = onPointDrag;
        button.onPointerClickDele = onClick;
    }



    private void onPointerDown(PointerEventData obj)
    {
        inputStartPos = obj.position;
    }

    private void onPointDrag(PointerEventData obj)
    {
        if (dragCopyed)
        {
            if (deleFollowMouse != null) deleFollowMouse(obj);
            return;
        }
        offset = obj.delta;
        if (offset == Vector2.zero)
        {
            return;
        }
        if (deleSetDraggedPosition != null) deleSetDraggedPosition(obj);
    }

    private void onPointerUp(PointerEventData obj)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            //Sprite spri = this.GetComponent<Image>().sprite; ;
            dragCopyed = true;
            scroRect.vertical = false;
            //if (deleSetDraggingIcon != null) deleSetDraggingIcon(materialData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragCopyed = false;
        scroRect.vertical = true;
    }

    private void onClick()
    {

        //LoaderPool.InnerLoad("animation_sulengji",SimpleLoadTypeEnum.prefabAssetBundle,null,null);
    }

    public override void Render()
    {
        base.Render();
        Debug.Log(productData.thumbnailUri);
        BumResourceManager.loadResource(productData.thumbnailUri,onLoadSetTextrue,null,BumResourceType.eBumResourceType_texture2D);
    }
    private void onLoadSetTextrue(object obj) {
        Texture2D texture = obj as Texture2D;
        image.texture = texture;
    }
    protected override void Update()
    {
        base.Update();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (onClickByShowCAD != null)
        {
            onClickByShowCAD(productData);
            Debug.Log("Show CAD   显示CAD ");
        }

    }

    public void OnFocusEnter()
    {
    }

    public void OnFocusExit()
    {
       
    }
}
