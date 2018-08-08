using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateLine : MonoBehaviour {

    private MeshRenderer []  materials;

	// Use this for initialization
	void Start () {
        materials = this.GetComponentsInChildren<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].material.mainTextureScale = Vector2.right * materials[i].transform.localScale.x * 0.03f + Vector2.up;
            materials[i].material.mainTextureOffset = new Vector2(-Time.time * 0.1f, 0);
        }
    }
}
