using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResourceEvent : MyEvent
{
    public const string Progress = "Progress";
    public const string Cancel = "Cancel";
    public const string Complete = "Complete";
    public const string QueueProgress = "QueueProgress";
    public const string QueueComplete = "QueueComplete";
    public const string ItemProgress = "ItemProgress";

    public LoadResourceEvent(string type, object data = null) : base(type, data)
    {

    }
}
