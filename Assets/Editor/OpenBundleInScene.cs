using System.IO;
using UnityEngine;
using UnityEditor;

public class OpenBundleInScene
{
    [MenuItem("Assets/OpenBundle", false, 0)]
    [MenuItem("Tools/OpenBundle", false, 1)]
    public static void Open()
    {
        Object[] objs = Selection.objects;
        if (objs != null && objs.Length != 0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                Object obj = objs[i];
                string path = AssetDatabase.GetAssetPath(obj);
                //if (path.EndsWith(".assetbundle") == false && path.EndsWith(".unity3d") == false&& path.EndsWith(""))//.unity3d
                //{
                //    Debug.Log(path + " 不是.AssetBundle .unity3d");
                //    continue;
                //}
                //else
                //{
                    FileStream fs = File.OpenRead(Application.absoluteURL + path);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);

                    string name = Path.GetFileNameWithoutExtension(path);
                    AssetBundle ab = AssetBundle.LoadFromMemory(bytes);
                    Object[] gameObj = ab.LoadAllAssets();
                    if (gameObj == null)
                    {
                        Debug.LogError(path + " 不是AssetBundle");
                        ab.Unload(false);
                        continue;
                    }
                    Object go = null;
                    for (int k = 0; k < gameObj.Length; k++)
                    {
                        if (gameObj[k].GetType() == typeof(GameObject))
                        {
                            go = gameObj[k];
                        }
                    }
                    if (go == null)
                    {
                        Debug.LogError(path + " AssetBundle不是GameObject");
                        ab.Unload(false);
                        return;
                    }

                    GameObject instatObj = GameObject.Instantiate(go) as GameObject;
                    ab.Unload(false);
                }
            //}
        }
        else
        {
            Debug.Log("未选中assetBundle");
        }
    }

}
