using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItemData : ItemData
{
    public bool special = false;
    public string stringId = "0";
    public string seekID = "local";
    public string textureURL = "";
    public ProductItemData(string id,string seekid,string textureURL)
    {
        this.stringId = id;
        this.seekID = seekid;
        this.textureURL = textureURL;
    }

}
