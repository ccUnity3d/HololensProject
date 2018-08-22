using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.GazeRuler;
using UnityEngine.XR.WSA.Input;
using System;

public class MeasureState : FunctionState
{
    public const string Name = "MeasureState";
    public bool idDown = false;
    private GestureRecognizer gestureRecognizer;

    private MeasureManager measureManager {
        get {
            return MeasureManager.Instance;
        }
    }
    private ModelView modelView;
    public override void enter()
    {
        modelView = inputStateMachine.targetTransform.GetComponent<ModelView>();
        modelView.enabled = false;
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

        gestureRecognizer.Tapped += TappedEvent;

        // Start looking for gestures.
        gestureRecognizer.StartCapturingGestures();
    }

    private void TappedEvent(TappedEventArgs obj)
    {
        measureManager.OnSelect();
    }

    public override void exit()
    {
        base.exit();
        modelView.enabled = true;
        measureManager.DeleteLine();
        gestureRecognizer.StopCapturingGestures();
        gestureRecognizer.Tapped -= TappedEvent;
    }
}
