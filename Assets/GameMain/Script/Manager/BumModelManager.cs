using System.Collections;
using System.Collections.Generic;

public class BumModelManager : IBumInitable
{
    public Dictionary<string, BumModel> modelCollection;
    private bool loadingOver = false;

    public BumModelManager()
    {
        modelCollection = new Dictionary<string, BumModel>();
    }

    public IEnumerator init()
    {
        string modelUrl = "http://pboypz1tn.bkt.clouddn.com/was/table/projectTable.json";
        BumResourceManager.loadResource(modelUrl,(object data) =>
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
                BumModel model = new BumModel();
                model.Deserializable(listData[i] as Dictionary<string, object>);
                modelCollection.Add(model.uuid, model);
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

    public BumModel getModel(string id)
    {
        BumModel temp = null;
        modelCollection.TryGetValue(id, out temp);

        return temp;
    }
}
