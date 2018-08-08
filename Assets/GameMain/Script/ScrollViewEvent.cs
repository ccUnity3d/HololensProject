using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewEvent : MyEvent
{

    public const string ProductItemOnclick = "ProductItemOnclick";

    public ScrollViewEvent(string type, object data = null) : base(type, data)
    {

    }
}
