using System;
using System.Collections.Generic;
using UnityEngine;

public class BumTickManager
{
    private List<Action> actionList = new List<Action>();
    private List<Action> temp = new List<Action>();

    //private List<float> tempAddTime = new List<float>();
    //private List<Action> tempAdd = new List<Action>();

    public BumTickManager()
    {

    }

    public void init()
    {

    }

    public  void add(Action act)
    {
        if (actionList.Contains(act))
            return;

        actionList.Add(act);
    }

    public  void Remove(Action act)
    {
        if (actionList.Contains(act) == false)
            return;

        actionList.Remove(act);
    }

    public void tick()
    {
        //if (tempAdd.Count > 0)
        //{
        //    for (int i = 0; i < tempAdd.Count; i++)
        //    {
        //        if (Time.realtimeSinceStartup > tempAddTime[i])
        //        {
        //            if (actionList.Contains(tempAdd[i]) == false)
        //            {
        //                actionList.Add(tempAdd[i]);
        //            }
        //            tempAddTime.RemoveAt(i);
        //            tempAdd.RemoveAt(i);
        //        }
        //    }            
        //}
        if (actionList.Count == 0) return;
        temp.Clear();
        temp.AddRange(actionList);
        for (int i = 0; i < temp.Count; i++)
        {
            temp[i]();
        }
    }

    public void clear()
    {
        actionList.Clear();
        temp.Clear();
    }
}
