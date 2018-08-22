﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;

public class GazeMoveState : InputState {

    public const string Name = "GazeMoveState";
    private GestureRecognizer gestureRecognizer;
    TwoHandManipulatable twoHandManipulatable;
    private Placeable placeable;
    private ModelView modelView;
    public override void enter()
    {
        base.enter();
        if (inputStateMachine.targetTransform == null) {
            return;
        }
        twoHandManipulatable = inputStateMachine.targetTransform.GetComponent<TwoHandManipulatable>();
        if (twoHandManipulatable == null) {
            twoHandManipulatable = inputStateMachine.targetTransform.gameObject.AddComponent<TwoHandManipulatable>();
        }
        twoHandManipulatable.enabled = false;
        modelView = inputStateMachine.targetTransform.GetComponent<ModelView>();
        if (modelView == null)
        {
            Debug.LogError("no exit ModelView");
        }
        else {
            modelView.enabled = false;
        }
        placeable = inputStateMachine.targetTransform.GetComponent<Placeable>();
        placeable.enabled = true;
        gestureRecognizer = new GestureRecognizer();
        gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);

        gestureRecognizer.Tapped += TappedEvent;

        // Start looking for gestures.
        gestureRecognizer.StartCapturingGestures();
    }

    private void TappedEvent(TappedEventArgs obj)
    {
        placeable.OnSelected();
        if (!placeable.IsPlacing) {
            twoHandManipulatable.enabled = true;
            modelView.enabled = true;
            inputStateMachine.setState(Free3DState.Name);
        }
    }

    public override void exit()
    {
        base.exit();
        twoHandManipulatable.enabled = true;
        modelView.enabled = true;
        gestureRecognizer.StopCapturingGestures();
        gestureRecognizer.Tapped -= TappedEvent;
    }

}
