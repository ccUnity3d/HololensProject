using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BumUser
{
    public BumUserInfo userConfig;
    public BumUserLocalRecord userLocalRecord;
    public void init()
    {
        userConfig = new BumUserInfo();
        userLocalRecord = new BumUserLocalRecord();
    }

    public void getUserConfig()
    {

    }

    IEnumerator startInit( object []data)
    {
        userLocalRecord.init();
        yield return null;
        userConfig.init();
        yield return null;
    }

    public bool checkFirstLogin()
    {
        return true;
    }
}
