using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BumSceneManager
{
    //private List<float> tempAddTime = new List<float>();
    //private List<Action> tempAdd = new List<Action>();

    public BumSceneManager()
    {

    }

    public void init()
    {

    }
    public void clear() {

    }
    public IEnumerator LoadAsyncScene(int sceneIndex,Action callBack)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        if (callBack != null) callBack.Invoke();
    }
    public IEnumerator UnloadAsyncScene(int sceneIndex, UnityAction callBack)
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync(sceneIndex);
        while (!async.isDone)
        {
            yield return null;
        }
        if (callBack != null) callBack.Invoke();
    }
}
