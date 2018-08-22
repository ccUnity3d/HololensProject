using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialState : FunctionState
{
    public const string Name = "MaterialState";
    public bool idDown = false;
    ModelView modelView;
    private FunctionMachine functionMachine
    {
        get
        {
            return FunctionMachine.Instance;
        }
    }
    public override void enter()
    {
        base.enter();
        // 爆炸视觉
        //if (inputStateMachine.targetTransform == null)
        //{
        //    return;
        //}
        //modelView = inputStateMachine.targetTransform.GetComponent<ModelView>();
        //if (modelView == null)
        //{
        //    return;
        //}
        //if (modelView.myanimation.isPlaying)
        //{
        //    return;
        //}
        //idDown = !idDown;
        //modelView.myanimation["animation_clip_explosion"].time = 0;
        //modelView.myanimation["animation_clip_explosion"].speed = 1;
        //modelView.myanimation.Play("animation_clip_explosion");
        //modelView.OnPlanyInfo();
        //if (idDown)
        //{
        //    modelView.myanimation.Play("animation_clip_explosion");
        //    modelView.myanimation["animation_clip_explosion"].time = 0;
        //    modelView.myanimation["animation_clip_explosion"].speed = 1.0f;
        //}
        //else
        //{
        //    modelView.myanimation["animation_clip_explosion"].time = modelView.myanimation["animation_clip_explosion"].clip.length;
        //    modelView.myanimation["animation_clip_explosion"].speed = -1.0f;
        //    modelView.myanimation.Play("animation_clip_explosion");
        //    //modelView.myanimation.CrossFade("animation_clip_explosion");
        //    //modelView.myanimation.Rewind("animation_clip_explosion");
        //}

        //if (explodeView.isDown && modelView.myanimation.isPlaying == false)
        //{
        //    modelView.myanimation.Play("animation_clip_explosion");
        //}
        //if (explodeView.isDown == false && modelView.myanimation.isPlaying == false)
        //{
        //    modelView.myanimation.PlayQueued("animation_clip_explosion");
        //}
    }
    public override void exit()
    {
        base.exit();
        ////idDown = false;
        //if (modelView != null)
        //{
        //    idDown = false;
        //    modelView.myanimation.Play("animation_clip_explosion");
        //    modelView.myanimation["animation_clip_explosion"].time = 0;
        //    modelView.myanimation["animation_clip_explosion"].speed = 1;
        //    modelView.myanimation.Sample();
        //    modelView.myanimation.Stop();
        //modelView.OnExitPlanyInfo();

        //    //modelView.myanimation["animation_clip_explosion"].time = 0;
        //    //modelView.myanimation["animation_clip_explosion"].speed = 0f;
        //}
    }
    //public override void mUpdate()
    //{
    //    base.mUpdate();
    //}
    //public override void Ready()
    //{
    //    base.Ready();
    //}
}
