using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

    // Use this for initialization
    public  Transform parentTran;
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 directionToTarget = Camera.main.transform.position - transform.position;
        directionToTarget.y = 0;
        float angle = Vector3.Angle(directionToTarget.normalized, Vector3.forward);
        //float dot = Vector3.Dot(directionToTarget.normalized, Vector3.forward);
        //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        //parentTran.rotation = Quaternion.Euler(0, 180-angle, 0);
        if (directionToTarget.x <= 0)
        {
            parentTran.rotation = Quaternion.Euler(0, 180 - angle, 0);
        }
        else
        {
            parentTran.rotation = Quaternion.Euler(0, angle-180, 0);
        }
    }
}
