using UnityEngine;

public class TweenCombine : MonoBehaviour {

	public bool useScale = false;
	public bool usePosition = false;
	public bool useRotat = false;
	public bool useColor = true;

	public Style useScaleType = Style.PingPong;
	public Style usePositionType = Style.PingPong;
	public Style useRotatType = Style.PingPong;
	public Style useColorType = Style.PingPong;

    public Vector3 fromScale = Vector3.one;
    public Vector3 toScale = Vector3.one;
    public float scaleDeletime = 0;
    public float timeOfScaleOnce = 1;

	public bool worldSpace = false;
    public Vector3 fromPosition = Vector3.zero;
    public Vector3 toPosition = Vector3.zero;
    public float positionDeletime = 0;
    public float timeOfPositionOnce = 1;

	public Vector3 fromAngle = Vector3.zero;
    public Vector3 toAngle = Vector3.zero;
    public float rotatDeletime = 0;
	public float timeOfRotatOnce = 1;

    public Color fromColor = Vector4.one;
    public Color toColor = Vector4.one;
    public float colorDeletime = 0;
    public float timeOfColorOnce = 1;
    public RenderType type = RenderType.Material;
    public string shaderColorName = "";


    [HideInInspector]
    public AnimationCurve animationCurve_Position = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
    [HideInInspector]
    public AnimationCurve animationCurve_Rotation = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
    [HideInInspector]
    public AnimationCurve animationCurve_Scale = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
    [HideInInspector]
    public AnimationCurve animationCurve_Color = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

	void OnEnable () {

		if (usePosition && fromPosition != toPosition)
        {
            MyTweenPosition tweenPos = this.gameObject.GetComponent<MyTweenPosition>();
            if (tweenPos == null) { tweenPos = this.gameObject.AddComponent<MyTweenPosition>(); }
			tweenPos.worldSpace = worldSpace;
			tweenPos.style = usePositionType;
            tweenPos.from = fromPosition + transform.localPosition;
            tweenPos.to = toPosition + transform.localPosition;
            tweenPos.duration = timeOfPositionOnce;
            tweenPos.curve = animationCurve_Position;
            tweenPos.ResetToBeginning();
            tweenPos.PlayForward();
        }
        else {
            
        }

		if (useScale && fromScale != toScale)
        {
            MyTweenScale tweenScal = this.gameObject.GetComponent<MyTweenScale>();
            if (tweenScal == null) { tweenScal = this.gameObject.AddComponent<MyTweenScale>(); }
			tweenScal.style = useScaleType;
            tweenScal.from = fromScale;
            tweenScal.to = toScale;
            tweenScal.duration = timeOfScaleOnce;
            tweenScal.curve = animationCurve_Scale;
            tweenScal.ResetToBeginning();
            tweenScal.PlayForward();
        }
        else {
            
        }

		if (useRotat && fromAngle != toAngle)
		{
            MyTweenRotation tweenRotat = this.gameObject.GetComponent<MyTweenRotation>();
            if (tweenRotat == null) { tweenRotat = this.gameObject.AddComponent<MyTweenRotation>(); }
			tweenRotat.style = useRotatType;
			tweenRotat.from = fromAngle;
			tweenRotat.to = toAngle;
			tweenRotat.duration = timeOfRotatOnce;
            tweenRotat.curve = animationCurve_Rotation;
            tweenRotat.ResetToBeginning();
            tweenRotat.PlayForward();
		}
		else {
			
		}

		if (useColor && fromColor != toColor)
        {
            MyTweenColor tweenColor = this.gameObject.GetComponent<MyTweenColor>();
            if (tweenColor == null) { tweenColor = this.gameObject.AddComponent<MyTweenColor>(); }
            tweenColor.renderType = type;
            tweenColor.shaderColorName = shaderColorName;
            tweenColor.style = useColorType;
            tweenColor.from = fromColor;
            tweenColor.to = toColor;
            tweenColor.duration = timeOfColorOnce;
            tweenColor.curve = animationCurve_Color;
            tweenColor.ResetToBeginning();
            tweenColor.PlayForward();
        }
        else {
            
        }
        
	}

}

