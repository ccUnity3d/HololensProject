using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BumUserLocalRecord  {


    public string userid = null;
    public string userPhone = "";
    public string createTime = "";

    public List<string> userList = new List<string>();
    public void Deserializer(Dictionary<string,object> dic)
    {
        foreach (string  key in dic.Keys)
        {
            switch (key)
            {
                case "userid":
                    userid = BumJsonTool.getStringValue(dic,key);break;
                case "userPhone":
                    userPhone = BumJsonTool.getStringValue(dic, key); break;
                case "createTime":
                    createTime = BumJsonTool.getStringValue(dic, key); break;
                case "userList":
                    userList = BumJsonTool.getStringListValue(dic, key); break;
                default:
                    break;
            }
        }
    }

    internal void init()
    {
        FileInfo info = new FileInfo(BumDefine.bumUserConfigPath+ "UserLocalRecord.json");
        if (!info.Exists)
        {
            return;
        }
        BumResourceManager.loadResource(BumDefine.bumUserConfigPathFile+ "UserLocalRecord.json", (object data) =>
        {
            string json = data.ToString();
            if (json == null)
            {
                return;
            }
            object jsonData = BumJsonTool.FromJson(json);
            Deserializer(jsonData as Dictionary<string, object>);
        }, null, BumResourceType.eBumResourceType_userInfo);
    }
}
