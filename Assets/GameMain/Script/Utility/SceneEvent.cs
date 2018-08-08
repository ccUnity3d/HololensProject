using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEvent : MyEvent {

    public const string ShowCAD = "ShowCAD";
    public const string LoadFactoryHome = "LoadFactoryHome";
    public const string AddWorldAnchor = "AddWorldAnchor";
    public const string DestoryWorldAnchor = "DestoryWorldAnchor";
    public const string Versions = "Versions";
    public SceneEvent(string type, object data = null) : base(type, data)
    {

    }
}
