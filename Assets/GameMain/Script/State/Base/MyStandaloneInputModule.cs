using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MyStandaloneInputModule : StandaloneInputModule
{
    public static MyStandaloneInputModule current;

    public MyStandaloneInputModule() : base()
    {
        current = this;
    }

    public bool tryGetMyMousePointerEventData(out PointerEventData data)
    {
        data = null;
        if (Input.touchCount == 0)
        {
            return false;
        }
        bool press;
        bool release;
        data = GetTouchPointerEventData(Input.GetTouch(0), out press, out release);
        return true;
    }
}
