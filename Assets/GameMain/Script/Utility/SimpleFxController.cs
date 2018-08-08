using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SimpleFxController : MonoBehaviour {
    public ParticleSystem[] fx;
    public Animation ssss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFx() {

        StartCoroutine(AutoHide(ssss.clip.length));
        fx.ToList().ForEach((p) => {
            p.Play();
        });
    }

    IEnumerator AutoHide(float delay) {
        ssss.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);
        ssss.gameObject.SetActive(false);
    }
}
