using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MyTweenColor : MyTween
{
    [HideInInspector]
    public Color from = Color.white;
    [HideInInspector]
    public Color to = Color.white;

    [HideInInspector]
    public RenderType renderType = RenderType.Material;
    [HideInInspector]
    public string shaderColorName = "_Color";

    private bool mCash = false;
    private Material mMat;
    private Renderer mR;
    private SpriteRenderer mSpR;
    private Mesh mMesh;

    private Transform mtran;

    private void Cash()
    {
        mCash = true;
        switch (renderType)
        {
            case RenderType.Material:
                mR = transform.GetComponent<Renderer>();
                if (mR != null)
                {
                    mMat = mR.material;
                }
                else
                {
                    DebugX.Log(transform.name + "物体 Material == null");
                    enabled = false;
                }
                break;
            case RenderType.ShaderColor:
                mR = transform.GetComponent<Renderer>();
                if (mR != null)
                {
                    mMat = mR.material;
                    if (mMat.HasProperty(shaderColorName))
                    {
                        break;
                    }
                    DebugX.Log(transform.name + "物体 Material.HasProperty(" + shaderColorName + ") == false");
                    break;
                }
                DebugX.Log(transform.name + "物体 Material == null");
                enabled = false;
                break;
            case RenderType.SpriteRenderer:
                mSpR = transform.GetComponent<SpriteRenderer>();
                if (mSpR == null)
                {
                    DebugX.Log(transform.name + "物体 SpriteRenderer == null");
                    enabled = false;
                }
                break;
            case RenderType.MeshColor:
                MeshFilter mMesR = transform.GetComponent<MeshFilter>();
                if (mMesR == null)
                {
                    mMesh = mMesR.mesh;
                    DebugX.Log(transform.name + "物体 Mesh == null");
                    enabled = false;
                }
                break;
            default:
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        mtran = transform;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnUpdate(float factor)
    {
        value = Color.Lerp(from, to, factor);
    }

    public Color value
    {
        get
        {
            if (mCash == false)
            {
                Cash();
            }
            return getColorByType();
        }
        set
        {
            if (mCash == false)
            {
                Cash();
            }
            setColorByType(value);
        }
    }
    
    private Color getColorByType()
    {
        switch (renderType)
        {
            case RenderType.Material:
                if (mMat != null) return mMat.color;
                break;
            case RenderType.ShaderColor:
                if (mMat != null && mMat.HasProperty(shaderColorName)) return mMat.GetColor(shaderColorName);
                break;
            case RenderType.SpriteRenderer:
                if (mSpR != null) return mSpR.color;
                break;
            case RenderType.MeshColor:
                if (mMesh != null)
                {
                    break;
                }
                if (mMesh.colors.Length < 0)
                {
                    break;
                }
                Color color = mMesh.colors[0];
                //for (int i = 0; i < mMesh.colors.Length; i++)
                //{
                //    if (color != mMesh.colors[i])
                //    {
                //        DebugX.Log();
                //        break;
                //    }
                //}
                return color;
                //break;
            default:
                break;
        }

        return Color.white;

    }

    private void setColorByType(Color color)
    {
        switch (renderType)
        {
            case RenderType.Material:
                if (mMat != null) mMat.color = color;
                break;
            case RenderType.ShaderColor:
                if (mMat != null && mMat.HasProperty(shaderColorName)) mMat.SetColor(shaderColorName, color);
                break;
            case RenderType.SpriteRenderer:
                if (mSpR != null) mSpR.color = color;
                break;
            case RenderType.MeshColor:
                if (mMesh != null)
                {
                    for (int i = 0; i < mMesh.colors.Length; i++)
                    {
                        mMesh.colors[i] = color;
                    }
                }
                break;
            default:
                break;
        }
    }

    public override void ResetToBeginning()
    {
        base.ResetToBeginning();
        value = from;
    }


    public override void SetEndToCurrentValue()
    {
        base.SetEndToCurrentValue();
        to = value;
    }

    public override void SetStartToCurrentValue()
    {
        base.SetStartToCurrentValue();
        from = value;
    }
}
