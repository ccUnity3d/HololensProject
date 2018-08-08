using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;

public class ResourcesPacker : Singleton<ResourcesPacker>
{
    #region 序列化字段   定义动态加载Sprite
    public static Dictionary<string, Sprite> mLoadSprite = new Dictionary<string, Sprite>();
    public static Dictionary<string, Material> mLaodMater = new Dictionary<string, Material>();
    public static Dictionary<string, Font> mLoadFont = new Dictionary<string, Font>();
    public static Dictionary<string, Texture2D> mLoadTexture = new Dictionary<string, Texture2D>();

    #endregion

    protected override void Awake()
    {
        LoadTexture();
        LoadMaterial();
        /*
        LoadTxSprite();
        LoadCharaSprite();
        LoadSkill1Sprite();
        LoadSkill2Sprite();
        LoadBuffSprite();
        LoadEnemySprite();
        LoadPawnSprite();
        LoadTitleSprite();
        LoadSkillOkSprite();
        LoadEnemyTpyeSprite();
        LoadMaterial();
        LoadFont();
        ColorAdd();
        LoadComboSprite();
        LoadSprite();
        */
    }
   
    #region  
    [Tooltip("Loading")]
    public List<Texture2D> mTextures;
    void LoadTexture()
    {
        for (int i = 0; i < mTxSprite.Count; i++)
        {
            if (!mLoadTexture.ContainsValue(mTextures[i]))
            {
                mLoadTexture.Add(mTextures[i].name, mTextures[i]);
            }
        }
    }
    #region  游戏中材质球的加载
    [Tooltip("游戏中材质球的加载")]
    public List<Material> material;
    void LoadMaterial()
    {
        for (int k = 0; k < material.Count; k++)
        {
            if (!mLaodMater.ContainsValue(material[k]))
            {
                mLaodMater.Add(material[k].name, material[k]);
            }
        }
    }
    #endregion
    #endregion
    [HideInInspector]
    #region  获取将领技能头像Sprite
    [Tooltip("将领头像sprite")]
    public List<Sprite> mTxSprite;
    void LoadTxSprite()
    {
        for (int i = 0; i < mTxSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mTxSprite[i]))
            {
                mLoadSprite.Add(mTxSprite[i].name, mTxSprite[i]);
            }
        }

    }
    #endregion
    [HideInInspector]
    #region  角色将领人物Sprite
    [Tooltip("角色 sprite")]
    public List<Sprite> mCharaSprite;
    void LoadCharaSprite()
    {
        for (int i = 0; i < mCharaSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mCharaSprite[i]))
            {
                mLoadSprite.Add(mCharaSprite[i].name, mCharaSprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  技能1图片Sprite
    [Tooltip("技能一 sprite")]
    public List<Sprite> mSkill1Sprite;
    void  LoadSkill1Sprite()
    {
        for (int i = 0; i < mSkill1Sprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mSkill1Sprite[i]))
            {
                mLoadSprite.Add(mSkill1Sprite[i].name, mSkill1Sprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  技能2图片Sprite
    [Tooltip("技能二 sprite")]
    public List<Sprite> mSkill2Sprite;
    void LoadSkill2Sprite()
    {
        for (int i = 0; i < mSkill2Sprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mSkill2Sprite[i]))
            {
                mLoadSprite.Add(mSkill2Sprite[i].name, mSkill2Sprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region buff sprite
    [Tooltip("buff sprite")]
    public List<Sprite> mBuffSprite;
    void LoadBuffSprite()
    {
        for (int i = 0; i < mBuffSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mBuffSprite[i]))
            {
                mLoadSprite.Add(mBuffSprite[i].name, mBuffSprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region 敌人 sprite
    [Tooltip("敌人 sprite")]
    public List<Sprite> mEnemySprite;
    void LoadEnemySprite()
    {
        for (int i = 0; i < mEnemySprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mEnemySprite[i]))
            {
                mLoadSprite.Add(mEnemySprite[i].name, mEnemySprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  颜色加成
    [Tooltip("颜色加成 Sprite")]
    public List<Sprite> mColorAdd;
    public void ColorAdd()
    {
        for (int i = 0; i < mColorAdd.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mColorAdd[i]))
            {
                mLoadSprite.Add(mColorAdd[i].name, mColorAdd[i]);
            }
        }
    }

    #endregion
    [HideInInspector]
    #region 棋子图片
    [Tooltip("棋子 sprite")]
    public List<Sprite> mPawnSprite;
    void LoadPawnSprite()
    {
        for (int i = 0; i < mPawnSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mPawnSprite[i]))
            {
                mLoadSprite.Add(mPawnSprite[i].name, mPawnSprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region   技能触发sprite
    [Tooltip("游戏中显示的标语Sprite")]
    public List<Sprite> mTitleSprite;
    void LoadTitleSprite()
    {
        for (int i = 0; i < mTitleSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mTitleSprite[i]))
            {
                mLoadSprite.Add(mTitleSprite[i].name, mTitleSprite[i]);
            }
        }
    }
    #endregion

    [HideInInspector]
    #region Skillok
    [Tooltip("SkillOk Sprite")]
    public List<Sprite> mSkillOkSprite;
    void LoadSkillOkSprite()
    {
        for (int i = 0; i < mSkillOkSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mSkillOkSprite[i]))
            {
                mLoadSprite.Add(mSkillOkSprite[i].name, mSkillOkSprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  敌人类型
    [Tooltip("EnemyType Sprite")]
    public List<Sprite> mEnemyTypeSprite;
    void LoadEnemyTpyeSprite()
    {
        for (int i = 0; i < mEnemyTypeSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mEnemyTypeSprite[i]))
            {
                mLoadSprite.Add(mEnemyTypeSprite[i].name, mEnemyTypeSprite[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  字体
    [Tooltip("Font")]
    public List<Font> mFont;
    void LoadFont()
    {
        for (int i = 0; i < mFont.Count; i++)
        {
            if (!mLoadFont.ContainsValue(mFont[i]))
            {
                mLoadFont.Add(mFont[i].name, mFont[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region  Combo数量
    [Tooltip("ComboNumber Sprite")]
    public List<Sprite> mComboNumber;
    void LoadComboSprite()
    {
        for (int i = 0; i < mComboNumber.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mComboNumber[i]))
            {
                mLoadSprite.Add(mComboNumber[i].name, mComboNumber[i]);
            }
        }
    }
    #endregion
    [HideInInspector]
    #region 其他sprite
    [Tooltip("sprite")]
    public List<Sprite> mSprite;
    void LoadSprite()
    {
        for (int i = 0; i < mSprite.Count; i++)
        {
            if (!mLoadSprite.ContainsValue(mSprite[i]))
            {
                mLoadSprite.Add(mSprite[i].name, mSprite[i]);
            }
        }
    }
    #endregion
}
