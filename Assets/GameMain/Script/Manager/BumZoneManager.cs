using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumZoneManager : IBumInitable
{
    public Dictionary<string , BumZone> zoneCollection;
    private bool loadingOver = false;

    public BumZoneManager()
    {
        zoneCollection = new Dictionary<string, BumZone> ();
    }

    public IEnumerator init()
    {
        string zoneUrl = "http://testdata.bumie.org/Bumie/0927/Zone/1c54bd6c-bdc9-4323-8ced-8a89435e17c6.json";
        BumResourceManager.loadResource(zoneUrl, (object data) =>
        {
            string json = (string)data;

            object jsonData = BumJsonTool.FromJson(json);
            if (jsonData == null)
            {
                return;
            }
            List<object> listData = jsonData as List<object>;
            for (int i = 0; i < listData.Count; i++)
            {
                BumZone zone = new BumZone();
                zone.deserializer(listData[i] as Dictionary<string, object>);
                zoneCollection.Add(zone.uuid, zone);
            }

            loadingOver = true;
        }, null, BumResourceType.eBumResourceType_json);
        yield return null;
    }

    public IEnumerator uninit()
    {
        yield return null;
    }

    public bool getLoadingState()
    {
        return loadingOver;
    }

    public BumZone getZone(string id)
    {
        BumZone temp = null;
        zoneCollection.TryGetValue(id, out temp);

        return temp;
    }
}
