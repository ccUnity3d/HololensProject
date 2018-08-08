using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public GameObject[] gameObjects;
	// Use this for initialization
	//void Start () {
 //       StartCoroutine(LookAtCamera());
	//}
    private void OnDisable()
    {
        StopCoroutine(LookAtCamera());
    }
    private void OnEnable()
    {
        StartCoroutine(LookAtCamera());
    }
    // Update is called once per frame
    void Update () {
       
	}
    IEnumerator LookAtCamera()
    {
        while (true)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].transform.LookAt(Camera.main.transform);
            }
            yield return 0.1;
        }
    }
}
