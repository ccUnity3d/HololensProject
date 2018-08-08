using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumPathManager 
{
    public BumPathManager()
    {

    }

    public void init()
    {

    }

    public bool hasCachedById(string id)
    {
        string path = getUrlById(id);
        return System.IO.File.Exists(path);
    }

    public string getIdByUrl(string URL)
    {
        string id = "";
        string[] strs = URL.Split('/');
        if (strs.Length > 1)
        {
            id = strs[strs.Length - 2];
        }
        return id;
    }

    public string getUrlById(string id)
    {
        return "";//LocalReadWriteURLHead + "alluserdata/" + "BuMie" + "/Json/ProductConfigCache/" + id + ".json";
    }
}
