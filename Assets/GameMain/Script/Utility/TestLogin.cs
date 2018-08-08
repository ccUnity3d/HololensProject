using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class TestLogin : MonoBehaviour
{
    //验证码输入框
    public InputField verificationInput;
    //手机号输入框
    public InputField phoneNumberInput;
    //获取验证码
    public Button getVerificationButton;
    //登陆
    public Button loginButton;
    // Use this for initialization
    void Start()
    {

        verificationInput = findControl<InputField>("verificationInput");
        phoneNumberInput = findControl<InputField>("phoneNumberInput");
        getVerificationButton = findControl<Button>("getVerificationButton");
        loginButton = findControl<Button>("loginButton");
        getVerificationButton.onClick.AddListener(onGetVerification);
        loginButton.onClick.AddListener(onLogin);
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void onLogin()
    {
        if (CheckPhoneNumber(phoneNumberInput.text))
        {
            getVerificationButton.GetComponentInChildren<Text>().text = phoneNumberInput.text;
        }
        else {
            Debug.Log("false");
        }
    }

    private void onGetVerification()
    {

    }

    private bool CheckPhoneNumber(string phoneNumber)
    {
        Regex rx = new Regex(@"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
        if (!rx.IsMatch(phoneNumber)) //不匹配
        {
            phoneNumber = ""; //变成空
            Debug.Log("手机号格式不对，请重新输入！");    //弹框提示
            return false;
        }
        else
        {
            return true;
        }
    }




    #region 封装寻找组件方法

    public T findControl<T>(string name) where T : Component
    {
        GameObject obj = dfsFindObject(transform, name);
        if (obj == null)
            return null;

        return obj.GetComponent<T>();
    }

    public GameObject dfsFindObject(Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; ++i)
        {
            Transform node = parent.GetChild(i);
            if (node.name == name)
                return node.gameObject;

            GameObject target = dfsFindObject(node, name);
            if (target != null)
                return target;
        }

        return null;
    }
    #endregion

}
