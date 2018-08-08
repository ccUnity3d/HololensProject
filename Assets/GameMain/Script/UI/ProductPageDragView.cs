using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ProductPageDragView : MonoBehaviour, IManipulationHandler
{

    private Vector3 origPosition;
    private float moveSensitivity = 1.5f;
    private MainPageController mainpageController
    {
        get
        {
            return MainPageController.Instance;
        }
    }
     private MainPage mainPage
    {
        get
        {
            return MainPage.Instance;
        }
    }

    public  void OnManipulationCanceled(ManipulationEventData eventData)
    {
    }
    public  void OnManipulationCompleted(ManipulationEventData eventData)
    {
        BumUITool.SetActionFalse(mainPage.adjustButton.gameObject);
        BumUITool.SetActionFalse(mainPage.hideButton.gameObject);
        BumUITool.SetActionTrue(mainPage.downButton.gameObject);
        BumUITool.SetActionTrue(mainPage.cancelButton.gameObject);
    }
    public  void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (mainpageController.isManipulation)
        {
            Vector3 newPos = Vector3.right * (eventData.CumulativeDelta.x) + Vector3.up * (eventData.CumulativeDelta.y)+Vector3.forward * (eventData.CumulativeDelta.z);
            Debug.Log(newPos);
            mainpageController.skin.transform.position = origPosition + newPos * moveSensitivity;
        }
    }
    public  void OnManipulationStarted(ManipulationEventData eventData)
    {
        if (mainpageController.isManipulation)
        {
            origPosition = mainpageController.skin.transform.position;
        }
    }
}
