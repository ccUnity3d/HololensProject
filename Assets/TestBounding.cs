using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBounding : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2)) {
            ShowBoundingBoxRig.Instance.SetTarget(this.gameObject);
            ShowBoundingBoxRig.Instance.Activate();
        }
	}
}
